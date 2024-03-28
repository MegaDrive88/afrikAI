using afrikAI.Pathfinding_Modules;
using System.Diagnostics;
using System.Numerics;

namespace afrikAI
{
    public class TileManager
    {
        private Tile[,] tiles;
        private int width;
        private int height;
        private TileGenerator generator;
        public TileManager(int _width, int _height, TileGeneratorData tileGeneratorData)
        {
            width = _width;
            height = _height;
            generator = new TileGenerator(width, height);
            tiles = generator.GenerateTiles(tileGeneratorData);
        }
		public TileManager(string _fileName, ref int _width,ref int _height):this(_fileName)
        {
            _width = width; _height = height;
		}
		public TileManager(string _fileName)
		{
			generator = new TileGenerator();
			tiles = generator.GenerateTiles(_fileName);
			width = tiles.GetLength(1);
			height = tiles.GetLength(0);
		}
		public void DrawTiles()
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw();
            }
        }
        public void DrawTile(int x ,int y)
        {
            tiles[y,x].Draw();
        }
        public Tile AddToTile(int x, int y, int amount)
        {
            Tile tile = tiles[y,x];
            int currNum = int.Parse(Statics.GetTypeNumFromType(tile.TileType));
            currNum += amount + 2000; // ugly but works
            tile.TileType = Statics.tileTypes[Math.Abs(currNum %= Statics.tileTypes.Count).ToString()];
            return tile;
        }
        public void DrawTile(int x, int y, ConsoleColor color)
        {
            tiles[y, x].Draw(color);
        }
        public void SwapTiles(int[][] positions)
        {
            SwapTiles(positions[0], positions[1]);
        }
        public void SwapTiles(int[] pos1, int[] pos2)
        {
            Tile tmp_Tile = (Tile)tiles[pos1[1], pos1[0]].Clone();
            tiles[pos1[1], pos1[0]] = (Tile)tiles[pos2[1], pos2[0]].Clone();
            tiles[pos2[1], pos2[0]] = tmp_Tile;
            tmp_Tile.SetPos(pos2);
            tiles[pos1[1], pos1[0]].SetPos(pos1);
        }
        public void SetTileTpye(int x, int y, string Type)
        {
            tiles[y,x].TileType = Type;
        }
              
        public void DrawShortestPathToWater(PathfindingContext pathfindingContext)
        {
            drawPath(getClosestPathToWater(pathfindingContext));
            //foreach (Tile tile in tiles) {
            //    Debug.WriteLine($"x = {tile.x} y = {tile.y} distance = {tile.ClosestDistance}");
            //}
        }
        public void SaveTiles(string fileName)
        {
            using(StreamWriter sw = new StreamWriter($"saved_deserts\\{fileName}"))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        sw.Write($"{Statics.GetTypeNumFromType(tiles[y, x].TileType)} ");
                    }
                    sw.Write('\n');
                }
            }
        }
        private void drawPath(TilePath path)
        {
            foreach (Vector2 pos in path.Path)
            {
                tiles[(int)pos.Y, (int)pos.X].Draw(ConsoleColor.Red);
            }
        }
        private TilePath getClosestPathToWater(PathfindingContext pathfindingContext)
        {
            Tile zebra = getZebra();
            List<Tile> waters = getWaters();
            TilePath shortestPath = pathfindingContext.GetShortestPath(tiles, zebra, waters[0]);
            for (int i = 1; i < waters.Count; i++)
            {
                TilePath path = pathfindingContext.GetShortestPath(tiles, zebra, waters[i]);
                if(path.Length < shortestPath.Length) shortestPath = path;
            }
            return shortestPath;
        }
        private Tile getZebra()
        {
            foreach (Tile tile in tiles) if (tile.TileType == "zebra") return tile;
            throw new Exception("No zebra on the map");
        }
        private List<Tile> getWaters()
        {
            List<Tile> waters = new List<Tile>();
            foreach (Tile tile in tiles) if (tile.TileType == "water") waters.Add(tile);
            return waters;
        }
    }
}
