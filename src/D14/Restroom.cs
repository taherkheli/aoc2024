namespace aoc.D14
{
  public class Restroom
  {
    private List<Robot> _robots;
    private readonly int _maxX;
    private readonly int _maxY;
    private Point[,] _grid;

    public Restroom(string path)
    {
      _robots = new List<Robot>();
      var lines = File.ReadAllLines(path);

      foreach (var line in lines)
      {
        var temp = line.Split(' ', 2, StringSplitOptions.TrimEntries);
        temp[0] = temp[0].Substring(2);
        temp[1] = temp[1].Substring(2);
        var p_s = temp[0].Split(',', 2, StringSplitOptions.TrimEntries);
        var v_s = temp[1].Split(',', 2, StringSplitOptions.TrimEntries);
        var p = new Point(int.Parse(p_s[0]), int.Parse(p_s[1]));
        var v = new Point(int.Parse(v_s[0]), int.Parse(v_s[1]));
        _robots.Add(new Robot(p, v));
      }

      _maxX = _robots.Max(r => r.P.X) + 1;
      _maxY = _robots.Max(r => r.P.Y) + 1;
      _grid = new Point[_maxY, _maxX];
    }

    public List<Robot> Robots { get { return _robots; } }

    public int Solve(bool isPartII = false)
    {
      if (isPartII)
        return PartII();
      else
        return PartI();
    }

    private int PartI()
    {
      var product = 1;

      for (int i = 0; i < 100; i++)
        Simulate();

      for (int i = 1; i < 5; i++)
        product *= GetRobotCount(i);

      return product;
    }

    private int PartII()
    {
      int seconds = 7847;

      for (int i = 0; i < seconds; i++)
        Simulate();

      UpdateGrid(); //move it back to inside Simulate() if printing is requred more frequently. 

      using (StreamWriter writer = new StreamWriter("output.txt", true))
      {
        writer.WriteLine("Iteration#{0}", seconds + 1);

        for (int r = 0; r < _maxY; r++)
        {
          for (int c = 0; c < _maxX; c++)
          {
            if (_grid[r, c].RobotCount == 0)
              writer.Write(".");
            else
              writer.Write(_grid[r, c].RobotCount);
          }

          writer.WriteLine();
        }
      }

      return seconds;
    }

    private void Simulate()
    {
      for (int i = 0; i < _robots.Count; i++)
      {
        int x, y;

        x = (_robots[i].P.X + _robots[i].V.X) % _maxX;
        if (x < 0)
          x = _maxX - Math.Abs(x);

        y = (_robots[i].P.Y + _robots[i].V.Y) % _maxY;
        if (y < 0)
          y = _maxY - Math.Abs(y);

        var r = _robots[i];
        r.P = new Point(x, y);
        _robots[i] = r;
      }
    }

    private void UpdateGrid()
    {
      _grid = new Point[_maxY, _maxX];

      for (int r = 0; r < _maxY; r++)
        for (int c = 0; c < _maxX; c++)
          _grid[r, c] = new Point(c, r);

      foreach (var r in _robots)
        _grid[r.P.Y, r.P.X].RobotCount++;
    }

    private int GetRobotCount(int quadrant)
    {
      if (quadrant < 1 || quadrant > 4)
        throw new ArgumentException("bad argument. Quadrant can only be 1-4");

      int mid_x = _maxX / 2;
      int mid_y = _maxY / 2;

      return quadrant switch
      {
        1 => _robots.FindAll(r => r.P.X < mid_x && r.P.Y < mid_y).Count,
        2 => _robots.FindAll(r => r.P.X > mid_x && r.P.Y < mid_y).Count,
        3 => _robots.FindAll(r => r.P.X < mid_x && r.P.Y > mid_y).Count,
        4 => _robots.FindAll(r => r.P.X > mid_x && r.P.Y > mid_y).Count,
        _ => -1
      };
    }
  }
}
