using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public interface IPathfindingStrategy
	{
		public List<Vector2> GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile);
	}
}
