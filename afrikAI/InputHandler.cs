using System.Diagnostics;

namespace afrikAI
{
	public class InputHandler
	{
		private readonly Menu menu;
		private readonly TileEditor tileEditor;
    
		public InputHandler(Menu _menu)
		{
			menu = _menu;
		}
		public InputHandler(TileEditor _editor)
		{
			tileEditor = _editor;
		}
		public InputHandler() { }
		public int[][] GetGameInput(int width, int height, List<Tile> invalidTiles)
		{
			Console.SetCursorPosition(0, height + 2);
			int[] pos1, pos2;
			Menu menu = new Menu(false);
            do
			{
				pos1 = menu.getCordInput(width, height, "Adja meg a mozgatni kívánt mező 2 koordinátáját:");
				if (!isValidCord(pos1, invalidTiles))
				{
					onInvalidCord();
				}
				else break;
			} while (true);
			do
			{
				pos2 = menu.getCordInput(width, height + 4, "Adja meg a mező új pozícióját:");
				if (!isValidCord(pos2, invalidTiles))
				{
					onInvalidCord();
				}
				else break;
			} while (true);
			return new int[][] {pos1 , pos2};
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
			else if (Statics.KeyBinds.EditorKeys['Q'].Contains(consoleKey)) tileEditor.Quit();
			else if (Statics.KeyBinds.EditorKeys['N'].Contains(consoleKey)) tileEditor.Next();
			HandleEditorInput();
        }
		private void onInvalidCord()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition(8, Console.CursorTop);
			Console.WriteLine("Vizet, oroszlánt és zebrát nem mozgathat.\n");
			Console.ResetColor();
		}
		private bool isValidCord(int[] cord, List<Tile> invalidTiles)
		{
			if (cord == null) return false;
			foreach (Tile tile in invalidTiles) if (cord[0] == tile.x && cord[1] == tile.y) return false;
			return true;
		}
	}
}
