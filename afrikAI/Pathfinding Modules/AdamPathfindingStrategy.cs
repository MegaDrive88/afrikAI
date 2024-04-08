using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        List<Vector2> vehtlok = new List<Vector2>();
        private static int totalRecursions = 0;
        private static HashSet<HashSet<Tile>> paths = new HashSet<HashSet<Tile>>();
        private List<int[]> getSurrounding(Tile tile) {
            return new List<int[]> {
                new[] { tile.x, tile.y - 1 },
                new[] { tile.x - 1, tile.y },
                new[] { tile.x, tile.y + 1 },
                new[] { tile.x + 1, tile.y }
            };
        }
        // private int DistanceHelper(){
        // }
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal, ref HashSet<Tile> path) { // ha nincs ut?
            List<int[]> surrounding = getSurrounding(start);
            foreach (int[] coords in new List<int[]>(surrounding)) {
                try {
                    if (new[] { "wall", "lion" }.Contains(tiles[coords[1], coords[0]].TileType) || tiles[coords[1], coords[0]].Calculated) throw new Exception("not ground");
                    //tiles[coords[1], coords[0]].ClosestDistance = ; // ez a lenyeg!!!!!!!!!!!!!!!!!!!!!!
                }
                catch {
                    surrounding.Remove(coords);
                }
            }
            surrounding = surrounding.OrderBy(x => tiles[x[1], x[0]].ClosestDistance).ToList();
            if (surrounding.Find(x => tiles[x[1], x[0]].TileType == goal.TileType) is not null) surrounding[0] = surrounding.Find(x => tiles[x[1], x[0]].TileType == goal.TileType);
            int[] s = surrounding[0];
            Tile curr = tiles[s[1], s[0]];
            if (curr.TileType == goal.TileType) {
                path.Add(curr);
                paths.Add(path);
                path = new HashSet<Tile>();
                return;
            }
            else if (curr.TileType == "ground") {
                curr.Calculated = true;
                path.Add(curr);
                MagicHappensHere(tiles, curr, goal, ref path);
            }
            //paths.Add(path); kell?
        }
        public TilePath? GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            HashSet<Tile> sPath = new HashSet<Tile>();
            MagicHappensHere(tiles, startTile, endTile, ref sPath);
            int shortestPossible = (startTile.x > endTile.x ? startTile.x - endTile.x : endTile.x - startTile.x) 
                                 + (startTile.y > endTile.y ? startTile.y - endTile.y : endTile.y - startTile.y);
            paths = new HashSet<HashSet<Tile>>() { paths.Where(x => x.Count >= shortestPossible).First() }; // szar az egesz
            foreach (Tile t in paths.First()) { // refactor
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(shortestLength, vehtlok);
        }
    }
}
