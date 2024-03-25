using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public interface IPathfindingStrategy
	{
		public TilePath GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile);
	}
}
