using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  class Map
  {
    private List<Cell> cells;
    private int width, height;
    private bool start, finish;
    private bool mapSettingsSet;

    public Map()
    {
      cells = new List<Cell>();
      mapSettingsSet = false;

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          cells.Add(new Cell(CellType.NORMAL, -1));
        }
      }

      start = false;
      finish = false;
    }

    //Print all cells of the map
    public void PrintGrid()
    {
      Console.WriteLine("Printing the map");
      int[] nums = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      string mod = "";
      int i = 0;
      foreach (Cell c in Cells)
      {
        mod = "";
        foreach(int j in nums)
        {
          if (j == c.Cost)
          {
            mod = " ";
          }
        }


        if (c.Type == CellType.WALL)
        {
          Console.Write("  W ");
        }
        else if (c.Type == CellType.START)
        {
          Console.Write("  S ");
        }
        else if (c.Type == CellType.END)
        {
          Console.Write("  E ");
        }
        else
        {
          Console.Write(" " + mod + c.Cost + " ");
        }

        if (i == width-1)
        {
          Console.WriteLine("");
          i = 0;
        }
        else
          i++;
      }
      Console.WriteLine("");
    }

    //Print all cells of the map
    public void PrintCellID()
    {
      Console.WriteLine("Printing the map");
      int i = 0;
      foreach (Cell c in Cells)
      {
        Console.Write(" " + cells.IndexOf(c) + " ");

        if (i == width - 1)
        {
          Console.WriteLine("");
          i = 0;
        }
        else
          i++;
      }
      Console.WriteLine("");
    }


    public void PrintLine()
    {
      Console.WriteLine("Printing the map");
      foreach (Cell c in Cells)
      {
        Console.WriteLine(Cells.IndexOf(c) + ": " + c.Type);
      }
    }

    public void DrawMap()
    {
      int x = 0;
      int y = 0;
      int w = 5;
      int h = 5;
      int pad = 2;
      int count = 0;
      foreach (Cell c in Cells)
      {
        SwinGame.DrawRectangle(Color.Black,x,y,w,h);
        SwinGame.DrawText(Cells.IndexOf(c).ToString(), Color.Black, x+pad, y+pad);

        if (count == width - 1)
        {
          x = 0;
          y += h;
          count = 0;
        }
        else
        {
          count++;
          x += w;
        }
      }
    }

    public List<int> ReadLineForInt(string line)
    {
      List<int> result = new List<int>();

      string[] numbers = Regex.Split(line, @"\D+");
      foreach (string value in numbers)
      {
        if (!string.IsNullOrEmpty(value))
        {
          int i = int.Parse(value);
          result.Add(i);
        }
      }

      return result;
    }

    public void Add(string line)
    {
      if (line.Substring(0,1) == "[" && !mapSettingsSet)
      {
        width = ReadLineForInt(line)[1];
        height = ReadLineForInt(line)[0];
        mapSettingsSet = true;
      }
      else if (line.Substring(0,1) == "(")
      {
        if (!start)
        {
          List<int> t = ReadLineForInt(line);
          cells[t[0] * height + (t[1] * width)].Type = CellType.START;
          start = true;
        }
        else if (!finish)
        {
          List<int> t = ReadLineForInt(line);
          cells[t[0] + (t[1] * width)].Type = CellType.END;
          finish = true;
        }
        else
        {
          List<int> t = ReadLineForInt(line);

          for (int i = 0; i < t[2]; i++)
          {
            for (int j = 0; j < t[3]; j++)
            {
              int tmp = (t[0] + t[1] * width) + i + (j * width);
              cells[tmp].Type = CellType.WALL;
            }
          }
        }
      }
    }

    public void SetCost(int costStep)
    {
      int EndID = EndCell;

      //can get adjcent cells to the END cell
      //then loop through those cell
      //and continue to loop until all cells have a cost that is not -1

      //make a list of all cells that are encountered
      //then go through the list and number each based on the number of steps from the START of the loop (END point)
      //add any new cells to the END of the list
      //remove any cell that has been assigned.

      List<int> frontier = new List<int>();
      foreach(Node c in GetAdjacentCells(EndID))
      {
        frontier.Add(c.ID);
      }

      bool quit = false;
      int step = 0;
      while (!quit)
      {
        quit = true;

        foreach(int i in frontier)
        {
          Cells[i].Cost = (step + 1)*costStep;
        }
        step++;

        List<int> TMPfrontier = new List<int>();
        foreach (int i in frontier)
        {
          foreach (Node c in GetAdjacentCells(i))
          {
            if (Cells[c.ID].Cost < 1 && Cells[c.ID].Type != CellType.END)
            {
              TMPfrontier.Add(c.ID);
            }
          }
        }
        frontier = TMPfrontier;

        //Console.WriteLine("Current frontier ");
        //foreach (int i in frontier)
        //{
        //  Console.Write(i + ", ");
        //}
        //Console.WriteLine("");


        foreach (Cell c in Cells)
        {
          if (c.Cost == -1)
          {
            quit = false;
            break;
          }
        }

        if (frontier.Count < 1)
          quit = true;

      }
    }

    public bool CellNotWall(int CellID)
    {
      if (CellID >= 0 && CellID < width * height) //check if CellID is with in map size;
      {
        if (Cells[CellID].Type != CellType.WALL)
        {
          return true;
        }
      }
      return false;
    }

    public List<Node> GetAdjacentCells(int CellID)
    {
      List<Node> cd = new List<Node>();

      if (CellID >= 0 && CellID < width*height) //check if CellID is with in map size;
      {

        if (CellID % width == width - 1) //check if on right side of map
        {
          if (CellNotWall(CellID-width))
            cd.Add(new Node(CellID - width, Action.UP)); //Up

          if (CellNotWall(CellID + width))
            cd.Add(new Node(CellID + width, Action.DOWN)); //Down

          if (CellNotWall(CellID - 1))
            cd.Add(new Node(CellID - 1, Action.LEFT)); //Left

        }
        else if (CellID % width == 0) //check if on left side of map
        {
          if (CellNotWall(CellID - width))
            cd.Add(new Node(CellID - width, Action.UP)); //Up

          if (CellNotWall(CellID + 1))
            cd.Add(new Node(CellID + 1, Action.RIGHT)); //Right

          if (CellNotWall(CellID + width))
            cd.Add(new Node(CellID + width, Action.DOWN)); //Down

        }
        else //the cellID is somewhere in the map
        {
          if (CellNotWall(CellID - width))
            cd.Add(new Node(CellID - width, Action.UP)); //Up

          if (CellNotWall(CellID + 1))
            cd.Add(new Node(CellID + 1, Action.RIGHT)); //Right

          if (CellNotWall(CellID + width))
            cd.Add(new Node(CellID + width, Action.DOWN)); //Down

          if (CellNotWall(CellID - 1))
            cd.Add(new Node(CellID - 1, Action.LEFT)); //Left
        }
      }

      return cd;
    }

    public List<Cell> Cells
    {
      get { return cells; }
    }

    public int Width
    {
      get { return width; }
    }

    public int Height
    {
      get { return height; }
    }

    public int StartCell
    {
      get
      {
        foreach(Cell c in Cells)
        {
          if (c.Type == CellType.START)
            return Cells.IndexOf(c);
        }
        return 0;
      }
    }

    public int EndCell
    {
      get
      {
        foreach (Cell c in Cells)
        {
          if (c.Type == CellType.END)
            return Cells.IndexOf(c);
        }
        return 0;
      }
    }

  }
}
