using afrikAI.Pathfinding_Modules;
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
		public Game(TileGeneratorData tileGenData, string _pathFindingCountext)
		{
			pathfindingContext = new PathfindingContext(_pathFindingCountext);
			width = tileGenData.Width;
			height = tileGenData.Height;
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(width, height, tileGenData);
		}
		public Game(string filePath, string _pathfindingContext)
		{
			pathfindingContext = new PathfindingContext(_pathfindingContext);
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(filePath, ref width, ref height);
		}
		public void Start()
		{
            tileManager.DrawTiles();
            while (true)
			{
				int[][] input = inputHandler.GetGameInput(width, height);
				tileManager.SwapTiles(input);
                tileManager.DrawTiles();
                tileManager.DrawShortestPathToWater(pathfindingContext); 
				Console.SetCursorPosition(0, 7); // topot meg kell szamolni
			}
		}
	}
}
