using System.Diagnostics;

namespace afrikAI
{
	public class TileEditor
	{
		private InputHandler inputHandler;
		private const ConsoleColor SELECTEDCOLOR = ConsoleColor.Green;
		private TileManager tileManager;
		private int width,height;
		private string name;
		private int x { get => Console.CursorLeft/2-1; }
		private int y { get => Console.CursorTop; }
        public TileEditor(string filePath) {
			Console.ResetColor();
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(filePath, ref width, ref height);
			tileManager.DrawTiles();
			Debug.WriteLine($"x = {Console.CursorLeft} y = {Console.CursorTop}");
			inputHandler.HandleEditorInput();
		}
		public void MoveCursor(char dir)
		{
			int[] moveMatrix = Statics.moveMatrixes[dir];
			tileManager.DrawTile(x, y); // reset last tile
			tileManager.DrawTile(Math.Max(Math.Min(x + moveMatrix[0], width-1), 0), Math.Max(Math.Min(y + moveMatrix[1], height-1), 0), SELECTEDCOLOR);
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
