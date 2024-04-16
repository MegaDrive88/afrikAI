namespace afrikAI
{
	public class TileGenerator
	{
		private int width;
		private int height;
		private const string PATH = "saved_deserts\\";
		public TileGenerator(){}
		public TileGenerator(int _width, int _height)
		{
			width = _width;
			height = _height;
		}
		public Tile[,] GenerateTiles(string fileName)
		{
			if (fileName != null)
			{
				return readFile($"{PATH}{fileName}");
			}
			else
			{
				throw new Exception("Filepath = null in TileGenerator/GenerateTiles");
			}
		}
		public Tile[,] GenerateTiles(TileGeneratorData data)
		{
			Tile[,] tiles = new Tile[height, width];
			genNormalTiles(tiles);
			genWalls(data, tiles);
			//genlions?
			//genStartAndEndPoint(tiles);
			return tiles;
		}
		private void genWalls(TileGeneratorData data, Tile[,] tiles)
		{
			for (int i = 0; i < data.Wall; i++)
			{
                List<int> cords = getWallTopleft();
				while (tiles[cords[1], cords[0]].TileType == "wall") cords = getWallTopleft();
                int[] nums = new int[] { 0, 0 };
                for (int j = 0; j < cords[3]; j++) {
					tiles[cords[1] + nums[0], cords[0] + nums[1]] = new Tile(cords[0] + nums[1], cords[1] + nums[0], "wall");
					if (cords[2] == 0) nums[1]++;
					else nums[0]++;
				}
			}
		}
		private void genStartAndEndPoint(Tile[,] tiles)
		{
			//List<List<int>> cords = genRandCords(2, tiles);
			//tiles[cords[0][1], cords[0][0]] = endTile;
			//tiles[cords[1][1], cords[1][0]] = startTile;
		}
		private void genNormalTiles(Tile[,] tiles)
		{
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					tiles[y, x] = new Tile(x, y, "ground");
				}
			}
		}
		private List<int> getWallTopleft()
		{
            List<int> cords = new List<int> { Random.Shared.Next(0, width - 1), Random.Shared.Next(0, height - 1) };
			while (cords[0] == 0 && cords[1] == 0) cords = new List<int> { Random.Shared.Next(0, width - 1), Random.Shared.Next(0, height - 1) };
            int dir = Random.Shared.Next(0, 2);
			int length = Random.Shared.Next(1, Math.Max(dir == 0 ? width - cords[0] - 1 : height - cords[1] - 1, 1));
            cords.Add(dir);
            cords.Add(length);
            return cords;
        }
        private Tile[,] readFile(string filePath)
		{
			if (!File.Exists(filePath)) throw new Exception($"Error In TileManager/readFile: File {filePath} doesn't exist");
			else
			{
				height = File.ReadAllLines(filePath).Length;
				width = File.ReadAllLines(filePath)[0].Length/2;
                Tile[,] tiles = new Tile[height, width];
                using (StreamReader sr = new StreamReader(filePath))
				{
                    int y = 0;
					while (!sr.EndOfStream)
					{
						string[] data = sr.ReadLine().Trim().Split(' ');
                        for (int x = 0; x < data.Length; x++)
						{
							Tile newTile;
							if (Statics.tileTypes.ContainsKey(data[x].ToString())) newTile = new Tile(x, y, Statics.tileTypes[data[x].ToString()]);
							else throw new Exception($"No Type for: {data[x]} in TileGenerator / readfile(filepath)");
							tiles[y,x] = newTile;
						}
						y++;
					}
				}
				return tiles;
			}
		}
	}
	
}
