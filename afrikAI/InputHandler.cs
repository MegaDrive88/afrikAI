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
		public string GetMenuInput()
		{
			string outPut;
			do
			{
				Console.Clear();
				Console.WriteLine("Írj be egy karaktert (a bal oldarlo) és nyomd meg az entert.");
				foreach (KeyValuePair<string, string> pair in menu.CurrentItems)
				{
					Console.WriteLine($"{pair.Key} -\t{pair.Value}");
				}
				Console.Write("\nKarakter = ");
				outPut = Console.ReadLine();

			} while (!menu.CurrentItems.ContainsKey(outPut));
			return outPut;
        }
	}
}
