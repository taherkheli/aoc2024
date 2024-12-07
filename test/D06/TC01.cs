using aoc.D06;

namespace aoc.test.D06
{
  public class TC01
	{
    [Theory]
    [InlineData(41, "initial.txt", false)]
    [InlineData(4939, "input.txt", false)]
    [InlineData(6, "initial.txt", true)]
    [InlineData(1434, "input.txt", true)]
    public void D6test(int expected, string input, bool isPartII)
    {
      var tracker = new GuardTracker(@"./D06/" + input);
      var actual = tracker.Execute(isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
