using System.Linq;
using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        List<Vector2> vehtlok = new List<Vector2>();
        private static int totalRecursions = 0;
        private static HashSet<Tile> finalPath = new HashSet<Tile>();
        //private static Tile[,] tiles;
        private List<int[]> getSurrounding(Tile[,] tiles, Tile tile) {
            List<int[]> s = new List<int[]>();
            if (tile.y > 0) s.Add(new[] { tile.x, tile.y - 1 });
            if (tile.x > 0) s.Add(new[] { tile.x - 1, tile.y });
            if (tile.y < tiles.GetLength(0) - 1) s.Add(new[] { tile.x, tile.y + 1 });
            if (tile.x < tiles.GetLength(1) - 1) s.Add(new[] { tile.x + 1, tile.y });
            return s;
        }
        private int distanceHelper(Tile[,] tiles, Tile t) {
            List<int[]> surrounding = getSurrounding(tiles, t);
            return t.ClosestDistance = surrounding.Min(x => tiles[x[1], x[0]].ClosestDistance) + 1;

            //foreach (int[] s in new List<int[]>(surrounding)) {

            //    Tile curr = tiles[s[1], s[0]];
            //    if (new[] { "wall", "lion" }.Contains(curr.TileType)) {
            //        curr.Calculated = true;
            //        continue;
            //    }
            //    curr.ClosestDistance = distanceHelper(tiles, t);
            //}
            //return t.ClosestDistance = getSurrounding(tiles, t).Min(x => tiles[x[1], x[0]].ClosestDistance) + 1;

        }
        private void initialize(Tile[,] tiles, Tile goal) {
            goal.ClosestDistance = 0;
            goal.Calculated = true;
            for (int i = 0; i < tiles.GetLength(0); i++) {
                for (int j = 0; j < tiles.GetLength(1); j++) {
                    if (j == 5) {

                    }
                    Tile curr = tiles[i, j];
                    if (curr.TileType == "ground") curr.ClosestDistance = distanceHelper(tiles, curr);
                    else if (j == 0) curr.ClosestDistance = Math.Abs(goal.y - i) + goal.x;
                    else if (new[] { "wall", "lion" }.Contains(curr.TileType)) curr.ClosestDistance = 
                            getSurrounding(tiles, curr).Max(x => tiles[x[1], x[0]].ClosestDistance == int.MaxValue / 2 ? -1 : tiles[x[1], x[0]].ClosestDistance) + 1
                            // + getSurrounding(tiles, curr).Count(x => tiles[x[1], x[0]].TileType == "wall");
                    curr.Draw();
                }
            }
        }
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref HashSet<Tile> path) { // ha nincs ut?
            List<int[]> surrounding = getSurrounding(tiles, start);
            surrounding = surrounding.OrderBy(x => tiles[x[1], x[0]].ClosestDistance).ToList();
            //if (surrounding.Find(x => tiles[x[1], x[0]].TileType == goal.TileType) is not null) surrounding[0] = surrounding.Find(x => tiles[x[1], x[0]].TileType == goal.TileType);
            int[] s = surrounding[0];
            Tile curr = tiles[s[1], s[0]];
            if (curr.TileType == goal.TileType) {
                path.Add(curr);
                finalPath = path;
                path = new HashSet<Tile>();
                return;
            }
            else if (curr.TileType == "ground") {
                curr.Calculated = true;
                path.Add(curr);
                MagicHappensHere(tiles, curr, goal, ref path);
            }
        }
        public TilePath? GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            HashSet<Tile> sPath = new HashSet<Tile>();
            initialize(tiles, endTile);
            
            //MagicHappensHere(tiles, startTile, endTile, ref sPath);
            foreach (Tile t in finalPath) { 
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(shortestLength, vehtlok);
        }
    }
}
