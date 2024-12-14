namespace aoc.D10
{
  public struct Point
  {
    public int R;
    public int C;
    public int H;
    public bool Visited;

    public Point(int r, int c, int h)
    {
      R = r;
      C = c;
      H = h;
      Visited = false;
    }
  }
}
