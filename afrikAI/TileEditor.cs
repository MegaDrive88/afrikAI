namespace afrikAI
{
	public class TileEditor
	{
		private const ConsoleColor SELECTEDCOLOR = ConsoleColor.Green;
		private TileManager tileManager;
		private int width,height;
		private string name;
		private int x { get => Console.CursorLeft * 2; }
		private int y { get => Console.CursorTop; }
        public TileEditor(string filePath) {
			tileManager = new TileManager(filePath, ref width, ref height);
			tileManager.DrawTiles();
		}
		public void MoveCursor(char dir)
		{
			int[] moveMatrix = Statics.moveMatrixes[dir];
			tileManager.DrawTile(x, y); // reset last tile
			tileManager.DrawTile(x + moveMatrix[0], y + moveMatrix[1], SELECTEDCOLOR);
		}
		public void EditTile(string tileTpye)
		{
			tileManager.SetTileTpye(x, y, tileTpye);
		}
		public void Save()
		{
			tileManager.SaveTiles(name);
		}
		public void ChangeTypeUp()
		{
			tileManager.AddToTile(x, y, 1);
		}
		public void ChangeTypeDown()
		{
			tileManager.AddToTile(x, y, -1);
        }

    }
}
