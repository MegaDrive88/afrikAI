using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private List<Vector2> vehtlok = new List<Vector2>();
        private bool initNeeded = true;
        private int totalRecursions = 0;
        private HashSet<Tile> finalPath = new HashSet<Tile>();
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
        private void initialize(ref Tile[,] tiles, Tile goal) {
            initNeeded = false;
            for (int i = 0; i < tiles.GetLength(0); i++) {
                for (int j = 0;  j < tiles.GetLength(1); j++) {
                    Tile curr = tiles[i, j];
                    if (j == 0) curr.ClosestDistance = Math.Abs(goal.y - i) + goal.x;
                    else if (curr.TileType == "ground") {
                        groundCount++;
                        curr.ClosestDistance = distanceHelper(tiles, curr);
                        curr.Calculated = curr.ClosestDistance != int.MaxValue / 2;
                        if (!curr.Calculated) initNeeded = true;
                    }
                    //curr.Draw(true);
                }
            }
        }
        private void magicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref HashSet<Tile> path) {
            List<Tile> surrounding = getSurrounding(tiles, start);
            Tile curr = surrounding.OrderBy(x => x.ClosestDistance).ToList()[0];
            if (curr.TileType == goal.TileType) {
                path.Add(curr);
                return;
            }
            else if (curr.TileType == "ground") {
                path.Add(curr);
                magicHappensHere(tiles, curr, goal, ref path);
            }
        }
        public TilePath? GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            endTile.ClosestDistance = 0;
            endTile.Calculated = true;
            while (initNeeded && totalRecursions <= groundCount) {
                groundCount = 0;
                initialize(ref tiles, endTile);
                totalRecursions++;
            }
            magicHappensHere(tiles, startTile, endTile, ref finalPath);
            if (!finalPath.Contains(endTile)) return null;
            foreach (Tile t in finalPath) { 
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(vehtlok.Count, vehtlok);
        }
    }
}
