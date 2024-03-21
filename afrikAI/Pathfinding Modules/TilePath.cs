using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace afrikAI.Pathfinding_Modules
{
	public class TilePath
	{
		public readonly int Length;
		public readonly List<Vector2> Path;
		public TilePath(int length, List<Vector2> positions)
		{
			Length = length;
			Path = positions;
		}
	}
}
