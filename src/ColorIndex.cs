using System;
using System.Collections.Generic;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  static class ColorIndex
  {
    private static Color normal;
    private static Color start;
    private static Color end;
    private static Color wall;

    private static Color searchedNormal;
    private static Color searchedStart;
    private static Color searchedEnd;

    public static Color Normal
    {
      get { return normal; }
      set { normal = value; }
    }

    public static Color Start
    {
      get { return start; }
      set { start = value; }
    }

    public static Color End
    {
      get { return end; }
      set { end = value; }
    }

    public static Color Wall
    {
      get { return wall; }
      set { wall = value; }
    }

    public static Color SearchedNormal
    {
      get { return searchedNormal; }
      set { searchedNormal = value; }
    }

    public static Color SearchedStart
    {
      get { return searchedStart; }
      set { searchedStart = value; }
    }

    public static Color SearchedEnd
    {
      get { return searchedEnd; }
      set { searchedEnd = value; }
    }

  }
}
