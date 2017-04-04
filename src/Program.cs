using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  enum Action { LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3, SUCK = 4, NOOP = 5 };
  enum CellType { NORMAL = 0, WALL = 1, START = 2, END = 3 };

  class Program
  {
    static void Main(string[] args)
    {
      Map map;
      Agent agn = new Agent();

      if (args.Length < 1)
      {
        Console.WriteLine("no file parameter");
      }
      else
      {
        //variables needed
        ReadFile file = new ReadFile(args[0]);
        if (file.Good)
        {
          map = new Map(file.Lines[0]);

          foreach (string line in file.Lines)
          {
            map.Add(line);
          }

          map.SetCost(1);

          //map.PrintLine();
          //map.PrintCellID();
          //map.PrintGrid();

          //agn.DFS(ref map);

          //Console.Write("Press any key to continue... ");
          //Console.ReadLine();
        }
      }

      if (args.Length > 1)
      {
        if (args[1] == "gui")
        {
          //Open the game window
          OpenGraphicsWindow("Maze Navigation", 800, 600);

          //Run the game loop
          while(false == WindowCloseRequested())
          {
              //Fetch the next batch of UI interaction
              ProcessEvents();

              //Clear the screen and draw the framerate
              ClearScreen(Color.White);
              DrawFramerate(0,0);

              map.DrawMap();

              //Draw onto the screen
              RefreshScreen(60);
          }
        }
      }

    }
  }
}
