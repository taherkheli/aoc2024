namespace aoc.D05
{
  public struct PageRule
  {
    public int Before { get; set; }
    public int After { get; set; }

    public PageRule(int before, int after)
    {
      Before = before;
      After = after;
    }
  }
}
