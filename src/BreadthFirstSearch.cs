using System;
using System.Collections.Generic;

namespace MazeNavigation
{

  //Expand all options one level at a time
  class BreadthFirstSearch
  {
    public List<Node> Frontier;
    public List<Node> FrontierNext;
    public List<Node> Searched;

    public BreadthFirstSearch()
    {
      Searched = new List<Node>();
      FrontierNext = new List<Node>();
    }

    public bool Run(ref Map map)
    {
      bool FoundEND = false;
      bool ENDedEarly = false;
      int Steps = 0;
      int MaxSteps = map.Width * map.Height * 1000;

      Searched = new List<Node>();

      Frontier = map.GetAdjacentCells(map.StartCell);

      //Console.WriteLine("Current frontier ");
      //foreach (Node i in Frontier)
      //{
      //  Console.Write(i.ID + ", ");
      //}
      //Console.WriteLine("");

      while (!FoundEND || !ENDedEarly)
      {

        FrontierNext = new List<Node>();

        foreach (Node cd in Frontier)
        {
          if (cd.ID == map.EndCell)
          {
            Searched.Add(cd);
            FoundEND = true;
            break;
          }
          else
          {
            bool found = false;

            if (Searched != null)
            {
              foreach (Node cd2 in Searched)
              {
                if (cd.ID == cd2.ID)
                  found = true;
              }
            }

            if (FrontierNext != null)
            {
              foreach (Node cd2 in FrontierNext)
              {
                if (cd.ID == cd2.ID)
                  found = true;
              }
            }

            if (!found)
            {
              FrontierNext.AddRange(map.GetAdjacentCells(cd.ID));
              Searched.Add(cd);
            }

          }

          Frontier = FrontierNext;

        }

        if (Steps >= MaxSteps)
          ENDedEarly = true;
        Steps++;
      }


      Console.WriteLine("Format: (current node, move direction, new node)");
      Console.WriteLine("Searched: ");
      foreach (Node cd in Searched)
      {
        Console.Write("(" + " here" + ", " + cd.Direction + ", " + cd.ID + "), ");
      }
      Console.WriteLine(" ");

      Console.WriteLine("ENDedEarly: " + ENDedEarly + ", FoundEND: " + FoundEND);
      return ENDedEarly || FoundEND;
    }
    
  }
}
