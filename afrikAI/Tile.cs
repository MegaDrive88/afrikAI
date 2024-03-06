namespace afrikAI
{
    public class Tile
    {
        private ConsoleColor bgColor;
        public int x { get;private set; }
        public int y { get;private set; }
        private int closestDistance = int.MaxValue;
        public int ClosestDistance { 
            get => closestDistance; 
            set => closestDistance = Math.Max(0, Math.Min(value, closestDistance)); 
        }
        public bool Calculated;
        public string TileType { 
            get => TileType;
            set
            {
                // handle updating here???
                TileType = value;
                bgColor = Statics.tileColors[value];
            }
        }
        public Tile(int _x, int _y, string _type)
        {
            TileType = _type;
            bgColor = Statics.tileColors[TileType];
            x = _x;
            y = _y;
        }
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bgColor;
            Console.Write(" ");
        }

    }
}