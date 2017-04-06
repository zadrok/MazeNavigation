using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class Agent
  {

    private List<SearchMethod> searchMethods;

    public Agent()
    {
      searchMethods = new List<SearchMethod>();
      searchMethods.Add(new DepthFirstSearch("DFS"));
      searchMethods.Add(new BreadthFirstSearch("BFS"));
      searchMethods.Add(new GreedyBestFirstSearch("GBFS"));
      searchMethods.Add(new AStarSearch("AS"));
      searchMethods.Add(new CustomSearch1("CUS1"));
      searchMethods.Add(new CustomSearch2("CUS2"));
    }

    public void Run(string aSearch, ref Map map)
    {
      foreach (SearchMethod sm in SearchMethods)
      {
        if (aSearch.ToLower() == sm.Name.ToLower())
          Console.WriteLine(sm.Run(ref map));
      }
    }

    public List<SearchMethod> SearchMethods
    {
      get { return searchMethods; }
    }

  }
}
