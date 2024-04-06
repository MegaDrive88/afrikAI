using System.Diagnostics;
using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public class PathfindingContext
	{
		private Dictionary<string, IPathfindingStrategy> pathStrategys = new Dictionary<string, IPathfindingStrategy>() 
		{
			{"DP", new DPPathFindingStrategy()}
		};
		private IPathfindingStrategy Strategy;

		public PathfindingContext(string _strategy = "DP")
		{
			ChangeStrategy(_strategy);
		}
		public void ChangeStrategy(string strategy)
		{
			if (pathStrategys.ContainsKey(strategy)) Strategy = pathStrategys[strategy];
			else Debug.WriteLine($"No such strategy as {strategy} (PathfindingContext.cs)");
		}
		public TilePath? GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile)
		{
			return Strategy.GetShortestPath(tiles, startTile, endTile);
		}
	}
}
