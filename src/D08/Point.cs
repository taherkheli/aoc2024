namespace aoc.D08
{
  public struct Point
  {
    public int r;
    public int c;
    public bool hasAntenna;
    public char frequency;
    public bool isAntinode;

    public Point(int r, int c, bool hasAntenna = false, char frequency = '.')
    {
      this.r = r;
      this.c = c;
      this.hasAntenna = hasAntenna;
      this.frequency = frequency;
    }
  }
}
