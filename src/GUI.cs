using System;
using System.Collections.Generic;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  class GUI
  {
    private bool animateMap;
    private bool animateStart;
    private bool animateEnd;
    private SearchResult searchResults;

    public GUI()
    {
      animateMap = false;
      animateStart = false;
      animateEnd = false;
      searchResults = null;
    }

    public void Run(ref Agent agn, ref Map map)
    {
      //Open the game window
      OpenGraphicsWindow("Maze Navigation", 800, 600);

      SetColorIndex();
      map.SetColors();

      //Run the game loop
      while(false == WindowCloseRequested())
      {
          //Fetch the next batch of UI interaction
          ProcessEvents();

          //Clear the screen and draw the framerate
          ClearScreen(Color.White);
          //DrawFramerate(0,0);

          DrawInterface(ref agn, ref map);
          DrawMap(ref map);
          AnimateMap(ref map);

          //Draw onto the screen
          RefreshScreen(30);
      } //end of while
    }

    public void DrawInterface(ref Agent agn, ref Map map)
    {
      int x = 10;
      int y = 15;
      int w = 0;
      int h = 14;

      foreach (SearchMethod sm in agn.SearchMethods)
      {
        w = sm.Name.Length*8;
        if (Button(sm.Name, Color.Black, Color.LightBlue, Color.Black, x,y,w,h))
        {
          searchResults = sm.Run(ref map);
          animateMap = true;
          animateStart = true;
          animateEnd = false;
        }
        x += w + 10;
      }

      string tmp = "Reset Map";
      w = tmp.Length*8;
      if (Button(tmp, Color.Black, Color.Pink, Color.Black, x,y,w,h))
      {
        map.SetColors();
      }
      x += w + 10;
    }

    public void AnimateMap(ref Map map)
    {
      if (animateMap) //make sure the map needs to be animated
      {
        if (animateStart) //check if this is the start of the animation
        {
          map.SetColors();
          animateStart = false;
        } //animateStart

        if (searchResults.SearchedNodes.Count > 0) //make sure that there is someting to animte with
        {
          foreach(Node n in searchResults.SearchedNodes)
          {
            if (n.ID == map.StartCell)
            {
              map.SetCellColor(n.ID, ColorIndex.SearchedStart);
            }
            else if (n.ID == map.EndCell)
            {
              map.SetCellColor(n.ID, ColorIndex.SearchedEnd);
            }
            else
            {
              map.SetCellColor(n.ID, ColorIndex.SearchedNormal);
            }

          } //end foreach

          //Console.WriteLine(searchResults.FinalPath.Count);
          foreach(Node n in searchResults.FinalPath)
          {
            if (n.ID == map.StartCell)
            {
              map.SetCellColor(n.ID, ColorIndex.SearchedStart);
            }
            else if (n.ID == map.EndCell)
            {
              map.SetCellColor(n.ID, ColorIndex.SearchedEnd);
            }
            else
            {
              map.SetCellColor(n.ID, ColorIndex.FinalNormal);
            }

          } //end foreach

          animateEnd = true; //this is the end of the animation
        }
        else
        {
          animateEnd = true; //this is the end of the animation
        } //Count

        if (animateEnd) //check if this is the end of the animation
        {
          animateMap = false;
          animateEnd = false;
          searchResults = null;
        } //animateEnd
      } //animateMap
    }

    public void DrawMap(ref Map map)
    {
      int x = 0;
      int y = 65;
      int w = 55;
      int h = 55;
      int textPad = 3;
      int textHeight = 12;
      int count = 0;
      foreach (Cell c in map.Cells)
      {
        //draw coloured cell
        SwinGame.FillRectangle(c.Color,x,y,w,h);
        SwinGame.DrawRectangle(Color.Black,x,y,w,h); //draw cell outline
        SwinGame.DrawText(map.Cells.IndexOf(c).ToString(), Color.Black, x+textPad, y+textPad); // cell ID
        SwinGame.DrawText(c.Type.ToString(), Color.Black, x+textPad, y+textPad+textHeight); // cell Type
        SwinGame.DrawText(c.Cost.ToString(), Color.Black, x+textPad, y+textPad+textHeight+textHeight); // cell cost

        if (count == map.Width - 1)
        {
          x = 0;
          y += h;
          count = 0;
        }
        else
        {
          count++;
          x += w;
        }
      } // end foreach
    }

    public bool Button(string txt, Color txtColor, Color btnBackColor, Color btnLineColor, int x, int y, int w, int h)
    {
      int textPad = 3;
      SwinGame.FillRectangle(btnBackColor,x,y,w+(textPad*2),h); //draw background
      SwinGame.DrawRectangle(btnLineColor,x,y,w+(textPad*2),h); //draw outline
      SwinGame.DrawText(txt, txtColor, x+textPad, y+textPad); //text

      Rect A = new Rect();
			A.x = x;
			A.y = y;
			A.width = w;
			A.height = h;

			Rect B = new Rect();
			B.x = (int)SwinGame.MouseX();
			B.y = (int)SwinGame.MouseY();
			B.width = 3;
			B.height = 3;

			return ExtensionMethods.rectOverlap(A,B) && SwinGame.MouseClicked(MouseButton.LeftButton);
    }

    public void SetColorIndex()
    {
      ColorIndex.Normal = Color.LightGray;
      ColorIndex.Start = Color.Crimson;
      ColorIndex.End = Color.ForestGreen;
      ColorIndex.Wall = Color.Cyan;

      ColorIndex.SearchedNormal = Color.Pink;
      ColorIndex.SearchedStart = Color.Magenta;
      ColorIndex.SearchedEnd = Color.DarkMagenta;

      ColorIndex.FinalNormal = Color.HotPink;
    }

  }
}
