using aoc.D09;

namespace aoc.test.D09
{
  public class TC01
	{
    [Theory]
    [InlineData(60, "initial.txt", false)]
    [InlineData(1928, "initial-II.txt", false)]
    [InlineData(6154342787400, "input.txt", false)]
    [InlineData(2858, "initial-II.txt", true)]
    [InlineData(6183632723350, "input.txt", true)]
    public void D9test(long expected, string input, bool isPartII)
    {
      var fragmenter = new Fragmenter(@"./D09/" + input, isPartII);
      var actual = fragmenter.GetChecksum();
      Assert.Equal(expected, actual);
    }
  }
}
