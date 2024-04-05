using System.Diagnostics;
using System.Numerics;

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
                    if (!tile.Calculated)
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
			List<Tile> nextTiles = getNextTiles(tiles, tile, width, height);
			Tile nextTile = nextTiles.Where(t => t.ClosestDistance == nextTiles.Min(t => t.ClosestDistance)).First(); // only returns 1 in Future could make so it returns all paths.
			path.Add(new Vector2(tile.x, tile.y));
			if (nextTile.ClosestDistance == 0) 
			{
				Debug.WriteLine(nextTile.x + " " + nextTile.y);
				path.Add(new Vector2(nextTile.x, nextTile.y));
				shortestLength++;
			} // nextTile == endTile;
			else ClosestTileHelper(tiles, nextTile, width, height, ref path);
		}
		private void ResetTiles(Tile[,] tiles)
		{
			foreach (Tile tile in tiles) tile.Calculated = false;
        }
        public TilePath GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile)
		{
			foreach(Tile tile in tiles) tile.ResetDistance(); 
			shortestLength = 0;
			List<Vector2> path = new List<Vector2>();
			for (int i = 0; i < 2; i++) UpdateTiles(tiles, endTile); // works? MAY NEED TO REDO!!!
			ClosestTileHelper(tiles, startTile, tiles.GetLength(1), tiles.GetLength(0), ref path);
			return new TilePath(shortestLength, path);
		}
	}
}
