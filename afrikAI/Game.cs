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

		private List<Tile> lions;
		public Game(TileGeneratorData tileGenData, string _pathFindingCountext)
		{
			pathfindingContext = new PathfindingContext(_pathFindingCountext);
			width = tileGenData.Width;
			height = tileGenData.Height;
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(width, height, tileGenData);
		}
		public Game(string fileName, string _pathfindingContext)
		{
			pathfindingContext = new PathfindingContext(_pathfindingContext);
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(fileName, ref width, ref height);
		}
		public void StartOld()
		{
            tileManager.DrawTiles(); // ne az incomingot accepteld!!!
            while (true)
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
			
			lions = tileManager.GetLions();
			while (true)
			{
				Console.ResetColor();
				Console.Clear();
				tileManager.DrawTiles();
				int[][] input = inputHandler.GetGameInput(width, height);
				tileManager.SwapTiles(input);
				moveLions();
				lions = tileManager.GetLions();
				tileManager.MoveCloserToWater(pathfindingContext);
			}
		}
		public void PathTest()
		{
			tileManager.DrawTiles();
			Thread.Sleep(5000);
			tileManager.DrawShortestPathToWater(pathfindingContext);
		}
		private void moveLions()
		{
			Tile zebra = tileManager.getZebra();
            foreach (Tile lion in lions)
            {
				tileManager.MoveCloserToTile(pathfindingContext, lion, zebra);
            }
        }

	}
}
