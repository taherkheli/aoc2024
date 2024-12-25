namespace aoc.D12
{
  public struct Garden
  {
    private Tile[,] _grid;
    private List<char> _types;
    private int _rows;
    private int _cols;

    public Garden(string path)
    {
      var lines = File.ReadAllLines(path);
      _rows = lines.Length;
      _cols = lines[0].Length;
      _grid = new Tile[_rows, _cols];
      _types = new List<char>();

      for (int r = 0; r < _rows; r++)
        for (int c = 0; c < _cols; c++)
          _grid[r, c] = new Tile((r, c), lines[r][c]);

      _types = GetTypes();
    }

    public int GetFencingCosts(bool isPartII = false)
    {
      int price = 0;

      if (isPartII)
      {
        foreach (var t in _types)
          price += GetPricePartII(GetDistinctRegions(t));
      }
      else
      {
        foreach (var t in _types)
          price += GetPrice(GetDistinctRegions(t));
      }

      return price;
    }

    private List<List<Tile>> GetDistinctRegions(char type)
    {
      var result = new List<List<Tile>>();
      var directions = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
      var visited = new HashSet<Tile>();

      for (int r = 0; r < _rows; r++)
      {
        for (int c = 0; c < _cols; c++)
        {
          if ((_grid[r, c].Type == type) && (!visited.Contains(_grid[r, c])))
            {
            var region = new List<Tile>();
            var stack = new Stack<Tile>();
            Tile curr;
            stack.Push(_grid[r, c]);

            while (stack.Count > 0)
            {
              curr = stack.Pop();
              visited.Add(_grid[curr.Pos.Item1, curr.Pos.Item2]);

              region.Add(_grid[curr.Pos.Item1, curr.Pos.Item2]);
              var matchingNeighbors = new HashSet<Tile>();

              foreach (var dir in directions)
              {
                int rnew = curr.Pos.Item1 + dir.Item1;
                int cnew = curr.Pos.Item2 + dir.Item2;

                if (IsWithinBounds(rnew, cnew) && (_grid[r, c].Type == _grid[rnew, cnew].Type) && !visited.Contains(_grid[rnew, cnew]))
                  matchingNeighbors.Add(_grid[rnew, cnew]);  
              }

              foreach (var item in matchingNeighbors)
                if (!stack.Contains(item))  //avoid duplicates
                  stack.Push(item);
            }

            if (region.Count > 0)
              result.Add(region);
          }
        }
      }

      return result;
    }

    //what does it cost to fence all regions of a particular type of tile
    private int GetPrice(List<List<Tile>> regions)
    {
      int area = 0;
      int perimeter = 0;
      int cost = 0;
      var directions = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

      foreach (var region in regions)
      {
        area = region.Count;
        perimeter = 0;

        foreach (var tile in region)
        {
          int count = 0;

          foreach (var dir in directions)
          {
            int rnew = tile.Pos.Item1 + dir.Item1;
            int cnew = tile.Pos.Item2 + dir.Item2;

            if (!IsWithinBounds(rnew, cnew) || (_grid[rnew, cnew].Type != tile.Type))
              count++;
          }

          perimeter += count;
        }

        cost += area * perimeter;
      }

      return cost;
    }

    private int GetPricePartII(List<List<Tile>> regions)
    {
      int area = 0;
      int perimeter = 0;
      int cost = 0;
      var corner1 = ((0, -1), (-1, -1), (-1, 0));
      var corner2 = ((0, 1), (-1, 1), (-1, 0));
      var corner3 = ((0, 1), (1, 1), (1, 0));
      var corner4 = ((0, -1), (1, -1), (1, 0));
      var corners = new ((int, int), (int, int), (int, int))[] { corner1, corner2, corner3, corner4 };

      foreach (var region in regions)
      {
        area = region.Count;
        perimeter = 0;

        foreach (var tile in region)
        {
          foreach (var corner in corners)
          {
            int r1 = tile.Pos.Item1 + corner.Item1.Item1;
            int c1 = tile.Pos.Item2 + corner.Item1.Item2;
            int r2 = tile.Pos.Item1 + corner.Item2.Item1;
            int c2 = tile.Pos.Item2 + corner.Item2.Item2;
            int r3 = tile.Pos.Item1 + corner.Item3.Item1;
            int c3 = tile.Pos.Item2 + corner.Item3.Item2;
            var threePoints = ((r1, c1), (r2, c2), (r3, c3));

            if (ThreePointsFormAnEdge(threePoints, tile.Type))
              perimeter++;
          }
        }

        cost += area * perimeter;
      }

      return cost;
    }

    //what types of tiles do we have in this garden
    private List<char> GetTypes()
    {
      var result = new List<char>();
      var alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                                  'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
      for (int i = 0; i < alphabet.Length; i++)
      {
        int count = 0;

        for (int r = 0; r < _rows; r++)
          for (int c = 0; c < _cols; c++)
            if (_grid[r, c].Type == alphabet[i])
              count++;

        if (count > 0)
          result.Add(alphabet[i]);
      }

      return result;
    }

    private bool IsWithinBounds(int r, int c)
    {
      return !((r < 0) || (c < 0) || (r > _rows - 1) || (c > _cols - 1));
    }

    private bool ThreePointsFormAnEdge(((int, int), (int, int), (int, int)) threePoints, char type)
    {
      var r1 = threePoints.Item1.Item1;
      var c1 = threePoints.Item1.Item2;
      var r2 = threePoints.Item2.Item1;
      var c2 = threePoints.Item2.Item2;
      var r3 = threePoints.Item3.Item1;
      var c3 = threePoints.Item3.Item2;

      // if r1 and r3 are out => outer edge
      if ((!IsWithinBounds(r1, c1) || _grid[r1, c1].Type != type) && (!IsWithinBounds(r3, c3) || _grid[r3, c3].Type != type))
        return true;

      // if r1 and r3 are in, but r2 is not => inner edge
      if ((IsWithinBounds(r1, c1) && _grid[r1, c1].Type == type) &&
          (IsWithinBounds(r3, c3) && _grid[r3, c3].Type == type) &&
          (IsWithinBounds(r2, c2) && _grid[r2, c2].Type != type))
        return true;

      return false;
    }
  }
}