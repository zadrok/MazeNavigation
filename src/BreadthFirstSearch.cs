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
            SearchResults.EndFound = true;
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

      //reconstruct the most optimal path
      SearchResults.ReconstructPath(ref map);

      if (ExtensionMethods.Debug)
      {
        Console.WriteLine("Final Path: ");
        SearchResults.PrintFinalPath();
        SearchResults.PrintSearched();
        SearchResults.OutputInfo();
      }

      return SearchResults;
    } //end Run()

  } //end class
} //end namespace
