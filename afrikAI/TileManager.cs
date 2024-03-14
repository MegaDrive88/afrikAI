using afrikAI.Pathfinding_Modules;
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
        public TileManager(string _filepath, ref int _width,ref int _height) // might be better to change 
        {
            generator = new TileGenerator();
            tiles = generator.GenerateTiles(_filepath);
            width = tiles.GetLength(1);_width = width;
            height = tiles.GetLength(0);_height = height;
		}
        public void DrawTiles()
        {
            foreach (Tile tile in tiles)
            {
                tile.Draw();
            }
        }
        public void SwapTiles(int[][] positions)
        {
            SwapTiles(positions[0], positions[1]);
        }
        public void SwapTiles(int[] pos1, int[] pos2)
        {
            Tile tmp_Tile = tiles[pos1[0], pos1[1]];
            tiles[pos1[0], pos1[1]] = tiles[pos2[0], pos2[1]];
            tiles[pos2[0], pos2[1]] = tmp_Tile;
            tmp_Tile.SetPos(pos2);
            tiles[pos1[0], pos1[1]].SetPos(pos1);
        }
        
        private void DrawPath(TilePath path)
        {
            foreach (Vector2 pos in path.Path)
            {
                tiles[(int)pos.Y, (int)pos.X].Draw(ConsoleColor.Red);
            }
        }
        private TilePath getClosestPath(PathfindingContext pathfindingContext)
        {
            Tile zebra = getZebra();
            List<Tile> waters = getWaters();
            Path shortestPath;

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
