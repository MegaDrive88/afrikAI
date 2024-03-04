using System.Diagnostics;

namespace afrikAI
{
    public class TileManager
    {
        private Tile[,] tiles;
        private int width;
        private int height;
        private string filePath;

        public TileManager(int _width, int _height, string _filePath)
        {
            width = _width;
            height = _height;
            filePath = _filePath;
            tiles = new Tile[height, width];
            readFile();
        }
        public void DrawTiles()
        {
            foreach (Tile tile in tiles)
            {
                tile.DrawTile();
            }
        }
        private void readFile()
        {
            if(!File.Exists(filePath)) Debug.WriteLine($"Error In TileManager/readFile: File {filePath} doesn't exist");
            else
            { 
                using(StreamReader sr = new StreamReader(filePath))
                {
                    int y = 0;
                    while(!sr.EndOfStream)
                    {
                        string[] data = sr.ReadLine().Trim().Split(' ');
                        if (data.Length != width) Debug.WriteLine($"Warning in TileManager/readFile file width ({data.Length}) != Tiles width ({width})");
                        for (int x = 0;x < data.Length ;x++)
                        {
                            switch(data[x])
                            {
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
                    if(y != height) Debug.WriteLine( $"Warning in TileManager/readFile file height ({data.Length}) != Tiles width ({width})")
                }
            }
        }
    }
}
