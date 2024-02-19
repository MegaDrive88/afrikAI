namespace afrikAI
{
    public class TileManager
    {
        
        private Tile[,] tiles;
        private int width;
        private int height;
        private TileGenerator tileGenerator;
        

        public TileManager(int _width, int _height, string? _filePath = null)
        {
            width = _width;
            height = _height;
            tileGenerator = new TileGenerator(width, height);
            tiles = tileGenerator.GenerateTiles(_filePath);
        }
        public void DrawTiles() { 
            foreach (Tile tile in tiles) tile.DrawTile(); 
        }
        public void SwapTiles(int x1, int y1, int x2, int y2)
        {    
            // c# 7.0 or newer
            (tiles[y2, x2], tiles[y1, x1]) = (tiles[y1, x1], tiles[y2, x2]);	

            // c# 6.0 or under.
			//Tile tmpTile = tiles[y1, x1];
            //tiles[y1,x1] = tiles[y2,x2];
            //tiles[y2,x2] = tmpTile;
        }
        public void SwapTiles(int[] cord1, int[] cord2) => SwapTiles(cord1[0], cord1[1], cord2[0], cord2[1]);
        
    }
}
