using System.Diagnostics;
using System.Numerics;
using System.Security;

namespace afrikAI.Pathfinding_Modules
{
	public class DPPathFindingStrategy : IPathfindingStrategy
	{
		private int shortestLength = 0;
        private void UpdateTiles(Tile[,] tiles, Tile endTile)
        {
            endTile.ClosestDistance = 0;
            int width = tiles.GetLength(1);
            int height = tiles.GetLength(0);
            Queue<Tile> toUpdate = new Queue<Tile>();
            toUpdate.Enqueue(endTile);
            while (toUpdate.Count > 0)
            {
                Tile currTile = toUpdate.Dequeue();
                List<Tile> nextTiles = getNextTiles(tiles, currTile, width, height);
                foreach (Tile tile in nextTiles)
                {
                    if (!tile.Calculated && tile.TileType != "wall")
					{
						tile.Calculated = true;
                        toUpdate.Enqueue(tile);
						tile.ClosestDistance = currTile.ClosestDistance + 1;
                    }  
                }
            }
            ResetTiles(tiles);
        }
        private List<Tile> getNextTiles(Tile[,] tiles, Tile Tile, int width, int height)
		{
			List<Tile> nextTiles = new List<Tile>();
			if (Tile.x > 0) nextTiles.Add(tiles[Tile.y, Tile.x - 1]);
			if (Tile.x < width - 1) nextTiles.Add(tiles[Tile.y, Tile.x + 1]);
			if (Tile.y > 0) nextTiles.Add(tiles[Tile.y - 1, Tile.x]);
			if (Tile.y < height - 1) nextTiles.Add(tiles[Tile.y + 1, Tile.x]);
			return nextTiles;
		}
		private void ClosestTileHelper(Tile[,] tiles, Tile tile, int width, int height, ref List<Vector2> path)
		{

			// if path is empty list than no path has been found.
			List<Tile> nextTiles = getNextTiles(tiles, tile, width, height);
			Tile? nextTile = null;
			foreach (Tile t in nextTiles)
			{
				if(nextTile == null && t.TileType != "wall")
				{
					nextTile = t;
				}
				else if(nextTile != null && t.ClosestDistance < nextTile.ClosestDistance)
				{
					nextTile = t;
				}
			}
			if(nextTile == null)
			{
				path = new List<Vector2>();
				return;
			}
			path.Add(new Vector2(tile.x, tile.y));
			shortestLength++;
			
			
			
			if (nextTile.ClosestDistance == 0) 
			{
				shortestLength--;
				path.Add(new Vector2(nextTile.x, nextTile.y));
			} // nextTile == endTile;
			else if (shortestLength > width * height / 2)
			{
				path = new List<Vector2>();
				return;
			}
			else ClosestTileHelper(tiles, nextTile, width, height, ref path);
		}
		private void ResetTiles(Tile[,] tiles)
		{
			foreach (Tile tile in tiles) tile.Calculated = false;
        }
        public TilePath? GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile)
		{
			foreach(Tile tile in tiles) tile.ResetDistance(); 
			shortestLength = 0;
			List<Vector2> path = new List<Vector2>();
			UpdateTiles(tiles, endTile);
			ClosestTileHelper(tiles, startTile, tiles.GetLength(1), tiles.GetLength(0), ref path);
			if(path.Count > 0) return new TilePath(shortestLength, path);
			else return null;
		}
	}
}
