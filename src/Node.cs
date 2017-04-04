using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class Node
  {
    private int id;
    private Action direction;
    private List<Node> connectedNodes;

    public Node(int inID, Action inDirection)
    {
      id = inID;
      direction = inDirection;
      connectedNodes = new List<Node>();
    }

    public int ID
    {
      get { return id; }
    }

    public Action Direction
    {
      get { return direction; }
    }

    internal List<Node> ConnectedNodes
    {
      get { return connectedNodes; }
      set { connectedNodes = value; }
    }

  }
}
