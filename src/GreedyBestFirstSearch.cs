using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class GreedyBestFirstSearch : SearchMethod
  {
    public GreedyBestFirstSearch(string aName)
    {
      Name = aName;
    }

    public override SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult(Name, map.StartCell, map.EndCell, map.Width * map.Height * 1000); //pass in how mant steps to take

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
