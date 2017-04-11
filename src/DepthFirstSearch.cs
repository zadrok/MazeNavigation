﻿using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class DepthFirstSearch : SearchMethod
  {
    public DepthFirstSearch(string aName)
    {
      Name = aName;
    }

    public override SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult(map.Width * map.Height * 1000); //pass in how mant steps to take
      int Goal = map.EndCell;

      while (!SearchResults.End())
      {



        SearchResults.TakeStep();
      } //end while

      if (ExtenstionMethods.Debug)
        SearchResults.OutputInfo();
      return SearchResults;
    }
  }
}
