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
      SearchResult SearchResults = new SearchResult(map.Width * map.Height * 1000); //pass in how mant steps to take
      int Goal = map.EndCell;

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
