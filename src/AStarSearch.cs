using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class AStarSearch : SearchMethod
  {
    public AStarSearch(string aName)
    {
      Name = aName;
    }

    public Node GetCurrent(List<Node> aList)
    {
      if (aList.Count < 1)
        return null;

      Node tmp = aList[0];
      foreach (Node n in aList)
      {
        if (n.FScore < tmp.FScore)
          tmp = n;
      }
      return tmp;
    }

    public bool NodeInList(Node aNode, List<Node> aList)
    {
      foreach (Node n in aList)
      {
        if (n.ID == aNode.ID)
          return true;
      }
      return false;
    }

    public int GetGScore(Node aNode, List<Node> aList)
    {
      foreach (Node n in aList)
      {
        if (n.ID == aNode.ID)
          return n.GScore;
      }
      return 10000000;
    }

    public override SearchResult Run(ref Map map)
    {
      SearchResult SearchResults = new SearchResult(Name, map.StartCell, map.EndCell, map.Width * map.Height * 1000); //pass in how mant steps to take
      bool extraDebug = false;

      SearchResults.Frontier.Add(SearchResults.StartNode);
      SearchResults.Frontier[SearchResults.FrontierLast].GScore = 0;
      SearchResults.Frontier[SearchResults.FrontierLast].FScore = map.HeuristicCostEstimate(map.StartCell, map.EndCell);
      if (extraDebug)
        Console.WriteLine("1");

      while (SearchResults.End())
      {
        //get current node to work on
        Node current = GetCurrent(SearchResults.Frontier);
        if (current == null)
        {
          SearchResults.EndNull = true;
          break;
        }
        if (extraDebug)
          Console.WriteLine("2");

        //check if the current node is the goal node
        if (current.ID == SearchResults.GoalNode.ID)
        {
          SearchResults.SearchedNodes.Add(current);
          SearchResults.EndFound = true;
          break;
        }
        if (extraDebug)
          Console.WriteLine("3");

        //remove the current node from the Frontier
        SearchResults.Frontier.Remove(current);
        //add the current node to the list of searched nodes (closed set)
        SearchResults.SearchedNodes.Add(current);

        if (extraDebug)
          Console.WriteLine("4");

        //check all of the current nodes neighbors
        foreach (Node n in map.GetAdjacentNodes(current.ID))
        {
          if (extraDebug)
            Console.WriteLine("5");
          //check if n is in the closed set, if so skip
          if (NodeInList(n, SearchResults.SearchedNodes))
            continue;

          //find the distance from start to n
          int tentative_gScore = current.GScore + 1;

          if (extraDebug)
            Console.WriteLine("6");

          //if n is not currently in the Frontier add it.
          if (!NodeInList(n, SearchResults.Frontier))
          {
            n.GScore = tentative_gScore;
            n.FScore = n.GScore + map.HeuristicCostEstimate(current.ID, n.ID);
            n.BestAdjacentNodeID = current.ID;
            SearchResults.Frontier.Add(n);
            if (extraDebug)
              Console.WriteLine("BOOP");
          }
          else if (NodeInList(n, SearchResults.Frontier))
          {
            if (extraDebug)
              Console.WriteLine("BLOOP");
            //if the node is in the frontier and has a lower gScore then it can be skipped
            if (tentative_gScore >= GetGScore(n, SearchResults.Frontier))
            {
              if (extraDebug)
                Console.WriteLine("POOP");
              continue;
            }
          }
          if (extraDebug)
            Console.WriteLine("7");

          //getting to this point means that; this is the best path found so far so it needs to be recorded
          n.GScore = tentative_gScore;
          n.FScore = n.GScore + map.HeuristicCostEstimate(current.ID, n.ID);
          n.BestAdjacentNodeID = current.ID;
          SearchResults.Frontier.Add(n);

          if (extraDebug)
            Console.WriteLine("8");

        } //end foreach loop



        SearchResults.TakeStep();
      } //end while

      //reconstruct the most optimal path
      SearchResults.ReconstructPath(ref map);

      if (extraDebug)
        Console.WriteLine("9");

      if (ExtensionMethods.Debug)
      {
        Console.WriteLine("Final Path: ");
        foreach (Node n in SearchResults.FinalPath)
        {
          Console.WriteLine("ID: " + n.ID + ", Direction: " + n.Direction);
        }

        Console.WriteLine("Searched: ");
        foreach (Node n in SearchResults.SearchedNodes)
        {
          Console.Write(n.ID + ", ");
        }
        Console.WriteLine(" ");

        SearchResults.OutputInfo();
      }

      return SearchResults;
    }
  }
}
