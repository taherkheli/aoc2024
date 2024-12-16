namespace aoc.D14
{
  public struct Point
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int RobotCount { get; set; }

    public Point(int x, int y)
    {
      this.X = x;
      this.Y = y;
      RobotCount = 0;
    }
  }
}
