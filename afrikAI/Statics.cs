namespace afrikAI
{
	public static class Statics
	{
		public static class KeyBinds
		{
			public readonly static ConsoleKey[] MenuConfirm = new ConsoleKey[] { ConsoleKey.Enter, ConsoleKey.Spacebar };
			public readonly static Dictionary<char, ConsoleKey[]> EditorKeys = new Dictionary<char, ConsoleKey[]>()
			{
				{'L',  new ConsoleKey[] {ConsoleKey.A, ConsoleKey.LeftArrow } },
				{'R',  new ConsoleKey[] {ConsoleKey.D, ConsoleKey.RightArrow} },
				{'U',  new ConsoleKey[] {ConsoleKey.W, ConsoleKey.UpArrow } },
				{'D',  new ConsoleKey[] {ConsoleKey.S, ConsoleKey.DownArrow} },
				{'+' , new ConsoleKey[] {ConsoleKey.Add } },
				{'-' , new ConsoleKey[] {ConsoleKey.Subtract } }

            };
			public static List<int> AcceptedInputKeys() {
				List<int> l = Enumerable.Range(48, 10).ToList(); // szamok
				l.AddRange(Enumerable.Range(96, 10).ToList()); // numpad
				l.AddRange(Enumerable.Range(65, 26).ToList()); // betuk
				l.Add(189);
				l.Add(109); // 2 fele kotojel
				return l;
			}
        }
		public static ConsoleKey[] MenuExit = new ConsoleKey[] { ConsoleKey.Escape, ConsoleKey.Backspace };
		
		public static Dictionary<string, ConsoleColor> tileColors = new Dictionary<string, ConsoleColor>()
		{
			{ "ground", ConsoleColor.Yellow },
			{ "wall", ConsoleColor.DarkYellow },
			{ "water", ConsoleColor.Blue },
			{ "lion", ConsoleColor.DarkRed },
			{ "zebra", ConsoleColor.Black },
		};
		public static Dictionary<string, string> tileTypes = new Dictionary<string, string>()
		{
			{"0", "ground" },
			{"1", "wall" },
			{"2", "water" },
			{"3", "lion" },
			{"4", "zebra" }
		};
		public static Dictionary<char, int[]> moveMatrixes = new Dictionary<char, int[]>()
		{
			{'L', new int[] {-1,0} },
			{'R', new int[] {1,0} },
			{'U', new int[] {0,-1} },
			{'D', new int[] {0,1} }
		};
        public static string GetTypeNumFromType(string type)
        {
			return tileTypes.Keys.Where(k => tileTypes[k] == type).First();
        }
    }
}
