using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  static class ExtensionMethods
  {
    private static bool debug;

    public static bool valueInRange(int value, int min, int max)
		{
			return (value >= min) && (value <= max);
		}

    public static bool rectOverlap(Rect A, Rect B)
		{
			bool xOverlap = valueInRange(A.x, B.x, B.x + B.width) || valueInRange(B.x, A.x, A.x + A.width);
			bool yOverlap = valueInRange(A.y, B.y, B.y + B.height) || valueInRange(B.y, A.y, A.y + A.height);

			return xOverlap && yOverlap;
		}

    public static bool Debug
    {
      get { return debug; }
      set { debug = value; }
    }

  }
}
