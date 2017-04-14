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
    private bool foundEnd;
    private bool endedEarly;
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
      foundEnd = false;
      endedEarly = false;
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
        EndedEarly = true;
      Steps++;
    }

    public bool End()
    {
      return !foundEnd && !endedEarly;
    }

    public void OutputInfo()
    {
      Console.WriteLine(Name);
      Console.WriteLine("EndedEarly: " + EndedEarly + ", FoundEnd: " + FoundEnd + ", Steps taken: " + Steps + ", Max steps allowed: " + MaxSteps);
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

    public string Name
    {
      get { return name; }
    }

    public bool FoundEnd
    {
      get { return foundEnd; }
      set { foundEnd = value; }
    }

    public bool EndedEarly
    {
      get { return endedEarly; }
      set { endedEarly = value; }
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
