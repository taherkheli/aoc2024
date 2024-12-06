namespace aoc.D05
{
  public static class Printer
  {
    public static int AddPageNumsAfterOrdering(string path, bool isPartII = false)
    {
      int sum = 0;
      var lines = File.ReadAllLines(path);
      var rules = new List<PageRule>();
      var updates = new List<int[]>();
      bool rulesEnded = false;

      //parse input
      foreach (var line in lines)
      {
        if (!rulesEnded)
        {
          if (string.IsNullOrWhiteSpace(line))
          {
            rulesEnded = true;
            continue;
          }
          else   //parse rules
          {
            var pageNums = line.Split(new char[] { '|' }, StringSplitOptions.TrimEntries);
            var rule = new PageRule(int.Parse(pageNums[0]), int.Parse(pageNums[1]));
            rules.Add(rule);
          }
        }
        else //parse updates
        {
          var nums = line.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
          var update = new int[nums.Length];

          for (int i = 0; i < nums.Length; i++)
            update[i] = int.Parse(nums[i]);

          updates.Add(update);
        }
      }

      foreach (var update in updates)
      {
        var violationObserved = false;

        for (int i = 0; i < update.Length - 1; i++)
        {
          if (ViolationOccured(update[i], update[i + 1], rules))
          {
            violationObserved = true;
            break;
          }
        }

        if (isPartII)
        {
          if (violationObserved)
            sum += FixOrderThenCalculate(update, rules);
        }
        else
        {
          if (!violationObserved)
            sum += update[update.Length / 2];
        }
      }
      return sum;
    }

    private static int FixOrderThenCalculate(int[] update, List<PageRule> rules)
    {
      var violationObserved = false;

      do
      {
        violationObserved = false;

        for (int i = 0; i < update.Length - 1; i++)
        {
          if (ViolationOccured(update[i], update[i + 1], rules))
          {
            var temp = update[i];
            update[i] = update[i + 1];
            update[i + 1] = temp;
            violationObserved = true;
            break;
          }
        }
      }
      while (violationObserved);

      return update[update.Length / 2];
    }

    private static bool ViolationOccured(int p_before, int p_after, List<PageRule> rules)
    {
      foreach (var rule in rules)
      {
        if ((rule.Before == p_before) && (rule.After == p_after))   //satifies rule
          return false;

        if ((rule.After == p_before) && (rule.Before == p_after))  //clear violation
          return true;
      }

      return false;  //no obvious violations
    }
  }
}
