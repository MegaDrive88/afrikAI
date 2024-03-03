using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public interface IPathfindingStrategy
	{
		public Vector2[] GetShortestPath(List<Tile> tiles);
	}
}
