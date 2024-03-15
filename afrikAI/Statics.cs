namespace afrikAI
{
	public static class Statics
	{
		public static class KeyBinds
		{
			public static ConsoleKey[] MenuConfirm = new ConsoleKey[] { ConsoleKey.Enter, ConsoleKey.Spacebar };
			public static List<int> AcceptedInputKeys() {
				List<int> l = Enumerable.Range(48, 10).ToList(); // szamok
				l.AddRange(Enumerable.Range(96, 10).ToList()); // numpad
				l.AddRange(Enumerable.Range(65, 26).ToList()); // betuk
				l.Add(189);
				l.Add(109); // 2 fele kotojel
				return l;
			}
        }
	}
}
