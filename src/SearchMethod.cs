using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class SearchMethod
  {
    private string name;
    public virtual SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult("default", 0); //pass in how mant steps to take
      return SearchResults;
    }
    public string Name
    {
      get { return name; }
      set { name = value; }
    }
  }
}
