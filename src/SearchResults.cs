using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class SearchResults
  {

    private List<Node> searchedNodes;
    private List<Node> finalPath;

    public SearchResults()
    {
      searchedNodes = new List<Node>();
      finalPath = new List<Node>();
    }

    internal List<Node> SearchedNodes
    {
      get { return searchedNodes; }
      set { searchedNodes = value; }
    }

    internal List<Node> FinalPath
    {
      get { return finalPath; }
      set { finalPath = value; }
    }

  }
}
