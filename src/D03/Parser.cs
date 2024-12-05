using System.Text.RegularExpressions;

namespace aoc.D03
{
  public static class Parser
  {
    public static int Parse(string path, bool isPartII = false)
    {
      int sum = 0;
      var text = File.ReadAllText(path);

      if (isPartII) 
      {  
        string pattern = @"do\(\)|don\'t\(\)|mul\((\d{1,3}),(\d{1,3})\)";
        MatchCollection matches = Regex.Matches(text, pattern);
        bool enabled = true;
        
        foreach (Match match in matches)
        {
          if (enabled)
          {
            if (!(match.Value == "do()" || match.Value == @"don't()"))            
              sum += ExecuteMultiply(match.Value);            
            else
            {
              if (match.Value == @"don't()")
                enabled = false;
            }        
          }
          else //disabled 
          {
            if (match.Value == @"do()")
              enabled = true;
          }
        }
      }
      else
      {
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";        
        MatchCollection matches = Regex.Matches(text, pattern);

        foreach (Match match in matches)
          sum += ExecuteMultiply(match.Value);        
      }

      return sum;
    }

    private static int ExecuteMultiply(string value)
    {
      var s = value.Trim(new char[] { 'm', 'u', 'l', '(' }).TrimEnd(')');
      var numbers = s.Split(',');
      return int.Parse(numbers[0]) * int.Parse(numbers[1]);
    }
  }
}
