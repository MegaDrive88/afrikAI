using System.Diagnostics;

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
       
    }
}
