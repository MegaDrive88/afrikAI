using System.Diagnostics;

namespace afrikAI
{
	public class InputHandler
	{
		private Game game;
		private Menu menu;

		public InputHandler(Game _game, Menu _menu)
		{
			game = _game;
			menu = _menu;
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
		public void HandleMenuInput()
		{
			ConsoleKey consoleKey = Console.ReadKey(true).Key;
			if (Statics.KeyBinds.MenuDown.Contains(consoleKey)) menu.MenuUp();
			else if (Statics.KeyBinds.MenuDown.Contains(consoleKey)) menu.MenuDown();
			else if (Statics.KeyBinds.MenuConfirm.Contains(consoleKey)) menu.Confrim();
			else if (Statics.KeyBinds.MenuExit.Contains(consoleKey)) menu.Exit();
			else
			{
				Debug.WriteLine($"{consoleKey} not in keybinds, press another key.");
				HandleMenuInput();
			}
		}
	}
}
