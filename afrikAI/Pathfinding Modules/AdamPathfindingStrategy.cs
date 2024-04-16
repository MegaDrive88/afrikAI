using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private List<Vector2> finalPath = new List<Vector2>();
        private bool initNeeded = true;
        private int totalRecursions = 0;
        private int groundCount = 0;
        private List<Tile> getSurrounding(Tile[,] tiles, Tile tile) {
            List<Tile> s = new List<Tile>();
            if (tile.y > 0) s.Add(tiles[tile.y - 1, tile.x]);
            if (tile.x > 0) s.Add(tiles[tile.y, tile.x - 1]);
            if (tile.y < tiles.GetLength(0) - 1) s.Add(tiles[tile.y + 1, tile.x]);
            if (tile.x < tiles.GetLength(1) - 1) s.Add(tiles[tile.y, tile.x + 1]);
            return s;
        }
        private int distanceHelper(Tile[,] tiles, Tile t) {
            List<Tile> surrounding = getSurrounding(tiles, t);
            return t.ClosestDistance = surrounding.Min(x => x.ClosestDistance) + 1;
        }
        private void initialize(ref Tile[,] tiles) {
            initNeeded = false;
            for (int i = 0; i < tiles.GetLength(0); i++) {
                for (int j = 0;  j < tiles.GetLength(1); j++) {
                    Tile curr = tiles[i, j];
                    if (curr.TileType == "ground") {
                        if (getSurrounding(tiles, curr).Count(x => x.TileType == "wall") >= 3) {
                            curr.Calculated = true;
                            continue;
                        }
                        groundCount++;
                        curr.ClosestDistance = distanceHelper(tiles, curr);
                        curr.Calculated = curr.ClosestDistance != int.MaxValue / 2;
                        if (!curr.Calculated) initNeeded = true;
                    }
                    //curr.Draw(true);
                }
            }
        }
        private void magicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref List<Vector2> path) {
            if (totalRecursions > groundCount) return;
            List<Tile> surrounding = getSurrounding(tiles, start);
            Tile curr = surrounding.OrderBy(x => x.ClosestDistance).ToList()[0];
            if (curr.TileType == goal.TileType) {
                path.Add(new Vector2(curr.x, curr.y));
                return;
            }
            else if (curr.TileType == "ground") {
                path.Add(new Vector2(curr.x, curr.y));
                totalRecursions++;
                magicHappensHere(tiles, curr, goal, ref path);
            }
        }
        public TilePath? GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            foreach (Tile tile in tiles) tile.ResetDistance();
            endTile.ClosestDistance = 0;
            endTile.Calculated = true;
            finalPath = new List<Vector2> { new Vector2(startTile.x, startTile.y) };
            while (initNeeded && totalRecursions <= groundCount) {
                groundCount = 0;
                initialize(ref tiles);
                totalRecursions++;
            }
            totalRecursions = 0;
            magicHappensHere(tiles, startTile, endTile, ref finalPath);
            if (!finalPath.Contains(new Vector2(endTile.x, endTile.y))) return null;
            return new TilePath(finalPath.Count, finalPath);
        }
    }
}
