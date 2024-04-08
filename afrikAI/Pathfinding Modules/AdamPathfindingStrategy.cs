using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        List<Vector2> vehtlok = new List<Vector2>();
        private static HashSet<HashSet<Tile>> paths = new HashSet<HashSet<Tile>>();
        private int DistanceHelper(Tile[,] tiles, int[] t1, int[] t2) {
            int wallcount = 0;
            for(int i = t1[1] < t2[1] ? t1[1] : t2[1]; i <= (t1[1] < t2[1] ? t2[1] : t1[1]); i++) {
                for (int j = t1[0] < t2[0] ? t1[0] : t2[0]; j <= (t1[0] < t2[0] ? t2[0] : t1[0]); j++) {
                    if (tiles[i, j].TileType == "wall") wallcount++;
                }
            }
            return wallcount * 100 / (((t1[1] < t2[1] ? t2[1] - t1[1] : t1[1] - t2[1]) + 1) * ((t1[0] < t2[0] ? t2[0] - t1[0] : t1[0] - t2[0]) + 1));   
        }
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref HashSet<Tile> path) {
            List<int[]> surrounding = new List<int[]> {
                new[] { start.x, start.y - 1 },
                new[] { start.x - 1, start.y },
                new[] { start.x, start.y + 1 },
                new[] { start.x + 1, start.y }
            };
            foreach (int[] s in new List<int[]>(surrounding)) {
                try {
                    _ = tiles[s[1], s[0]];
                    tiles[s[1], s[0]].ClosestDistance = DistanceHelper(tiles, s, new[] { goal.x, goal.y });
                }
                catch {
                    surrounding.Remove(s);
                }
            }
            surrounding = surrounding.OrderBy(x => tiles[x[1], x[0]].ClosestDistance).ToList();
            if (surrounding.Find(x => tiles[x[1], x[0]].TileType == "water") is not null) surrounding[0] = surrounding.Find(x => tiles[x[1], x[0]].TileType == "water");
            foreach (int[] s in surrounding) {
                Tile curr = tiles[s[1], s[0]];
                if (curr.Calculated || new[] { "wall", "lion" }.Contains(curr.TileType)) {
                    curr.Calculated = true;
                    continue;
                }
                else if (curr.TileType == "water") {
                    path.Add(curr);
                    paths.Add(path);
                    path = new HashSet<Tile>();
                }
                else if (curr.TileType == "ground") {
                    path.Add(curr);
                    curr.Calculated = true;
                    MagicHappensHere(tiles, curr, goal, ref path);
                }
            }
            paths.Add(path);            
        }
        public TilePath GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            HashSet<Tile> sPath = new HashSet<Tile>();
            MagicHappensHere(tiles, startTile, endTile, ref sPath);
            int shortestPossible = (startTile.x > endTile.x ? startTile.x - endTile.x : endTile.x - startTile.x) 
                                 + (startTile.y > endTile.y ? startTile.y - endTile.y : endTile.y - startTile.y);
            paths = new HashSet<HashSet<Tile>>() { paths.Where(x => x.Count >= shortestPossible).First() }; // min?
            foreach (Tile t in paths.First()) { 
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(shortestLength, vehtlok);
        }
    }
}
