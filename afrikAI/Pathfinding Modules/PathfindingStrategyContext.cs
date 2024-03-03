using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public class PathfindingStrategyContext
	{
		private IPathfindingStrategy Strategy;
		public void ChangeStrategy(IPathfindingStrategy strategy)
		{
			Strategy = strategy;
		}

		
		public Vector2[] GetShortestPath(List<Tile> tiles)
		{
			return Strategy.GetShortestPath(tiles);
		}
	}
}
