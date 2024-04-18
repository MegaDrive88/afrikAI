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
            drawNumbers();
        }
        public void DrawTile(int x ,int y)
        {
            tiles[y,x].Draw();
        }
        public void DrawTile(int[] cords)
        {
            tiles[cords[1], cords[0]].Draw();
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
        public void SwapTiles(Tile tile1, Tile tile2)
        {
            SwapTiles(new int[] { tile1.x, tile1.y }, new int[] { tile2.x, tile2.y });
        }
        public void SwapTiles(int[] pos1, int[] pos2)
        {
            Tile tmp_Tile = (Tile)tiles[pos1[1], pos1[0]].Clone();
            tiles[pos1[1], pos1[0]] = (Tile)tiles[pos2[1], pos2[0]].Clone();
            tiles[pos2[1], pos2[0]] = tmp_Tile;
			tiles[pos2[1], pos2[0]].SetPos(pos2);
            tiles[pos1[1], pos1[0]].SetPos(pos1);
        }
        public void SetTileTpye(int x, int y, string Type)
        {
            tiles[y,x].TileType = Type;
        }
        public string GetTileType(int x, int y) => tiles[y, x].TileType;
        public TileGeneratorData GetTilesData()
        {
            int lion = 0;int water = 0; int wall = 0; 
            foreach (Tile tile in tiles)
            {
                switch (tile.TileType)
                {
                    case "lion":
                        lion++; 
                        break;
					case "water":
                        water++;
                        break;
                    case "wall":
                        wall++;
                        break;
                }
            }
            return new TileGeneratorData(width, height, wall, lion, water);
        }
        public void DrawShortestPathToWater(PathfindingContext pathfindingContext)
        {
            TilePath? path = getClosestPathToWater(pathfindingContext);
            if(path != null) drawPath(path);
		}
        public void MoveCloserToWater(PathfindingContext pathfindingContext, Game? game = null)
        {
            Tile zebra = GetZebra();
            TilePath? path = getClosestPathToWater(pathfindingContext);
            List<Tile> nextTiles = GetNextTiles(tiles, zebra, width, height);
            List<Tile> nextWater = nextTiles.Where(t => t.TileType == "water").ToList();
            if(nextWater.Count > 0 && game != null)
            {
                Tile water = nextWater.First();
                zebra.TileType = "ground";
                water.TileType = "zebra";
                zebra.Draw();
                water.Draw();
                Thread.Sleep(1000);
                game.GameEnd(true);
			}
            
            List<Tile> nextLions = nextTiles.Where(x => x.TileType == "lion").ToList();
            if(nextLions.Count > 0)
            {
                foreach (Tile tile in nextTiles)
                {
                    if (tile.TileType != "wall" && tile.TileType != "lion")
                    {                        
						SwapTiles(tile, zebra);
						return;                   
                    }
                }
            }
            if(path == null) 
            {
                Debug.WriteLine("No Path found.");
                return;
            }
            if(path.Length > 0)
            {
                SwapTiles(new int[] { zebra.x, zebra.y }, new int[] { (int)path.Path[1].X, (int)path.Path[1].Y });
                zebra = GetZebra();  
			}
		}
        public void SaveTiles(string fileName)
        {
            try
            {
				using (StreamWriter sw = new StreamWriter($"saved_deserts\\{fileName}"))
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
            catch (Exception e)
            {
				Debug.WriteLine($"SaveTiles Error: {e.Message}");
            }
            
        }
		public void MoveCloserToTile(PathfindingContext pathfindingContext, Tile startTile, Tile endTile, Game game)
		{
            if (startTile.TileType == "lion" && endTile.TileType == "zebra" && GetNextTiles(tiles, startTile, width, height).Count(x => x.TileType == "zebra") > 0) {
                if (game.isRunning) {
                    startTile.TileType = "ground";
                    endTile.TileType = "lion";
                    startTile.Draw();
                    endTile.Draw();
                    Thread.Sleep(1000);
                    game.GameEnd(false); 
                
                }
                return;
            }
			TilePath? path = pathfindingContext.GetShortestPath(tiles, startTile, endTile);
            if (path == null) return;
			if (path.Length > 0)
			{
				SwapTiles(new int[] { startTile.x, startTile.y }, new int[] { (int)path.Path[1].X, (int)path.Path[1].Y });
                if (startTile.TileType == "lion") Thread.Sleep(500);
                DrawTile(startTile.x, startTile.y);
                DrawTile((int)path.Path[1].X, (int)path.Path[1].Y);
			}
		}
		public Tile GetZebra()
		{
			foreach (Tile tile in tiles) if (tile.TileType == "zebra") return tile;
			throw new Exception("No zebra on the map");
		}
        public List<Tile> GetLions()
        {
            List<Tile> lions = new List<Tile>();
            foreach (Tile tile in tiles)
            {
                if(tile.TileType == "lion") lions.Add(tile);
            }
            return lions;
        }
        public List<Tile> GetInvalidTiles()
        {
            List<Tile> invalidTiles = new List<Tile>();
            foreach (Tile tile in tiles) if (Statics.invalidInputTypes.Contains(tile.TileType)) invalidTiles.Add(tile);
            return invalidTiles;
        }
        private void drawPath(TilePath path)
        {
            foreach (Vector2 pos in path.Path)
            {
                tiles[(int)pos.Y, (int)pos.X].Draw(ConsoleColor.Red);
            }
        }
        private TilePath? getClosestPathToWater(PathfindingContext pathfindingContext)
        {
            Tile zebra = GetZebra();
            List<Tile> waters = getWaters();
            TilePath? shortestPath = pathfindingContext.GetShortestPath(tiles, zebra, waters[0]);
            for (int i = 1; i < waters.Count; i++)
            {
                TilePath? path = pathfindingContext.GetShortestPath(tiles, zebra, waters[i]);
                if(path != null && (shortestPath == null || path.Length < shortestPath.Length)) shortestPath = path;
            }
            return shortestPath;
        }
        private void drawNumbers() {
            for (int i = 0; i < tiles.GetLength(0); i++) {
                Console.SetCursorPosition(tiles.GetLength(1) * 2, i);
                if (i % 2 == 0) Console.ForegroundColor = ConsoleColor.White;
                else Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(i + 1);
            }
            Console.WriteLine();
            for (int i = 0; i < tiles.GetLength(1); i++) {
                if (i % 2 == 0) Console.ForegroundColor = ConsoleColor.White;
                else Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write((i + 1).ToString() + new string(' ', 2 - (i + 1).ToString().Length));
            }
            Console.ResetColor();
            Console.SetCursorPosition(width*2, height-1);
        }
        private List<Tile> getWaters()
        {
            List<Tile> waters = new List<Tile>();
            foreach (Tile tile in tiles) if (tile.TileType == "water") waters.Add(tile);
            return waters;
        }
        public static List<Tile> GetNextTiles(Tile[,] tiles, Tile Tile, int width, int height)
        {
            List<Tile> nextTiles = new List<Tile>();
            if (Tile.x > 0) nextTiles.Add(tiles[Tile.y, Tile.x - 1]);
            if (Tile.x < width - 1) nextTiles.Add(tiles[Tile.y, Tile.x + 1]);
            if (Tile.y > 0) nextTiles.Add(tiles[Tile.y - 1, Tile.x]);
            if (Tile.y < height - 1) nextTiles.Add(tiles[Tile.y + 1, Tile.x]);
            return nextTiles;
        }

    }
}
