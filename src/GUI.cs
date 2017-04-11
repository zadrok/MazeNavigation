using System;
using System.Collections.Generic;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,

namespace MazeNavigation
{
  class GUI
  {
    public GUI()
    {

    }

    public void Run(ref Agent agn, ref Map map)
    {
      //Open the game window
      OpenGraphicsWindow("Maze Navigation", 800, 600);

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

          //Draw onto the screen
          RefreshScreen(30);
      } //end of while
    }

    public void DrawInterface(ref Agent agn, ref Map map)
    {
      int x = 10;
      int y = 15;
      int w = 0;
      int h = 16;

      foreach (SearchMethod sm in agn.SearchMethods)
      {
        w = sm.Name.Length*10;
        if (Button(sm.Name, Color.Black, Color.LightBlue, Color.Black, x,y,w,h))
          Console.WriteLine(sm.Run(ref map).PrintFinalPath());

        x += w + 10;
      }
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
        SwinGame.DrawText(map.Cells.IndexOf(c).ToString(), Color.Black, x+textPad, y+textPad); //cellID
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
      }
    }

    public bool valueInRange(int value, int min, int max)
		{
			return (value >= min) && (value <= max);
		}

    public bool rectOverlap(Rect A, Rect B)
		{
			bool xOverlap = valueInRange(A.x, B.x, B.x + B.width) || valueInRange(B.x, A.x, A.x + A.width);
			bool yOverlap = valueInRange(A.y, B.y, B.y + B.height) || valueInRange(B.y, A.y, A.y + A.height);

			return xOverlap && yOverlap;
		}

    public bool Button(string txt, Color txtColor, Color btnBackColor, Color btnLineColor, int x, int y, int w, int h)
    {
      int textPad = 3;
      SwinGame.FillRectangle(btnBackColor,x,y,w,h); //draw background
      SwinGame.DrawRectangle(btnLineColor,x,y,w,h); //draw outline
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

			return rectOverlap(A,B) && SwinGame.MouseClicked(MouseButton.LeftButton);
    }

  }
}
