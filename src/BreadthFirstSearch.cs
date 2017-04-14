using System;
using System.Collections.Generic;

namespace MazeNavigation
{

  //Expand all options one level at a time
  class BreadthFirstSearch : SearchMethod
  {

    public BreadthFirstSearch(string aName)
    {
      Name = aName;
    }

    public override SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult(Name, map.StartCell, map.EndCell, map.Width * map.Height * 1000); //pass in how mant steps to take
      SearchResults.Frontier.Add(SearchResults.StartNode);

      while (SearchResults.End())
      {
        //check all nodes in Frontier
        foreach (Node n in SearchResults.Frontier)
        {
          //check current node for goal state
          if (n.ID == SearchResults.GoalNode.ID)
          {
            SearchResults.SearchedNodes.Add(n);
            SearchResults.FoundEnd = true;
            break;
          }
          else
          {
            bool found = false;

            //check if the current node is in the list of searched nodes
            foreach (Node n2 in SearchResults.SearchedNodes)
            {
              if (n.ID == n2.ID)
                found = true;
            }

            //if current node is not found then it can be add to the list to search next
            if (!found)
            {
              List<Node> tmp = map.GetAdjacentNodes(n.ID);
              foreach (Node n2 in tmp)
              {
                n2.BestAdjacentNodeID = n.ID;
              }
              SearchResults.FrontierNext.AddRange(tmp);
              SearchResults.SearchedNodes.Add(n);
            }

          } //end else

        } //end foreach

        SearchResults.Frontier = SearchResults.FrontierNext;
        SearchResults.ClearFrontierNext();

        SearchResults.TakeStep();
      } //end while

      bool done = false;
      SearchResults.FinalPath.Add(SearchResults.GoalNode);
      while (!done)
      {
        SearchResults.ClearFrontierNext();
        foreach (Node n in SearchResults.SearchedNodes)
        {
          if (n.ID == SearchResults.FinalPath[SearchResults.FinalPath.Count-1].ID)
          {
            Node tmp = new Node(n.BestAdjacentNodeID, map.GetAction(n.ID, n.BestAdjacentNodeID));
            //tmp.Print();
            SearchResults.FinalPath.Add(tmp);
          }
        }

        foreach (Node n in SearchResults.FinalPath)
        {
          if (n.ID == SearchResults.StartNode.ID)
            done = true;
        }
      }

      SearchResults.FinalPath.RemoveAt(0);
      SearchResults.FinalPath = SearchResults.Reverse(SearchResults.FinalPath);

      if (ExtensionMethods.Debug)
      {
        Console.WriteLine("Final Path: ");
        foreach (Node n in SearchResults.FinalPath)
        {
          Console.WriteLine("ID: " + n.ID + ", Direction: " + n.Direction);
        }

        Console.WriteLine("Searched: ");
        foreach (Node n in SearchResults.SearchedNodes)
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
