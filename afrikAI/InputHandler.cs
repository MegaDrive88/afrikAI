using System.Diagnostics;

namespace afrikAI
{
	public class InputHandler
	{
		private Game game;
		private Menu menu;

        public InputHandler(Menu _menu) {
            menu = _menu;
        }
        public InputHandler(Game _game) {
            game = _game;
        }

        public int[][] GetGameInput()
		{
			return new int[][] { getCordInput("Add meg a tile 2 kordinátáját amelyet mozgatni szeretnél"), getCordInput("Add meg azt a két kordinátát ahova mozgatni szeretnéd") };
		}
		private int[] getCordInput(string inputMessage = "",string cord1Message = "", string cord2Message = "")
		{
			Console.WriteLine(inputMessage);
			int x, y;
			do
			{
				Console.Write($"{cord1Message} x = ");
				if (!int.TryParse(Console.ReadLine(), out x)) continue;
			} while (x < 0 || x >= game.Width);
			do
			{
				Console.Write($"{cord2Message} y = ");
				if (!int.TryParse(Console.ReadLine(), out y)) continue;
			} while (y < 0 || y >= game.Height);
			return new int[] { x, y };
		}
		public void HandleMenuInput() {
			ConsoleKey consoleKey = Console.ReadKey(true).Key;
			if (Statics.KeyBinds.MenuUp.Contains(consoleKey)) menu.MenuMove(-1);
			else if (Statics.KeyBinds.MenuDown.Contains(consoleKey)) menu.MenuMove(1);
			else if (Statics.KeyBinds.MenuConfirm.Contains(consoleKey)) menu.Confirm();
			else if (Statics.KeyBinds.MenuExit.Contains(consoleKey)) menu.Exit();
			else if (((int)consoleKey >= 48 && (int)consoleKey <= 57) ||
					 ((int)consoleKey >= 96 && (int)consoleKey <= 105)) menu.GetUserInput(consoleKey); // fomenube ne
			else if (consoleKey == ConsoleKey.Backspace) menu.DeleteLastChar();
			else {
				Debug.WriteLine($"{consoleKey} not in keybinds, press another key.");
				HandleMenuInput();
			}
		}
	}
}
