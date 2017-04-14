using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class AStarSearch : SearchMethod
  {
    public AStarSearch(string aName)
    {
      Name = aName;
    }

    public override SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult(Name, map.StartCell, map.EndCell, map.Width * map.Height * 1000); //pass in how mant steps to take

      // List<Node> closedSet = new List<Node>();
      // List<Node> openSet = new List<Node>();
      // openSet.Add(SearchResults.StartNode);
      // openSet[0].GCost = 0;
      // openSet[0].FCost = map.HeuristicCostEstimate(map.StartCell, map.EndCell);


      while (SearchResults.End())
      {



        SearchResults.TakeStep();
      } //end while

      if (ExtensionMethods.Debug)
        SearchResults.OutputInfo();
      return SearchResults;
    }
  }
}
