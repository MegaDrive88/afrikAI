using System.Drawing;
using System.Numerics;

namespace afrikAI
{
    public class Tile : ICloneable
    {
        private ConsoleColor bgColor;
        public int x { get;private set; }
        public int y { get;private set; }
        private int closestDistance = int.MaxValue/2;
        public int ClosestDistance { 
            get => closestDistance; 
            set => closestDistance = Math.Max(0, Math.Min(value, closestDistance)); 
        }
        public bool Calculated;
        private string tileType;
        public string TileType { 
            get => tileType;
            set
            {
                // handle updating here???
                tileType = value;
                bgColor = Statics.tileColors[value];
                ResetDistance();
            }
        }
		/// <summary>
		/// type: 
		/// 0 - ground 
		/// 1 - wall
		/// 2 - water
        /// 3 - lion
        /// 4 - zebra
		/// </summary>
		public Tile(int _x, int _y, string _type)
        {
            TileType = _type;
            bgColor = Statics.tileColors[TileType];
            x = _x;
            y = _y;
        }
        public void Draw(bool debugMode = false)
        {
            Console.SetCursorPosition(x * 2, y);
            Console.BackgroundColor = bgColor;
            if (debugMode) {
                Console.ForegroundColor = ConsoleColor.Black;
                if (closestDistance.ToString().Length > 2) Console.Write("MX");
                else if (closestDistance.ToString().Length == 1) Console.Write($"0{closestDistance}");
                else Console.Write(closestDistance.ToString());
            }
            else {
                Console.Write("  ");
                Console.ResetColor();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        public void ResetDistance()
        {
            closestDistance = int.MaxValue/2;
        }
        public void Draw(ConsoleColor color)
        {
			Console.SetCursorPosition(x * 2, y);
			Console.BackgroundColor = color;
			Console.Write("  ");
      Console.ResetColor();

		}
        
        public void SetPos(int[] pos)
        {
            x = pos[0];
            y = pos[1];
        }

    }
}