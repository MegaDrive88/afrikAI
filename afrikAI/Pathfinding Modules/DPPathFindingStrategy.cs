using System.Numerics;

namespace afrikAI.Pathfinding_Modules
{
	public class DPPathFindingStrategy : IPathfindingStrategy
	{
		private void UpdateTiles(Tile[,] tiles, Tile startTile, Tile endTile) 
		{
			UpdateTileHelper(tiles, endTile);
			ResetTiles(tiles);
		}
		private void UpdateTileHelper(Tile[,] tiles, Tile Tile)
		{
			Tile leftTile = tiles[Tile.y, Tile.x];
		}
		private void ResetTiles(Tile[,] tiles)
		{
			foreach (Tile tile in tiles) tile.Calculated = true;

        }
        public Vector2[] GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile)
		{
			UpdateTiles(tiles, startTile, endTile);
			return new Vector2[] {new Vector2(1,1), new Vector2(2,2) };
		}
	}
}
