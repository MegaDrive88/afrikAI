using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public class DPPathFindingStrategy : IPathfindingStrategy
	{
		private void UpdateTiles(List<Tile> tiles) { }
		public Vector2[] GetShortestPath(List<Tile> tiles)
		{
			UpdateTiles(tiles);
			return new Vector2[] {new Vector2(1,1), new Vector2(2,2) };
		}
	}
}
