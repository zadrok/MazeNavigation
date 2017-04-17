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
