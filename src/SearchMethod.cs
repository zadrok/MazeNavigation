
using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class SearchMethod
  {
    private string name;

    public virtual SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult("default", 0, 0, 0); //pass in how mant steps to take
      return SearchResults;
    }

    public virtual Node GetCurrent(List<Node> aList)
    {
      if (aList.Count < 1)
        return null;

      return aList[0];
    }

    public virtual Node GetCurrent(List<Node> aList, ref Map map)
    {
      if (aList.Count < 1)
        return null;

      return aList[0];
    }

    public virtual bool NodeInList(Node aNode, List<Node> aList)
    {
      foreach (Node n in aList)
      {
        if (n.ID == aNode.ID)
          return true;
      }
      return false;
    }

    public string Name
    {
      get { return name; }
      set { name = value; }
    }
  }
}
