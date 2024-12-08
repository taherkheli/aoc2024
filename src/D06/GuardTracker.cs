namespace aoc.D06
{
  public class GuardTracker
  {
    private Guard? _guard;
    private Point[,] _grid;
    private int _rows;
    private int _cols;

    public GuardTracker(string path)
    {
      var lines = File.ReadAllLines(path);
      _rows = lines.Length;
      _cols = lines[0].Length;
      this._grid = new Point[_rows, _cols];

      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _cols; c++)
        {
          _grid[r, c] = new Point(r, c);

          if (lines[r][c] == '^')
          {
            this._guard = new Guard(Direction.Up, new Position(r, c));
            _grid[r, c].VisitCount++;
          }

          if (lines[r][c] == '#')
            _grid[r, c].Obstructed = true;
        }
      }
    }

    public int Execute(bool isPartII = false) 
    {
      if (isPartII)
        return PartII();
      else
        return PartI();
    }

    private int PartI()
    {
      int result = 0;

      if (Simulate())  //guard exited grid
      {
        for (int r = 0; r < _rows; r++)
          for (int c = 0; c < _cols; c++)
            if (_grid[r, c].VisitCount > 0)
              result++;
      }
      else
        result = -1;

      return result;
    }

    private int PartII()
    {
      int ri = -1;
      int ci = -1;
      int trappedCount = 0;

      //intial psoition
      if (this._guard != null)
      {
        ri = this._guard.Pos.R;
        ci = this._guard.Pos.C;
      }

      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _cols; c++)
        {
          // if already obstructed or initial position
          if ((_grid[r, c].Obstructed) || ((r == ri) && (c == ci)))
            continue;

          // put an obstruction
          _grid[r, c].Obstructed = true;

          if (!Simulate())  //guard trapped
            trappedCount++;

          //reset grid and restore guard's initial position 
          if (this._guard != null)
          {
            _guard.Pos = new Position(ri, ci);
            _guard.Direction = Direction.Up;
          }
          _grid[r, c].Obstructed = false;
        }
      }

      return trappedCount;
    }

    private bool Simulate()
    {
      bool onEdge = false;
      bool exited = false;
      int loopCount = 0;

      if (this._guard != null)
      {
        while(true)
        {
          int r = 0;
          int c = 0;

          switch (this._guard.Direction)
          {
            case Direction.Up:
              if (_guard.Pos.R == 0) //on grid boundary, do NOT look for obstruction 
              {
                r = _guard.Pos.R - 1;
                c = _guard.Pos.C;
                onEdge = true;
              }
              else if (_grid[_guard.Pos.R - 1, _guard.Pos.C].Obstructed)
              {
                _guard.Direction = Direction.Right;
                r = _guard.Pos.R;
                c = _guard.Pos.C + 1;
              }
              else
              {
                r = _guard.Pos.R - 1;
                c = _guard.Pos.C;
              }
              break;

            case Direction.Down:
              if (_guard.Pos.R == _rows - 1) //on grid boundary, do NOT look for obstruction 
              {
                r = _guard.Pos.R + 1;
                c = _guard.Pos.C;
                onEdge = true;
              }
              else if (_grid[_guard.Pos.R + 1, _guard.Pos.C].Obstructed)
              {
                _guard.Direction = Direction.Left;
                r = _guard.Pos.R;
                c = _guard.Pos.C - 1;
              }
              else
              {
                r = _guard.Pos.R + 1;
                c = _guard.Pos.C;
              }
              break;

            case Direction.Left:
              if (_guard.Pos.C == 0) //on grid boundary, do NOT look for obstruction 
              {
                r = _guard.Pos.R;
                c = _guard.Pos.C - 1;
                onEdge = true;
              }
              else if (_grid[_guard.Pos.R, _guard.Pos.C - 1].Obstructed)
              {
                _guard.Direction = Direction.Up;
                r = _guard.Pos.R - 1;
                c = _guard.Pos.C;
              }
              else
              {
                r = _guard.Pos.R;
                c = _guard.Pos.C - 1;
              }
              break;

            case Direction.Right:
              if (_guard.Pos.C == _cols - 1) //on grid boundary, do NOT look for obstruction 
              {
                r = _guard.Pos.R;
                c = _guard.Pos.C + 1;
                onEdge = true;
              }
              else if (_grid[_guard.Pos.R, _guard.Pos.C + 1].Obstructed)
              {
                _guard.Direction = Direction.Down;
                r = _guard.Pos.R + 1;
                c = _guard.Pos.C;
              }
              else
              {
                r = _guard.Pos.R;
                c = _guard.Pos.C + 1;
              }
              break;

            default:
              throw new Exception("should not happen");
          }

          if (onEdge)
            _guard.Pos = new Position(r, c);
          else
          {
            if (!_grid[r, c].Obstructed)
            {
              _grid[r, c].VisitCount++;
              _guard.Pos = new Position(r, c);
            }
          }

          if ( (_guard.Pos.C < 0) || (_guard.Pos.C > _cols - 1) || (_guard.Pos.R < 0) || (_guard.Pos.R > _rows - 1) )
          {
            exited = true;
            break;
          }
          else
          {
            loopCount++;
            if (loopCount > 6000)  //magic number tuned manually. :)
              break;
          }
        }
      }

      return exited;
    }
  }
}
