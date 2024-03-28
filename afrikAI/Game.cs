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
		public Game(int _width, int _height, TileGeneratorData tileGenData, string _pathFindingCountext)
		{
			pathfindingContext = new PathfindingContext(_pathFindingCountext);
			width = _width;
			height = _height;
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(width, height, tileGenData);
		}
		public Game(string fileName, string _pathfindingContext)
		{
			pathfindingContext = new PathfindingContext(_pathfindingContext);
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(fileName, ref width, ref height);
		}
		public void Start()
		{
			while (true)
			{
				Console.ResetColor();
				Console.Clear();
				tileManager.DrawTiles();
				int[][] input = inputHandler.GetGameInput(width, height);
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
		public void PathTest()
		{
			tileManager.DrawTiles();
			Thread.Sleep(5000);
			tileManager.DrawShortestPathToWater(pathfindingContext);
		}
	}
}
