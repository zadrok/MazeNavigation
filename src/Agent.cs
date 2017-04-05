using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class Agent
  {
    public Agent()
    {

    }

    public void DFS(ref Map map)
    {
      Console.WriteLine("Starting depth-first search");
      DepthFirstSearch dfs = new DepthFirstSearch();
      Console.WriteLine(dfs.Run(ref map));
    }

    public void BFS(ref Map map)
    {
      Console.WriteLine("Starting breadth-first search");
      BreadthFirstSearch bfs = new BreadthFirstSearch();
      Console.WriteLine( bfs.Run(ref map) );
    }

    public void GBFS(ref Map map)
    {
      Console.WriteLine("Starting greedy best-first");
      GreedyBestFirstSearch gbfs = new GreedyBestFirstSearch();
      Console.WriteLine( gbfs.Run(ref map) );
    }

    public void AS(ref Map map)
    {
      Console.WriteLine("Starting A* (“A Star”)");
      AStarSearch ass = new AStarSearch();
      Console.WriteLine( ass.Run(ref map) );
    }

    public void CUS1(ref Map map)
    {
      Console.WriteLine("Starting Your search strategy 1");
      CustumSearch1 cus1 = new CustumSearch1();
      Console.WriteLine( cus1.Run(ref map) );
    }

    public void CUS2(ref Map map)
    {
      Console.WriteLine("Starting Your search strategy 2");
      CustomSearch2 cus2 = new CustomSearch2();
      Console.WriteLine( cus2.Run(ref map) );
    }

  }
}
