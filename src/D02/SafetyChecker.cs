namespace aoc.D02
{
  public static class SafetyChecker
  {
    public static int GetSafeRecordsCount(string path, bool isPartII = false)
    {
      int safeCount = 0;
      var lines = File.ReadAllLines(path);

      foreach (var line in lines)
      {
        string[] numbers_s = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] record = new int[numbers_s.Length];

        for (int i = 0; i < numbers_s.Length; i++)
          record[i] = int.Parse(numbers_s[i]);

        if (isPartII)
        {
          if ( (IsIncreasing(record) || IsDecreasing(record)) && DiffIsWithinRange(record) )
            safeCount++;
          else 
          {
            if (RecordIsSafeWithOneOmission(record))
              safeCount++;
          }
        }
        else
        {
          if ( (IsIncreasing(record) || IsDecreasing(record)) && DiffIsWithinRange(record) )
            safeCount++;          
        }
      }

      return safeCount;
    }

    private static bool DiffIsWithinRange(int[] record)
    {
      for (int i = 0; i < record.Length - 1; i++)
      {
        var diff = Math.Abs(record[i] - record[i + 1]);

        if ( (diff < 1) || (diff > 3) )
          return false;
      }

      return true;
    }

    public static bool IsDecreasing(int[] record)
    {
      for (int i = 0; i < record.Length - 1; i++)
      {
        if (record[i] <= record[i+1])        
          return false;        
      }

      return true;
    }

    public static bool IsIncreasing(int[] record)
    {
      for (int i = 0; i < record.Length - 1; i++)
      {
        if (record[i] >= record[i + 1])
          return false;
      }

      return true;
    }

    private static bool RecordIsSafeWithOneOmission(int[] record)
    {
      for (int i = 0; i < record.Length; i++)
      {
        var new_record = new int[record.Length - 1];

        for (int j = 0; j < new_record.Length; j++)
        {
          if (j < i)
            new_record[j] = record[j];
          else  
            new_record[j] = record[j + 1];
        }

        if ((IsIncreasing(new_record) || IsDecreasing(new_record)) && DiffIsWithinRange(new_record)) 
          return true;
      }

      return false;
    }
  }
}
