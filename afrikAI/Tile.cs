namespace afrikAI
{
    public class Tile
    {
        private ConsoleColor bgColor { get; set; }
        private Dictionary<Type, ConsoleColor> tileColors = new Dictionary<Type, ConsoleColor>()
        {
        };
        private int x { get; set; }
        private int y { get; set; }
        private int closestDistance = int.MaxValue;
        public int ClosestDistance { 
            get => closestDistance; 
            set => closestDistance = Math.Max(value, closestDistance); 
        }
        public Tile(int _x, int _y)
        {
            bgColor = tileColors[GetType()];
            x = _x;
            y = _y;
        }
        //public bool IsWater { get => GetType() == typeof(Water)};
        //public bool IsSafe { get => GetType() == typeof(Wall)};
        public void DrawTile()
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bgColor;
            Console.Write(" ");
        }

    }
}