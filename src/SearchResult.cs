using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class SearchResult
  {

    private List<Node> searchedNodes;
    private List<Node> frontier;
    private List<Node> frontierNext;
    private List<Node> finalPath;
    private Node goalNode;
    private Node startNode;
    private bool endFound;
    private bool endEarly;
    private bool endNull;
    private int steps;
    private int maxSteps;
    private string name;

    public SearchResult(string aName, int startID, int goalID, int maxStepsToTake)
    {
      goalNode = new Node(goalID, Action.NOOP);
      startNode = new Node(startID, Action.NOOP);
      searchedNodes = new List<Node>();
      frontier = new List<Node>();
      frontierNext = new List<Node>();
      finalPath = new List<Node>();
      name = aName;
      endFound = false;
      endEarly = false;
      endNull = false;
      steps = 0;
      maxSteps = maxStepsToTake;
    }

    public void ClearFrontierNext()
    {
      frontierNext = new List<Node>();
    }

    public List<Node> Reverse(List<Node> aList)
    {
      aList.Reverse();
      foreach (Node n in aList)
      {
        n.Direction = OppositeAction(n.Direction);
      }
      return aList;
    }

    public Action OppositeAction(Action aDir)
    {
      if (aDir == Action.LEFT)
      {
        return Action.RIGHT;
      }
      else if (aDir == Action.RIGHT)
      {
        return Action.LEFT;
      }
      else if (aDir == Action.UP)
      {
        return Action.DOWN;
      }
      else if (aDir == Action.DOWN)
      {
        return Action.UP;
      }

      return Action.NOOP;
    }

    public void TakeStep()
    {
      //check steps
      if (Steps >= MaxSteps)
        EndEarly = true;
      Steps++;
    }

    public bool End()
    {
      return !endFound && !endEarly && !endNull;
    }

    public void OutputInfo()
    {
      Console.WriteLine(Name);
      Console.WriteLine("EndEarly: " + EndEarly + ", EndFound: " + EndFound + ", EndNull: " + EndNull + ", Steps taken: " + Steps + ", Max steps allowed: " + MaxSteps);
    }

    public void PrintFinalPath()
    {
      if (finalPath.Count > 0)
      {
        string result = "";
        foreach (Node n in finalPath)
        {
          result += n.Direction.ToString() + ", ";
        }
        Console.WriteLine(result.Substring(0, result.Length-2));
      }
    }

    public void ReconstructPath(ref Map map)
    {
      bool done = false;
      FinalPath.Add(GoalNode);
      while (!done)
      {
        ClearFrontierNext();
        foreach (Node n in SearchedNodes)
        {
          if (n.ID == FinalPath[FinalPathLast].ID)
          {
            Node tmp = new Node(n.BestAdjacentNodeID, map.GetAction(n.ID, n.BestAdjacentNodeID));
            FinalPath.Add(tmp);
          }
        }

        foreach (Node n in FinalPath)
        {
          if (n.ID == StartNode.ID)
            done = true;
        }
      }

      FinalPath.RemoveAt(0);
      FinalPath = Reverse(FinalPath);
    }

    public string Name
    {
      get { return name; }
    }

    public bool EndFound
    {
      get { return endFound; }
      set { endFound = value; }
    }

    public bool EndEarly
    {
      get { return endEarly; }
      set { endEarly = value; }
    }

    public bool EndNull
    {
      get { return endNull; }
      set { endNull = value; }
    }

    public int Steps
    {
      get { return steps; }
      set { steps = value; }
    }

    public int MaxSteps
    {
      get { return maxSteps; }
      set { maxSteps = value; }
    }

    public int FrontierLast
    {
      get { return Frontier.Count-1; }
    }

    public int FinalPathLast
    {
      get { return FinalPath.Count-1; }
    }

    public Node StartNode
    {
      get { return startNode; }
      set { startNode = value; }
    }

    public Node GoalNode
    {
      get { return goalNode; }
      set { goalNode = value; }
    }

    internal List<Node> Frontier
    {
      get { return frontier; }
      set { frontier = value; }
    }

    internal List<Node> FrontierNext
    {
      get { return frontierNext; }
      set { frontierNext = value; }
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
