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
      bool FoundEnd = false;
      bool EndedEarly = false;
      int Steps = 0;
      int MaxSteps = map.Width * map.Height * 1000;
      int Goal = map.EndCell;

      Searched = new List<Node>();

      Frontier = map.GetAdjacentCells(map.StartCell);

      //Console.WriteLine("Current frontier ");
      //foreach (Node i in Frontier)
      //{
      //  Console.Write(i.ID + ", ");
      //}
      //Console.WriteLine("");

      while (!FoundEnd && !EndedEarly)
      {
        //make sure that the list of nodes to check next is empty.
        FrontierNext = new List<Node>();

        foreach (Node cd in Frontier)
        {
          //check goal state
          if (cd.ID == Goal)
          {
            Searched.Add(cd);
            FoundEnd = true;
            break;
          }
          else
          {
            bool found = false;

            //check if the current node is in the list of searched nodes
            foreach (Node cd2 in Searched)
            {
              if (cd.ID == cd2.ID)
                found = true;
            }

            //check if the current node is in the list of nodes to check next
            foreach (Node cd2 in FrontierNext)
            {
              if (cd.ID == cd2.ID)
                found = true;
            }

            //if current node is not found then it can be add to the list to search next
            if (!found)
            {
              FrontierNext.AddRange(map.GetAdjacentCells(cd.ID));
              Searched.Add(cd);
            }

          }

          //make the list of nodes to search next the current list of nodes to search
          Frontier = FrontierNext;

        }

        if (Steps >= MaxSteps)
          EndedEarly = true;
        Steps++;
      }


      Console.WriteLine("Format: (current node, move direction, new node)");
      Console.WriteLine("Searched: ");
      foreach (Node cd in Searched)
      {
        Console.Write("(" + " here" + ", " + cd.Direction + ", " + cd.ID + "), ");
      }
      Console.WriteLine(" ");

      Console.WriteLine("EndedEarly: " + EndedEarly + ", FoundEnd: " + FoundEnd +
                        ", Steps taken: " + Steps + ", Max steps allowed: " + MaxSteps);
      return EndedEarly || FoundEnd;
    }

  }
}
