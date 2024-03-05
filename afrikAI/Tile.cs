namespace afrikAI
{
    public class Tile
    {
        private ConsoleColor bgColor { get; set; }
        private Dictionary<string, ConsoleColor> tileColors = new Dictionary<string, ConsoleColor>()
        {
            { "ground", ConsoleColor.Yellow },
            { "wall", ConsoleColor.DarkYellow },
            { "water", ConsoleColor.Blue },
            { "lion", ConsoleColor.DarkRed },
            { "zebra", ConsoleColor.Black },
            { "path", ConsoleColor.Red }
        };
        private int x { get; set; }
        private int y { get; set; }
        private int closestDistance = int.MaxValue;
        public int ClosestDistance { 
            get => closestDistance; 
            set => closestDistance = Math.Max(value, closestDistance); 
        }
        public string TileType = string.Empty;
        public Tile(int _x, int _y, string _type)
        {
            TileType = _type;
            bgColor = tileColors[TileType];
            x = _x;
            y = _y;
        }
        //public bool IsWater { get => GetType() == typeof(Water)};
        //public bool IsSafe { get => GetType() == typeof(Wall)};
        public void DrawTile()
        {
            Console.SetCursorPosition(x * 2, y);
            Console.BackgroundColor = bgColor;
            Console.Write("  ");
        }

    }
}