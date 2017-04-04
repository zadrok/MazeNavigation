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
      Console.WriteLine("STARTing depth-first search");
      DepthFirstSearch dfs = new DepthFirstSearch();
      Console.WriteLine(dfs.Run(ref map));
    }

    public void BFS(ref Map map)
    {
      Console.WriteLine("STARTing breadth-first search");
      BreadthFirstSearch bfs = new BreadthFirstSearch();
      Console.WriteLine( bfs.Run(ref map) );
    }

    public void GBFS(ref Map map)
    {
      Console.WriteLine("STARTing greedy best-first");
      GreedyBestFirstSearch gbfs = new GreedyBestFirstSearch();
    }

    public void AS(ref Map map)
    {
      Console.WriteLine("STARTing A* (“A Star”)");
      AStarSearch ass = new AStarSearch();
    }

    public void CUS1(ref Map map)
    {
      Console.WriteLine("STARTing Your search strategy 1");
      CustumSearch1 cus1 = new CustumSearch1();
    }

    public void CUS2(ref Map map)
    {
      Console.WriteLine("STARTing Your search strategy 2");
      CustomSearch2 cus2 = new CustomSearch2();
    }

  }
}
