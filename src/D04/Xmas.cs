using System.Text;

namespace aoc.D04
{
  public static class Xmas
  {
    public static int GetCount(string path, bool isPartII = false)
    {
      int sum = 0;
      var lines = File.ReadAllLines(path);
      int rows = lines.Length;
      int cols = lines[0].Length;
      var chars = new char[rows, cols];

      for (int r = 0; r < rows; r++)      
        for (int c = 0; c < cols; c++)
          chars[r, c] = lines[r][c];

      if (isPartII )
      {
        for (int r = 0; r < rows; r++)
        {
          for (int c = 0; c < cols; c++)
          {
            if (chars[r, c] != 'A')
              continue;
            else
            {
              bool leg1Found = false;
              bool leg2Found = false;

              //2 & 6
              if ( ((c - 1) > -1) && ((r - 1) > -1) && ((r + 1) < rows) && ((c + 1) < cols) )
              {
                var topLeft = chars[r - 1, c -1 ];
                var bottomRight = chars[r + 1, c + 1];

                if ( (topLeft == 'M' && bottomRight == 'S') || (topLeft == 'S' && bottomRight == 'M') )
                  leg1Found = true;
              }              

              //4 & 8
              if ( ((r - 1) > -1) && ((c + 1) < cols) && ((r + 1) < rows) && ((c - 1) > -1) )
              {
                var topRight = chars[r - 1, c + 1];
                var bottomLeft = chars[r + 1, c - 1];

                if ((topRight == 'M' && bottomLeft == 'S') || (topRight == 'S' && bottomLeft == 'M'))
                  leg2Found = true;
              }

              if (leg1Found && leg2Found)
                sum++;
            }
          }
        }
      }
      else
      {
        StringBuilder sb;

        for (int r = 0; r < rows; r++)
        {
          for (int c = 0; c < cols; c++)
          {
            if (chars[r, c] != 'X')
              continue;
            else
            {
              //1
              if ((c - 3) > -1)
              {
                sb = new StringBuilder();

                for (int i = c; i > c - 4; i--)
                  sb.Append(chars[r, i]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //2
              if (((c - 3) > -1) && ((r - 3) > -1))
              {
                sb = new StringBuilder();

                for (int i = 0; i < 4; i++)
                  sb.Append(chars[r - i, c - i]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //3
              if ((r - 3) > -1)
              {
                sb = new StringBuilder();

                for (int i = r; i > r - 4; i--)
                  sb.Append(chars[i, c]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //4
              if (((r - 3) > -1) && ((c + 3) < cols))
              {
                sb = new StringBuilder();

                for (int i = 0; i < 4; i++)
                  sb.Append(chars[r - i, c + i]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //5
              if ((c + 3) < cols)
              {
                sb = new StringBuilder();

                for (int i = c; i < c + 4; i++)
                  sb.Append(chars[r, i]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //6
              if (((r + 3) < rows) && ((c + 3) < cols))
              {
                sb = new StringBuilder();

                for (int i = 0; i < 4; i++)
                  sb.Append(chars[r + i, c + i]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //7
              if ((r + 3) < rows)
              {
                sb = new StringBuilder();

                for (int i = r; i < r + 4; i++)
                  sb.Append(chars[i, c]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }

              //8
              if (((r + 3) < rows) && ((c - 3) > -1))
              {
                sb = new StringBuilder();

                for (int i = 0; i < 4; i++)
                  sb.Append(chars[r + i, c - i]);

                if (sb.ToString() == "XMAS")
                  sum++;
              }
            }
          }
        }
      }
            
      return sum;
    }
  }
}
