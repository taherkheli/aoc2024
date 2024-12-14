using aoc.D10;

namespace aoc.test.D10
{
  public class TC01
	{
    [Theory]
    [InlineData(36, "initial.txt", false)]
    [InlineData(638, "input.txt", false)]
    [InlineData(81, "initial-II.txt", true)]
    [InlineData(1289, "input.txt", true)]
    public void D10test(long expected, string input, bool isPartII)
    {
      var p = new PathFinder(@"./D10/" + input);
      var actual = p.GetCount(isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
