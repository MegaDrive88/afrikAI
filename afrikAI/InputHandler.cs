using System.Diagnostics;
using System.Numerics;

namespace afrikAI
{
	public class InputHandler
	{
		private readonly Game game;
		private readonly Menu menu;
		private readonly TileEditor tileEditor;
    
		public InputHandler(Menu _menu)
		{
			menu = _menu;
		}
		public InputHandler(Game _game) 
		{
			game = _game;
		}
		public InputHandler(TileEditor _editor)
		{
			tileEditor = _editor;
		}
		public int[][] GetGameInput(int width, int height)
		{
			Console.SetCursorPosition(0, height+1);
			return new int[][] { getCordInput(width, height, "Add meg a tile 2 kordinátáját amelyet mozgatni szeretnél"), getCordInput(width, height, "Add meg azt a két kordinátát ahova mozgatni szeretnéd") };
		}
		private int[] getCordInput(int width, int height, string inputMessage = "",string cord1Message = "", string cord2Message = "")
		{

			Console.ResetColor();

			Console.WriteLine(inputMessage);
			int x, y;
			do
			{
				Console.Write($"{cord1Message}x = ");
				if (!int.TryParse(Console.ReadLine(), out x)) continue;
			} while (x < 0 || x >= width);
			do
			{
				Console.Write($"{cord2Message}y = ");
				if (!int.TryParse(Console.ReadLine(), out y)) continue;
			} while (y < 0 || y >= height);
			return new int[] { x, y };
		}
		public void HandleMenuInput() {
			ConsoleKeyInfo consoleKey = Console.ReadKey(true);
			if (consoleKey.Key == ConsoleKey.UpArrow) menu.MenuMove(-1);
			else if (consoleKey.Key == ConsoleKey.DownArrow) menu.MenuMove(1);
			else if (Statics.KeyBinds.MenuConfirm.Contains(consoleKey.Key)) menu.Confirm();
			else if (consoleKey.Key == ConsoleKey.Escape) menu.Exit();
			else if (Statics.KeyBinds.AcceptedInputKeys().Contains((int)consoleKey.Key)) menu.GetUserInput(consoleKey);
			else if (consoleKey.Key == ConsoleKey.Backspace) menu.DeleteLastChar();
			else {
				Debug.WriteLine($"{consoleKey.Key} not in keybinds, press another key.");
				HandleMenuInput();
			}
		}
		public void HandleEditorInput()
		{
			ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
			ConsoleKey consoleKey = consoleKeyInfo.Key;
            foreach (char key in Statics.moveMatrixes.Keys)
            {
				if (Statics.KeyBinds.EditorKeys[key].Contains(consoleKey)) {
					tileEditor.MoveCursor(key);
					break;
				}
            }
			foreach (string s in Statics.tileTypes.Keys)
			{
				if(consoleKeyInfo.KeyChar == char.Parse(s)) tileEditor.EditTile(Statics.tileTypes[s]);
			}
			if (Statics.KeyBinds.EditorKeys['+'].Contains(consoleKey)) tileEditor.ChangeTypeUp();
			else if (Statics.KeyBinds.EditorKeys['-'].Contains(consoleKey)) tileEditor.ChangeTypeDown();
			else if (Statics.KeyBinds.EditorKeys['S'].Contains(consoleKey)) tileEditor.Save();
			HandleEditorInput();
        }
		
	}
}
