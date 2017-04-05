using System;
using System.Collections.Generic;

namespace MazeNavigation
{
  class GreedyBestFirstSearch
  {
    public GreedyBestFirstSearch()
    {

    }

    public bool Run(ref Map map)
    {
      bool FoundEnd = false;
      bool EndedEarly = false;
      int Steps = 0;
      int MaxSteps = map.Width * map.Height * 1000;
      int Goal = map.EndCell;

      while (!FoundEnd && !EndedEarly)
      {



        //check steps
        if (Steps >= MaxSteps)
          EndedEarly = true;
        Steps++;
      } //end while

      Console.WriteLine("EndedEarly: " + EndedEarly + ", FoundEnd: " + FoundEnd +
                        ", Steps taken: " + Steps + ", Max steps allowed: " + MaxSteps);
      return EndedEarly || FoundEnd;
    }
  }
}
