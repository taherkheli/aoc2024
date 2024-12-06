using aoc.D06;

namespace aoc.test.D06
{
  public class TC01
	{
    [Theory]
    [InlineData(41, "initial.txt")]
    [InlineData(4939, "input.txt")]
    public void D6test(int expected, string input)
    {
      var tracker = new GuardTracker(@"./D06/" + input);
      var actual = tracker.PartI();
      Assert.Equal(expected, actual);
    }
  }
}
