using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  class Cell
  {
    private CellType type;
    private Color color;
    private int cost;

    public Cell(CellType aType, int aCost, Color aColor)
    {
      Type = aType;
      Cost = aCost;
      Color = aColor;
    }

    public CellType Type
    {
      get { return type; }
      set { type = value; }
    }

    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }
  }
}
