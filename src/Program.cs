using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  enum Action { LEFT = 0, RIGHT = 1, UP = 2, DOWN = 3, NOOP = 4 };
  enum CellType { NORMAL = 0, WALL = 1, START = 2, END = 3 };
  public struct Rect { public int x,y,width,height; };

  class Program
  {
    static void Main(string[] args)
    {
      if ((args.Length >= 3 && args[2].ToLower() == "debug") ? ExtensionMethods.Debug = true : ExtensionMethods.Debug = false);

      Map map = new Map();
      Agent agn = new Agent();

      if (args.Length >= 1) //check if a file (map) was passed
      {
        ReadFile file = new ReadFile(args[0]);
        if (file.Good)
        {
          map.ProcessFile(ref file);
          map.SetCost(1);
        } //end if good
      }

      if (args.Length >= 2) //check if a search was passed in.
      {
        if (args[1].ToLower() == "gui")
        {
          GUI gui = new GUI();
          gui.Run(ref agn, ref map);
        } //end gui check
        else
        {
          agn.Run(args[1], ref map);
        }
      } //end of length check == 2
      else
      {
        Console.WriteLine("no parameters");
      }
    } //end of main
  } //end of class
} //end of namespace
