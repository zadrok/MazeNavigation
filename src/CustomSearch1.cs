using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class CustomSearch1 : SearchMethod
  {
    public CustomSearch1(string aName)
    {
      Name = aName;
    }

    public override SearchResult Run(ref Map map)
    {
      //Last in first out
      SearchResult SearchResults = new SearchResult(Name, map.StartCell, map.EndCell, map.Width * map.Height * 1000); //pass in how mant steps to take
      SearchResults.Frontier.Add(SearchResults.StartNode);

      while (SearchResults.End())
      {
        //get current node to work on
        Node current = GetCurrent(SearchResults.Frontier);
        if (current == null)
        {
          SearchResults.EndNull = true;
          break;
        }

        //check if the current node is the goal node
        if (current.ID == SearchResults.GoalNode.ID)
        {
          SearchResults.SearchedNodes.Add(current);
          SearchResults.EndFound = true;
          break;
        }

        //remove the current node from the Frontier
        SearchResults.Frontier.Remove(current);
        //add the current node to the list of searched nodes (closed set)
        SearchResults.SearchedNodes.Add(current);

        //check all of the current nodes neighbors
        foreach (Node n in map.GetAdjacentNodes(current.ID))
        {
          //check if n is in the closed set or Frontier, if so skip
          if (NodeInList(n, SearchResults.SearchedNodes) || NodeInList(n, SearchResults.Frontier))
          {
            continue;
          }
          else
          {
            //add to front of queue
            n.BestAdjacentNodeID = current.ID;
            SearchResults.Frontier.Insert(0, n);
          }

        } //end foreach loop

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
