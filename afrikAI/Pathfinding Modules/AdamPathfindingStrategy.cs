namespace afrikAI.Pathfinding_Modules {
    public class AdamPathfindingStrategy : IPathfindingStrategy {
        private int shortestLength = 0;
        //private Dictionary<int, List<int>> DirectionSwitch = new Dictionary<int, List<int>> { };
        private void MagicHappensHere(Tile[,] tiles, Tile start, Tile goal) {
            //int left = start.x, right = left, up = start.y, down = up;
            //while (left != 0 && up != 0 && right != tiles.GetLength(1) && down != tiles.GetLength(0)) {
                // will cook later.
            //}
        }
        public TilePath GetShortestPath(Tile[,] tiles, Tile startTile, Tile endTile) {
            return new(shortestLength, new());
        }
        //private class Direction { }
    }
}
