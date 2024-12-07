namespace aoc.D06
{
  public class Guard
  {
    private Direction _direction; 
    private Position _position; 

    public Guard(Direction dir, Position pos)
    {
      this._direction = dir;
      this._position = pos;
    }

    public Direction Direction
    {
      get { return this._direction; }
      set { this._direction = value; }
    }

    public Position Pos
    {
      get { return this._position; }
      set { this._position = value; }
    }
  }
}