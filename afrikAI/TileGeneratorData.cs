namespace afrikAI
{
	public class TileGeneratorData
	{
		public readonly int Water;
		public readonly int Wall;
		public readonly int Lion;
		public readonly int Width;
		public readonly int Height;
		public TileGeneratorData(int width, int height, int wall, int lion, int water)
		{

			Width = width;
			Height = height;
			Wall = wall;
			Lion = lion;
			Water = water;
		}
	}
}
