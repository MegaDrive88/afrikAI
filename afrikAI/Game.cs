using afrikAI.Pathfinding_Modules;
using System.Diagnostics;
using System.Numerics;

namespace afrikAI
{
	public class Game
	{
		private int width;
		private int height;

		private readonly InputHandler inputHandler;
		private readonly PathfindingContext pathfindingContext;
		private readonly TileManager tileManager;

		public bool isRunning;
		private List<Tile> lions;
		public Game(TileGeneratorData tileGenData, string _pathFindingCountext)
		{
			pathfindingContext = new PathfindingContext(_pathFindingCountext);
			width = tileGenData.Width;
			height = tileGenData.Height;
			inputHandler = new InputHandler();
			tileManager = new TileManager(width, height, tileGenData);
			isRunning = true;
		}
		public Game(string fileName, string _pathfindingContext)
		{
			pathfindingContext = new PathfindingContext(_pathfindingContext);
			inputHandler = new InputHandler();
			tileManager = new TileManager(fileName, ref width, ref height);
			isRunning = true;
		}
		public void StartOld()
		{
            tileManager.DrawTiles(); // ne az incomingot accepteld!!!
            while (isRunning)
			{
				Console.ResetColor();
				Console.Clear();
				tileManager.DrawTiles();
				int[][] input = inputHandler.GetGameInput(width, height, tileManager.GetInvalidTiles());
				tileManager.SwapTiles(input);
				tileManager.DrawTiles();
				Thread.Sleep(1000);
				tileManager.DrawShortestPathToWater(pathfindingContext);
				Console.ReadKey();
				tileManager.MoveCloserToWater(pathfindingContext);
				tileManager.DrawTiles();
				Console.ReadKey();
			}
		}
		public void Start()
		{
			
			while (isRunning)
			{
                lions = tileManager.GetLions();
                Console.ResetColor();
				Console.Clear();
				tileManager.DrawTiles();
				int[][] input = inputHandler.GetGameInput(width, height, tileManager.GetInvalidTiles());
                tileManager.SwapTiles(input);
                tileManager.MoveCloserToWater(pathfindingContext, this);
                moveLions();
			}
		}
		public void PathTest()
		{
			tileManager.DrawTiles();
			Thread.Sleep(5000);
			tileManager.DrawShortestPathToWater(pathfindingContext);
		}
		public void GameEnd(bool win)
		{
			isRunning = false;
			Console.Clear();
			if (!win) Console.WriteLine("   ______                                 ___                           _  \r\n .' ___  |                              .'   `.                        | | \r\n/ .'   \\_|  ,--.   _ .--..--.  .---.   /  .-.  \\ _   __  .---.  _ .--. | | \r\n| |   ____ `'_\\ : [ `.-. .-. |/ /__\\\\  | |   | |[ \\ [  ]/ /__\\\\[ `/'`\\]| | \r\n\\ `.___]  |// | |, | | | | | || \\__.,  \\  `-'  / \\ \\/ / | \\__., | |    |_| \r\n `._____.' \\'-;__/[___||__||__]'.__.'   `.___.'   \\__/   '.__.'[___]   (_) \r\n");
			else Console.WriteLine(" ____  ____                               _            _  \r\n|_  _||_  _|                             (_)          | | \r\n  \\ \\  / / .--.   __   _     _   _   __  __   _ .--.  | | \r\n   \\ \\/ // .'`\\ \\[  | | |   [ \\ [ \\ [  ][  | [ `.-. | | | \r\n   _|  |_| \\__. | | \\_/ |,   \\ \\/\\ \\/ /  | |  | | | | |_| \r\n  |______|'.__.'  '.__.'_/    \\__/\\__/  [___][___||__](_) \r\n");
			Console.ReadKey(true);
			Menu menu = new Menu();
		}
		private void moveLions()
		{
			Tile zebra = tileManager.GetZebra();
            foreach (Tile lion in lions) tileManager.MoveCloserToTile(pathfindingContext, lion, zebra, this);
        }

	}
}
