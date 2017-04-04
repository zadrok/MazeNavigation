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
      Map map = new Map();
      Agent agn = new Agent();

      Console.WriteLine("args Length: " + args.Length);
      for (int i = 0; i < args.Length; i++)
      {
        Console.WriteLine(i + " : " + args[i]);
      }

      if (/*args.Length >= 1*/ true) //check if a file (map) was passed
      {
        ReadFile file = new ReadFile("map.txt");
        if (file.Good)
        {
          foreach (string line in file.Lines)
          {
            map.Add(line);
          }
          map.SetCost(1);

          //map.PrintLine();
          map.PrintCellID();
          map.PrintGrid();

          //agn.DFS(ref map);

          //Console.Write("Press any key to continue... ");
          //Console.ReadLine();
        } //end if good
      }
      else
      {
        Console.WriteLine("no file parameter");
      }

      if (args.Length >= 2)
      {
        if (/*args[1] == "gui"*/ true)
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
          } //end of while
        } //end of gui check
      } //end of length check
    } //end of main
  } //end of class
} //end of namespace
