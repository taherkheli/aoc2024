using aoc.D08;

namespace aoc.test.D08
{
  public class TC01
	{
    [Theory]
    [InlineData(14, "initial.txt", false)]
    [InlineData(303, "input.txt", false)]
    [InlineData(9, "initial-II.txt", true)]
    [InlineData(34, "initial.txt", true)]
    [InlineData(1045, "input.txt", true)]
    public void D8test(long expected, string input, bool isPartII)
    {
      var tracker = new AntinodeTracker(@"./D08/" + input);
      var actual = tracker.GetAntinodeCount(isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
