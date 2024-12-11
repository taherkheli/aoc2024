namespace aoc.D09
{
  public class Block
  {
    public bool IsFree;
    public int FileId;

    public Block(bool isFree, int fileId = -1)
    {
      IsFree = isFree;
      FileId = fileId;
    }
  }
}
