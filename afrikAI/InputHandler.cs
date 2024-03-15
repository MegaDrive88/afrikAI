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
	}
}
