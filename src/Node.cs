using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class Node
  {
    private int id;
    private int bestAdjacentNodeID;
    private int gCost;
    private int fCost;
    private Action direction;
    private List<Node> connectedNodes;

    public Node(int inID, Action inDirection)
    {
      id = inID;
      BestAdjacentNodeID = -1;
      gCost = -1;
      fCost = -1;
      direction = inDirection;
      connectedNodes = new List<Node>();
    }

    public void Print()
    {
      Console.WriteLine("ID: " + ID + ", BestAdjacentNodeID: " + BestAdjacentNodeID + ", Direction: " + Direction.ToString());
    }

    public int ID
    {
      get { return id; }
    }

    public int BestAdjacentNodeID
    {
      get { return bestAdjacentNodeID; }
      set { bestAdjacentNodeID = value; }
    }

    public int GCost
    {
      get { return gCost; }
      set { gCost = value; }
    }

    public int FCost
    {
      get { return fCost; }
      set { fCost = value; }
    }

    public Action Direction
    {
      get { return direction; }
      set { direction = value; }
    }

    internal List<Node> ConnectedNodes
    {
      get { return connectedNodes; }
      set { connectedNodes = value; }
    }

  }
}
