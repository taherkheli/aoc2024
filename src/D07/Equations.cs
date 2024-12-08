namespace aoc.D07
{
  public static class Equations
  {
    public static long Solve(string path, bool isPartII = false)
    {
      var lines = File.ReadAllLines(path);
      var calcs = new Calculation[lines.Length];
      long sum = 0;

      for (int i = 0; i < lines.Length; i++)
      {
        var strings = lines[i].Split(':');
        long lhs = Int64.Parse(strings[0].Trim());
        var arr_s = strings[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var operands = new long[arr_s.Length];

        for (int j = 0; j < arr_s.Length; j++)
          operands[j] = Int64.Parse(arr_s[j]);

        calcs[i] = new Calculation(lhs, operands);
      }

      for (int i = 0; i < calcs.Length; i++)  //one calc at a time
      {
        var ops = GetPermutations(calcs[i].operands.Length - 1, isPartII); //get relevant ops combos 

        for (int j = 0; j < ops.Length; j++)    //apply one op combo at a time
        {
          var operands = calcs[i].operands;     //current calc i.e. array of operands
          var operators = ops[j];               //current ops i.e. array of ops combos 

          long result = operands[0];

          for (int k = 1; k < operands.Length; k++) //apply current ops
          {
            if (isPartII) 
            {
              if (operators[k - 1] == '+')
                result += operands[k];
              else if (operators[k - 1] == '*')
                result *= operands[k];
              else // '|'
                result = Int64.Parse((result.ToString() + operands[k].ToString()));              
            }
            else
            {
              if (operators[k - 1] == '+')
                result += operands[k];
              else
                result *= operands[k];
            }
          }

          if (result == calcs[i].lhs) //check if its a true equation.
          {
            sum += calcs[i].lhs;
            break;
          }
        }
      }

      return sum;
    }

    private static char[][] GetPermutations(int x, bool isPartII = false)     //x = num of ops i.e. operands count - 1
    {
      char[] values;

      if (isPartII)
        values = ['+', '*', '|'];
      else
        values = ['+', '*'];

      int n = values.Length;
      int permutations = (int)Math.Pow(n, x);    //possible permutations n^x
      var result = new char[permutations][];

      char[] placeholders;

      for (int i = 0; i < permutations; i++)
      {
        int temp = i;
        placeholders = new char[x];

        for (int j = x - 1; j >= 0; j--)
        {
          placeholders[j] = values[temp % n];
          temp /= n;
        }

        result[i] = placeholders;
      }

      return result;
    }
  }
}
