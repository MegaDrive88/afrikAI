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
        private int closest;
        public int Closest { 
            get => closest; 
            set => closest = Math.Max(value, closest); 
        }
        //public bool IsSafe { get => GetType() == hazard
        public void DrawTile()
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bgColor;
            Console.Write(" ");
        }

    }
}