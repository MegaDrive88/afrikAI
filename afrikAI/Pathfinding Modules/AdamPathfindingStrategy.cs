using System.Numerics;

namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        List<Vector2> vehtlok = new List<Vector2>();
        //private static int cOUNTER = 0;
        private static List<List<Tile>> paths = new List<List<Tile>>();
        private static bool found;
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal) {
            int left = start.x, right = left, top = start.y, bottom = top;
            List<int[]> surrounding = new List<int[]> {
                new[] { start.x, start.y - 1 },
                new[] { start.x - 1, start.y },
                new[] { start.x, start.y + 1 },
                new[] { start.x + 1, start.y }
            };
            List<Tile> path = new List<Tile> { start };
            bool found = start.x == goal.x && start.y == goal.y;
            while (!found && left != 0 && top != 0 && right != tiles.GetLength(1) && bottom != tiles.GetLength(0)) {

                //found = start.x == goal.x && start.y == goal.y;

                foreach (int[] s in surrounding) {
                    if (new[] { "wall", "lion" }.Contains(tiles[s[1], s[0]].TileType)) {
                        tiles[s[1], s[0]].Calculated = true;
                        //break; // return?
                    }
                    else if (tiles[s[1], s[0]].TileType == "water") {
                        //GG
                    }
                    else if (tiles[s[1], s[0]].TileType == "ground") {

                        path.Add(tiles[s[1], s[0]]); // kulon path minden groundnak!!
                        //cOUNTER++;
                        //MagicHappensHere(tiles, tiles[s[1], s[0]], goal); - rekurzio
                    }
                    tiles[s[1], s[0]].Calculated = true;
                }
                paths.Add(path);
                shortestLength = paths.Min(x => x.Count);
            }
            







            //for (int i = top; i < tiles.GetLength(0); i++) {
            //    for(int j = left; j < tiles.GetLength(1); j++) {

            //        //bool toTheRight = goal.x > right;
            //        //bool toTheTop = goal.y > top;
            //    }
            //}
            //private void drawPath(TilePath path) {
            //    foreach (Vector2 pos in path.Path) {
            //        tiles[(int)pos.Y, (int)pos.X].Draw(ConsoleColor.Red);
            //    }
            //}



            //while ((tiles[up, right].TileType != "wall" && tiles[up, left].TileType != "wall") || (tiles[up, right].TileType != "water" && tiles[up, left].TileType != "water")) {
            //    if (toTheRight) right++; // goofy ahh condition ^
            //    else left++;
            //}
        }
        public TilePath GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            MagicHappensHere(tiles, startTile, endTile);
            paths = new List<List<Tile>>() { paths.Where(x => x.Count == shortestLength).ToList()[0] };
            foreach (Tile t in paths[0]) { // kirajzolas mukodik
                vehtlok.Add(new Vector2(t.x, t.y));
            }
            return new TilePath(shortestLength, vehtlok);
        }
    }
}
