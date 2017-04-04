using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class DepthFirstSearch //temp new breadth search.
  {
    public List<Node> Frontier;
    public List<int> Searched;

    public DepthFirstSearch()
    {
      Frontier = new List<Node>();
      Searched = new List<int>();
    }

    public bool Run(ref Map map)
    {
      bool FoundEND = false;
      bool ENDedEarly = false;
      int Steps = 0;
      int MaxSteps = map.Width * map.Height * 1000;

      Frontier.Add(new Node(map.StartCell, Action.NOOP));
      Frontier[0].ConnectedNodes = map.GetAdjacentCells(map.StartCell);

      while (!FoundEND || !ENDedEarly)
      {

        foreach (Node n in Frontier)
        {
          if (n.ID == map.EndCell)
          {
            FoundEND = true;
            break;
          }
          else
          {
            bool found = false;

            foreach (int s in Searched)
            {
              if (n.ID == s)
              {
                found = true;
                break;
              }
            }

            if (!found)
            {
              Searched.Add(n.ID);
              Frontier[Frontier.IndexOf(n)].ConnectedNodes.AddRange(map.GetAdjacentCells(n.ID));
            }

          }
        }

        if (Steps >= MaxSteps)
          ENDedEarly = true;
        Steps++;
      }


      Console.WriteLine("Format: (current node, move direction, new node)");
      Console.WriteLine("Searched: ");
      foreach (Node n in Frontier)
      {
        foreach (Node n2 in n.ConnectedNodes)
        {
          Console.Write("(" + n.ID + ", " + n2.Direction + ", " + n2.ID + "), ");
        }
        Console.WriteLine(" ");
      }
      Console.WriteLine(" ");

      Console.WriteLine("ENDedEarly: " + ENDedEarly + ", FoundEND: " + FoundEND);
      return ENDedEarly || FoundEND;
    }

  }


}
