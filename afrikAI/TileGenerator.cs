using System.Diagnostics;

namespace afrikAI
{
	public class TileGenerator
	{
		private int width;
		private int height;

		public TileGenerator(int _width, int _height)
		{
			width = _width;
			height = _height;
		}

		public Tile[,] GenerateTiles(string? filePath)
		{
			if(filePath != null)
			{
				return readFile(filePath);
			}
			else
			{
				return genTiles(width*height/10);
			}
			
		}
		private Tile[,] genTiles(int wallCount)
		{
			Tile[,] tiles = new Tile[height, width];
			genNormalTiles(tiles);
			genWalls(wallCount, tiles);
			genStartAndEndPoint(tiles);
			return tiles;

		}
		private void genWalls(int wallCount, Tile[,] tiles)
		{
			List<int[]> randCords = genRandCords(wallCount, tiles);
			foreach (int[] cord in randCords)
			{
				//tiles[cord[1], cord[0]] = wallTile
			}
		}
		private void genStartAndEndPoint(Tile[,] tiles)
		{
			List<int[]> cords = genRandCords(2, tiles);
			//tiles[cords[0][1], cords[0][0]] = endTile;
			//tiles[cords[1][1], cords[1][0]] = startTile;
		}
		private void genNormalTiles(Tile[,] tiles)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					//tiles[y,x] = normalTile
				}
			}
		}
		private List<int[]> genRandCords(int amount, Tile[,] tiles)
		{
			List<int[]> randCords = new List<int[]>();
			for (int i = 0; i < amount; i++)
			{
				int[] cords;
				do
				{
					cords = new int[] { Random.Shared.Next(0, width), Random.Shared.Next(0, height) };
				} while (tiles[cords[0], cords[1]].GetType() == typeof(Tile));
				randCords.Add(cords);
			}
			return randCords;
		}
		private Tile[,] readFile(string filePath)
		{
			if (!File.Exists(filePath)) throw new Exception($"Error In TileManager/readFile: File {filePath} doesn't exist");
			else
			{
				Tile[,] tiles = new Tile[height,width];
				using (StreamReader sr = new StreamReader(filePath))
				{
					int y = 0;
					while (!sr.EndOfStream)
					{
						string[] data = sr.ReadLine().Trim().Split(' ');
						if (data.Length != width) Debug.WriteLine($"Warning in TileManager/readFile file width ({data.Length}) != Tiles width ({width})");
						for (int x = 0; x < data.Length; x++)
						{
							switch (data[x])
							{
								// could use dictionary?

								case "0":
									// create TileType0 with TileFactory;
									break;
								case "1":
									// create TileType1 with TileFactory;
									break;
								case "2":
									// create TileType2 with TileFactory;
									break;
							}
						}
						y++;
					}
					if (y != height) Debug.WriteLine($"Warning in TileManager/readFile file height ({y}) != Tiles height ({height})");
				}
				return tiles;
			}

		}
	}
	
}
