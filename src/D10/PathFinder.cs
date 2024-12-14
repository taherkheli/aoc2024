namespace aoc.D10
{
  public class PathFinder
  {
    private readonly List<Point> _trailheads;
    private readonly List<Point> _peaks;
    private readonly Point[,] _grid;
    private readonly int _rows;
    private readonly int _cols;

    public PathFinder(string path)
    {
      _trailheads = new List<Point>();
      _peaks = new List<Point>();
      var lines = File.ReadAllLines(path);
      _rows = lines.Length;
      _cols = lines[0].Length;
      this._grid = new Point[_rows, _cols];

      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _cols; c++)
        {
          var h = int.Parse(lines[r][c].ToString());
          var p = new Point(r, c, h);
          _grid[r, c] = p;

          if (p.H == 0)
            _trailheads.Add(p);

          if (p.H == 9)
            _peaks.Add(p);
        }
      }
    }

    public int GetCount(bool isPartII)
    {
      int sum = 0;
      int score = 0;

      foreach (var trail in _trailheads)
      {
        score = 0;

        foreach (var peak in _peaks)
        {
          if (isPartII)
              score+= GetDistinctPathsCount(trail, peak);
          else
          {
            if (IsTrailReachable(trail, peak))
              score++;
          }          
        }

        sum += score;
      }

      return sum;
    }

    private bool IsTrailReachable(Point trail, Point peak)
    {
      var stack = new Stack<Point>();
      var current = trail;
      ResetGrid();
      _grid[current.R, current.C].Visited = true;

      while (true)
      {        
        if (current.H == peak.H && current.R == peak.R && current.C == peak.C) //hit intended peak
          return true;
        else
        {
          if (current.H == 9)  //hit an unintended peak
          {
            _grid[current.R, current.C].Visited = true;

            if (stack.Count > 0)
            {
              current = stack.Pop();
              _grid[current.R, current.C].Visited = true;
              continue;
            }
            else
              return false;
          }
        }

        var options = GetOptions(current);

        if (options.Count == 0)
        {
          if (stack.Count == 0)
            return false;
          else 
          {
            current = stack.Pop();
            _grid[current.R, current.C].Visited = true;
            continue;
          }
        }
        else  //update current to first option and push the rest on stack
        {
          current = options[0];
          _grid[current.R, current.C].Visited = true;

          for (var i = 1; i < options.Count; i++)
            stack.Push(options[i]);          
        }
      }
    }

    private int GetDistinctPathsCount(Point trail, Point peak)
    {
      var result = new List<List<Point>>();
      var stack = new Stack<List<Point>>();
      var path = new List<Point>();
      var current = trail;
      ResetGrid();
      _grid[current.R, current.C].Visited = true;
      path.Add(current);

      while (true)
      {
        if (current.H == peak.H && current.R == peak.R && current.C == peak.C) //hit intended peak
        {
          result.Add(path);
          ResetGrid();

          if (stack.Count > 0)
          {
            path = stack.Pop();
            current = path.Last();
            _grid[current.R, current.C].Visited = true;
            continue;
          }
          else
            break;
        }
        else
        {
          if (current.H == 9)  //hit an unintended peak
          {
            _grid[current.R, current.C].Visited = true;

            if (stack.Count > 0)
            {
              path = stack.Pop();
              current = path.Last();
              _grid[current.R, current.C].Visited = true;
              continue;
            }
            else
              break;
          }
        }

        var options = GetOptions(current);

        if (options.Count == 0)
        {
          if (stack.Count > 0)
          {
            path = stack.Pop();
            current = path.Last();
            _grid[current.R, current.C].Visited = true;
            continue;
          }
          else
            break;
        }
        else  //update path to include first option and push the rest of the posisble paths
        {
          for (var i = 1; i < options.Count; i++)
          {
            var possiblePath = new List<Point>(path.Select(p => p));
            possiblePath.Add(options[i]);
            stack.Push(possiblePath);
          }

          path.Add(options.First());
          current = path.Last();
          _grid[current.R, current.C].Visited = true;
        }
      }

      return result.Count;
    }

    private List<Point> GetOptions(Point p)
    {
      var options = new List<Point>();
      int r, c;

      //back
      r = p.R;
      c = p.C - 1;

      if (_IsWithinBounds(r, c))
        if (_grid[r, c].H - p.H == 1)
          if (!_grid[r, c].Visited)
            options.Add(_grid[r, c]);

      //up
      r = p.R - 1;
      c = p.C;
      if (_IsWithinBounds(r, c))
        if (_grid[r, c].H - p.H == 1)
          if (!_grid[r, c].Visited)
            options.Add(_grid[r, c]);

      //forward
      r = p.R;
      c = p.C + 1;

      if (_IsWithinBounds(r, c))
        if (_grid[r, c].H - p.H == 1)
          if (!_grid[r, c].Visited)
            options.Add(_grid[r, c]);

      //down
      r = p.R + 1;
      c = p.C;
      if (_IsWithinBounds(r, c))
        if (_grid[r, c].H - p.H == 1)
          if (!_grid[r, c].Visited)
            options.Add(_grid[r, c]);

      return options;
    }

    private bool _IsWithinBounds(int r, int c)
    {
      return !((c < 0) || (c > _cols - 1) || (r < 0) || (r > _rows - 1));
    }

    private void ResetGrid()
    {
      for (int r = 0; r < _rows; r++)
        for (int c = 0; c < _cols; c++)
          _grid[r, c].Visited = false;
    }
  }
}
