namespace aoc.D01
{
  public static class Parser
  {
    public static int Parse(string path, bool isPartII = false)
    {
      var sum = 0;
      var lines = File.ReadAllLines(path);
      var left = new int[lines.Length];
      var right = new int[lines.Length];

      for (int i = 0; i < lines.Length; i++)
      {
        string[] numbers = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        left[i] = Int32.Parse(numbers[0].Trim());
        right[i] = Int32.Parse(numbers[1].Trim());
      }

      if(isPartII)
      {
        for (int i = 0; i < left.Length; i++) 
        {
          int occurences = 0;

          for (int j = 0; j < left.Length; j++)
          {
            if (left[i] == right[j])
              occurences++;
          }

          sum += left[i] * occurences;
        } 
      }
      else
      {
        Array.Sort(left);
        Array.Sort(right);

        for (int i = 0; i < left.Length; i++)
          sum += Math.Abs(left[i] - right[i]);
      }

      return sum;
    }
  }
}
