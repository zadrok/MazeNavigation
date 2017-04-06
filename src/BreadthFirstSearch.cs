using System;
using System.Collections.Generic;

namespace MazeNavigation
{

  //Expand all options one level at a time
  class BreadthFirstSearch : SearchMethod
  {
    public List<Node> Frontier;
    public List<Node> FrontierNext;
    public List<Node> Searched;

    public BreadthFirstSearch(string aName)
    {
      Name = aName;
      Searched = new List<Node>();
      FrontierNext = new List<Node>();
    }

    public override bool Run(ref Map map)
    {
      bool FoundEnd = false;
      bool EndedEarly = false;
      int Steps = 0;
      int MaxSteps = map.Width * map.Height * 1000;
      int Goal = map.EndCell;

      Searched = new List<Node>();

      Frontier = map.GetAdjacentCells(map.StartCell);

      while (!FoundEnd && !EndedEarly)
      {
        //make sure that the list of nodes to check next is empty.
        FrontierNext = new List<Node>();

        //check all nodes in Frontier
        foreach (Node n in Frontier)
        {
          //check current node for goal state
          if (n.ID == Goal)
          {
            Searched.Add(n);
            FoundEnd = true;
            break;
          }
          else
          {
            bool found = false;

            //check if the current node is in the list of searched nodes
            foreach (Node n2 in Searched)
            {
              if (n.ID == n2.ID)
                found = true;
            }

            //check if the current node is in the list of nodes to check next
            foreach (Node n2 in FrontierNext)
            {
              if (n.ID == n2.ID)
                found = true;
            }

            //if current node is not found then it can be add to the list to search next
            if (!found)
            {
              FrontierNext.AddRange(map.GetAdjacentCells(n.ID));
              Searched.Add(n);
            }

          }

          //make the list of nodes to search next the current list of nodes to search
          Frontier = FrontierNext;

        }

        if (Steps >= MaxSteps)
          EndedEarly = true;
        Steps++;
      }


      Console.WriteLine("Searched: ");
      foreach (Node n in Searched)
      {
        Console.Write(n.ID + ", ");
      }
      Console.WriteLine(" ");

      Console.WriteLine("EndedEarly: " + EndedEarly + ", FoundEnd: " + FoundEnd +
                        ", Steps taken: " + Steps + ", Max steps allowed: " + MaxSteps);
      return EndedEarly || FoundEnd;
    }

  }
}
