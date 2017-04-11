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

    public override SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult(map.Width * map.Height * 1000); //pass in how mant steps to take
      int Goal = map.EndCell;

      Searched = new List<Node>();

      Frontier = map.GetAdjacentNodes(map.StartCell);

      while (!SearchResults.End())
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
            SearchResults.FoundEnd = true;
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
              FrontierNext.AddRange(map.GetAdjacentNodes(n.ID));
              Searched.Add(n);
            }

          }

          //make the list of nodes to search next the current list of nodes to search
          Frontier = FrontierNext;

        }


        SearchResults.TakeStep();
      } //end while


      if (ExtensionMethods.Debug)
      {
        Console.WriteLine("Searched: ");
        foreach (Node n in Searched)
        {
          Console.Write(n.ID + ", ");
        }
        Console.WriteLine(" ");

        SearchResults.OutputInfo();
      }

      return SearchResults;
    }

  }
}
