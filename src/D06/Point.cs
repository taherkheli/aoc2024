namespace aoc.D06
{
  public class Point
  {
    private int _r;
    private int _c;
    private int _visitCount;
    private bool _obstructed;

    public Point(int r, int c)
    {
      this._r = r;
      this._c = c;
      this._visitCount = 0;
      this._obstructed = false;
    }

    public int R
    {
      get { return this._r; }
      set { this._r = value; }
    }

    public int C
    {
      get { return this._c; }
      set { this._c = value; }
    }

    public int VisitCount
    {
      get { return this._visitCount; }
      set { this._visitCount = value; }
    }

    public bool Obstructed
    {
      get { return this._obstructed; }
      set { this._obstructed = value; }
    }
  }
}