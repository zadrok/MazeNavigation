using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  abstract class SearchMethod
  {
    public List<int> Frontier;
    public List<int> Searched;

    abstract public bool addToFrontier(int aCell);
    abstract protected List<int> popFrontier();

  }
}
