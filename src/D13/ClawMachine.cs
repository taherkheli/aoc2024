namespace aoc.D13
{
  public class ClawMachine
  {
    private List<((long, long), (long, long), (long, long))> _machines = new List<((long, long), (long, long), (long, long))>();

    public ClawMachine(string path)
    {
      var lines = File.ReadAllLines(path);
      (long, long) A = (-1, -1);
      (long, long) B = (-1, -1);
      (long, long) P = (-1, -1);
      long x, y;
      string[] nums;
      long j = 0;

      foreach (var line in lines)
        switch (j)
        {
          case 0:
            nums = line.Split(':')[1].Split(',');
            x = long.Parse(nums[0].Split('+')[1].Trim());
            y = long.Parse(nums[1].Split('+')[1].Trim());
            A = (x, y);
            j++;
            break;

          case 1:
            nums = line.Split(':')[1].Split(',');
            x = long.Parse(nums[0].Split('+')[1].Trim());
            y = long.Parse(nums[1].Split('+')[1].Trim());
            B = (x, y);
            j++;
            break;

          case 2:
            nums = line.Split(':')[1].Split(',');
            x = long.Parse(nums[0].Split('=')[1].Trim());
            y = long.Parse(nums[1].Split('=')[1].Trim());
            P = (x, y);
            j++;
            break;

          case 3:
            _machines.Add((A, B, P));
            j = 0;
            break;
        }
    }

    public long Solve(bool isPartII = false)
    {
      long sum = 0;
      var wins = new List<(long, long, long)>();  //index, a, b

      if (isPartII)
      {
        for (int i = 0; i < _machines.Count; i++)
        {
          var px = _machines[i].Item3.Item1 + 10000000000000;
          var py = _machines[i].Item3.Item2 + 10000000000000;
          var a = _machines[i].Item1;
          var b = _machines[i].Item2;
          _machines[i] = (a, b, (px, py));
        }
      }

      for (int i = 0; i < _machines.Count; i++)
      {     
        var a1 = _machines[i].Item1.Item1;
        var a2 = _machines[i].Item1.Item2;
        var b1 = _machines[i].Item2.Item1;
        var b2 = _machines[i].Item2.Item2;
        var c1 = _machines[i].Item3.Item1;
        var c2 = _machines[i].Item3.Item2;
        long x = -1;
        long y = -1;
        var d = a1 * b2 - b1 * a2;  //determinant  https://en.wikipedia.org/wiki/Cramer%27s_rule   

        if (d != 0)  //solvable
        {
          var temp1 = c1 * b2 - b1 * c2;
          var temp2 = a1 * c2 - c1 * a2;

          if ((temp1) % d == 0)
            x = temp1 / d;

          if ((temp2) % d == 0)
            y = temp2 / d;

          if ((x != -1) && (y != -1))
            wins.Add((i, x, y));
        }
      }

      foreach (var win in wins)
        sum += win.Item2 * 3 + win.Item3;

      return sum;
    }
  }
}
