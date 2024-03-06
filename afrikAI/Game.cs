using afrikAI.Pathfinding_Modules;

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
		public Game(string filePath, string _pathfindingContext)
		{
			pathfindingContext = new PathfindingContext(_pathfindingContext);
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(filePath, ref width, ref height);
		}

	}
}
