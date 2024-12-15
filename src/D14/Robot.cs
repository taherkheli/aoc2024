namespace aoc.D14
{
  public struct Robot
  {
    public Point P { get; set; }
    public Point V;

    public Robot(Point p, Point v)
    {
      P = p;
      V = v;
    }
  }
}
