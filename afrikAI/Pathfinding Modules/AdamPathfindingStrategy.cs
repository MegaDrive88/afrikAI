using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        List<Vector2> vehtlok = new List<Vector2>();
        private static List<HashSet<Tile>> paths = new List<HashSet<Tile>>();
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref HashSet<Tile> path) {
            //Tile zebra;
            //foreach (Tile tile in tiles) if (tile.TileType == "zebra") zebra = stile;
            //lassu
            List<int[]> surrounding = new List<int[]> {
                new[] { start.x, start.y - 1 },
                new[] { start.x - 1, start.y },
                new[] { start.x, start.y + 1 },
                new[] { start.x + 1, start.y }
            };
            //if (!path.All(x => x.TileType != "water")) return;
            foreach (int[] s in surrounding) {
                try {
                    _ = tiles[s[1], s[0]];
                }
                catch {
                    continue; // szélén van az adott tile a mapnak
                }
                Tile curr = tiles[s[1], s[0]];
                // eddig jo

                if (curr.Calculated) continue;
                if (new[] { "wall", "lion" }.Contains(curr.TileType)) {
                    curr.Calculated = true;
                    continue;
                    //break;//return;
                }
                else if (curr.TileType == "water") {
                    path.Add(curr);
                    paths.Add(path);
                    shortestLength = paths.Min(x => x.Count);
                    //path = new List<Tile>();
                    //foreach (Tile t in tiles) t.Calculated = false;
                    //curr.Calculated = true;
                    return; // GG
                }
                else if (curr.TileType == "ground") {
                    // ez az ag reworkot igenyel de nagyon

                    // if hashset hossza nagyobb mint width + height - startx - starty : szopo

                    path.Add(curr);
                    MagicHappensHere(tiles, curr, goal, ref path); // -rekurzio
                    //path = new List<Tile>();
                    foreach (Tile t in tiles) t.Calculated = false;
                    if (paths.Count != 1) return;
                    //if (paths.Contains(path)) {
                    //    path = new List<Tile>();
                    //    foreach (Tile t in tiles) t.Calculated = false;
                    //}
                    //if (!path.All(x => x.TileType != "water")) return;
                }
            }
            paths.Add(path);
            shortestLength = paths.Min(x => x.Count);
            

        }
        public TilePath GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            HashSet<Tile> sPath = new HashSet<Tile>();
            MagicHappensHere(tiles, startTile, endTile, ref sPath);
            paths = new List<HashSet<Tile>>() { paths.Where(x => x.Count == shortestLength).ToList()[0] };
            foreach (Tile t in paths[0]) { // kirajzolas mukodik
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(shortestLength, vehtlok);
        }
    }
}
