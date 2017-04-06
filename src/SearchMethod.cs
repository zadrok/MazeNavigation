using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class SearchMethod
  {
    private string name;
    public virtual bool Run(ref Map map)
    {
      return false;
    }
    public string Name
    {
      get { return name; }
      set { name = value; } 
    }
  }
}
