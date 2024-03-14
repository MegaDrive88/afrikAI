namespace afrikAI
{
	public class TileGeneratorData
	{
		public readonly int Wall;
		public readonly int Lion;
		public readonly int Width;
		public readonly int Height;
		public TileGeneratorData(int width, int height, int wall, int lion)
		{
			Width = width;
			Height = height;
			Wall = wall;
			Lion = lion;
		}
	}
}
