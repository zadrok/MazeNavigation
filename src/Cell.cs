using System;

namespace MazeNavigation
{
  class Cell
  {
    private CellType type;
    private int cost;

    public Cell(CellType inType, int inCost)
    {
      Type = inType;
      Cost = inCost;
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
  }
}
