using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class SearchResult
  {

    private List<Node> searchedNodes;
    private List<Node> finalPath;
    private bool foundEnd;
    private bool endedEarly;
    private int steps;
    private int maxSteps;

    public SearchResult(int maxStepsToTake)
    {
      searchedNodes = new List<Node>();
      finalPath = new List<Node>();
      foundEnd = false;
      endedEarly = false;
      steps = 0;
      maxSteps = maxStepsToTake;
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
      Console.WriteLine("EndedEarly: " + EndedEarly + ", FoundEnd: " + FoundEnd + ", Steps taken: " + Steps + ", Max steps allowed: " + MaxSteps);
    }

    public string PrintFinalPath()
    {
      string result = "";
      foreach (Node n in finalPath)
      {
        result += n.Direction.ToString() + ", ";
      }
      return result;
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
