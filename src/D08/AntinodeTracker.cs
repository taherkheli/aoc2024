namespace aoc.D08
{
  public class AntinodeTracker
  {
    private Point[,] _grid;
    private int _rows;
    private int _cols;

    public AntinodeTracker(string path)
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

          if (lines[r][c] != '.')
          {
            _grid[r, c].hasAntenna = true;
            _grid[r, c].frequency = lines[r][c];
          }
        }
      }
    }

    public int GetAntinodeCount(bool isPartII = false)
    {
      int sum = 0;
      var frequencies = new List<char>();

      //generate list of distinct freqs
      for (int r = 0; r < _rows; r++)
        for (int c = 0; c < _cols; c++)
          if (_grid[r, c].hasAntenna)
          {
            var f = _grid[r, c].frequency;

            if (!(frequencies.Contains(f)))
              frequencies.Add(f);
          }

      //mark antinodes for one frequency at a time
      foreach (var f in frequencies)
      {
        var pointsOfInterest = new List<Point>();

        //genrate POI's for this f
        for (int r = 0; r < _rows; r++)
          for (int c = 0; c < _cols; c++)
            if (_grid[r, c].frequency == f)
              pointsOfInterest.Add(_grid[r, c]);

        for (int i = 0; i < pointsOfInterest.Count; i++)
        {
          var p1 = pointsOfInterest[i];

          for (int j = i + 1; j < pointsOfInterest.Count; j++)
          {
            var p2 = pointsOfInterest[j];
            int r1, r2, c1, c2;
            var deltaR = p2.r - p1.r;
            var deltaC = p2.c - p1.c;

            if(isPartII)
            {
              foreach (var p in pointsOfInterest)    //in partII, all POI's are antinodes anyway
                _grid[p.r, p.c].isAntinode = true;
            }

            if ( (p1.r < p2.r) && (p1.c < p2.c) )  // r & c both increasing => -ve slope
            {
              r1 = p1.r - Math.Abs(deltaR); 
              c1 = p1.c - Math.Abs(deltaC);
              r2 = p2.r + Math.Abs(deltaR);
              c2 = p2.c + Math.Abs(deltaC);

              while (_IsWithinBounds(r1, c1))
              {
                _grid[r1, c1].isAntinode = true;
                r1 -= Math.Abs(deltaR);
                c1 -= Math.Abs(deltaC);

                if (!(isPartII))
                  break;
              }

              while (_IsWithinBounds(r2, c2))
              {
                _grid[r2, c2].isAntinode = true;
                r2 += Math.Abs(deltaR);
                c2 += Math.Abs(deltaC);

                if (!(isPartII))
                  break;
              }
            }
            else if ( (p1.r < p2.r) && (p1.c > p2.c) )  // r increasing & c decreasing => +ve slope
            {
              r1 = p1.r - Math.Abs(deltaR);
              c1 = p1.c + Math.Abs(deltaC);
              r2 = p2.r + Math.Abs(deltaR);
              c2 = p2.c - Math.Abs(deltaC);

              while (_IsWithinBounds(r1, c1))
              {
                _grid[r1, c1].isAntinode = true;
                r1 -= Math.Abs(deltaR);
                c1 += Math.Abs(deltaC);

                if (!(isPartII))
                  break;
              }

              while (_IsWithinBounds(r2, c2))
              {
                _grid[r2, c2].isAntinode = true;
                r2 += Math.Abs(deltaR);
                c2 -= Math.Abs(deltaC);

                if (!(isPartII))
                  break;
              }
            }
          }
        }
      }

      // find count of antinodes
      for (int r = 0; r < _rows; r++)
        for (int c = 0; c < _cols; c++)
          if (_grid[r, c].isAntinode)
            sum++;

      return sum;
    }

    private bool _IsWithinBounds(int r, int c)
    {
      return !((c < 0) || (c > _cols - 1) || (r < 0) || (r > _rows - 1));
    }
  }
}
