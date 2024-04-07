using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        List<Vector2> vehtlok = new List<Vector2>();
        private static HashSet<HashSet<Tile>> paths = new HashSet<HashSet<Tile>>();
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref HashSet<Tile> path) {
            List<int[]> surrounding = new List<int[]> {
                new[] { start.x, start.y - 1 },
                new[] { start.x - 1, start.y },
                new[] { start.x, start.y + 1 },
                new[] { start.x + 1, start.y }
            };
            List<int[]> temp = new List<int[]>(surrounding);
            foreach (int[] s in temp) {
                try {
                    _ = tiles[s[1], s[0]];
                }
                catch {
                    surrounding.Remove(s);
                }
            }
            if (surrounding.Find(x => tiles[x[1], x[0]].TileType == "water") is not null) surrounding[0] = surrounding.Find(x => tiles[x[1], x[0]].TileType == "water");
            
            //ennel kicsit komplexebb sortolas kell xd
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
            int shortestPossible = 1 + (startTile.x > endTile.x ? startTile.x - endTile.x : endTile.x - startTile.x) + (startTile.y > endTile.y ? startTile.y - endTile.y : endTile.y - startTile.y);
            paths = new HashSet<HashSet<Tile>>() { paths.Where(x => x.Count >= shortestPossible).First() };
            foreach (Tile t in paths.First()) { 
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(shortestLength, vehtlok);
        }
    }
}
