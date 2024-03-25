namespace afrikAI
{
	public class TileEditor
	{
		private const ConsoleColor SELECTEDCOLOR = ConsoleColor.Green;
		private TileManager tileManager;
		private int width,height;
		public TileEditor(string filePath) {
			tileManager = new TileManager(filePath, ref width, ref height);
			tileManager.DrawTiles();

		}
		public void MoveCursor(char dir)
		{
			int[] moveMatrix = Statics.moveMatrixes[dir];
			tileManager.DrawTile(Console.CursorLeft, Console.CursorTop); // reset last tile
			tileManager.DrawTile(Console.CursorLeft + moveMatrix[0], Console.CursorTop + moveMatrix[1], SELECTEDCOLOR);
		}
		
		
	}
}
