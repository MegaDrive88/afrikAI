using System;
using System.Diagnostics;
using System.Numerics;

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
			Console.SetCursorPosition(0, height+1);
			int[] pos1, pos2;
			do
			{
				pos1 = getCordInput(width, height, "Adja meg a mozgatni kívánt mező 2 koordinátáját:");
				Console.WriteLine();
				if (!isValidCord(pos1, invalidTiles))
				{
					onInvalidCord();
				}
				else break;
			} while (true);
			do
			{
				pos2 = getCordInput(width, height, "Adja meg a mező új pozícióját:");
				Console.WriteLine();
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
			HandleEditorInput();
        }
		private void onInvalidCord()
		{
			Console.CursorTop--;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\nVizet, oroszlánt és zebrát nem mozgathat.\n");
			Console.ResetColor();
		}
		private bool isValidCord(int[] cord, List<Tile> invalidTiles)
		{
			if (cord == null) return false;
			foreach (Tile tile in invalidTiles) if (cord[0] == tile.x && cord[1] == tile.y) return false;
			return true;
		}
		private int[] getCordInput(int width, int height, string inputMessage = "", string cord1Message = "", string cord2Message = "")
		{
			Console.ResetColor();
			Console.WriteLine(inputMessage);
			//Menu menu;
			int x, y;
			do
			{
				Console.Write($"{cord1Message}x = ");
				//ConsoleKeyInfo k = Console.ReadKey(true);
				//if (k.Key == ConsoleKey.Escape) {
				//    menu = new Menu();
				//    return new[] { 0, 0 };
				//}
				//if (Statics.KeyBinds.AcceptedInputKeys().Contains(k.KeyChar)) Console.Write(k.KeyChar); - elso key re mukodik csak...
				if (!int.TryParse( Console.ReadLine(), out x)) continue; // k.KeyChar +. Ezt csinaljuk mar a menummel
            } while (x < 1 || x >= width + 1);
			do
			{
				Console.Write($"{cord2Message}y = ");
				if (!int.TryParse(Console.ReadLine(), out y)) continue;
			} while (y < 1 || y >= height + 1);
			return new int[] { x - 1, y - 1 };
		}
	}
}
