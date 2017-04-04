using System;

namespace MazeNavigation
{
  class ReadFile
  {
    private string file;
    private string[] lines;
    private bool good;

    public ReadFile(string inFile)
    {
      file = inFile;
      try
      {
        lines = System.IO.File.ReadAllLines(File);
        good = true;
      }
      catch
      {
        Console.WriteLine("File '" + File + "' could not be found");
        good = false;
      }

    }

    public string File
    {
      get { return file; }
    }

    public string[] Lines
    {
      get { return lines; }
    }

    public bool Good
    {
      get { return good; }
    }

  }
}
