namespace aoc.D12
{
  public struct Tile
  {
    public (int, int) Pos;
    public char Type;

    public Tile((int, int) pos, char type)
    {
      Pos = pos;
      Type = type;
    }
  }
}
