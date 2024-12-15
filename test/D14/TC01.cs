using aoc.D14;

namespace aoc.test.D14
{
  public class TC01
	{
    [Theory]
    [InlineData(12, "initial.txt", false)]
    [InlineData(225648864, "input.txt", false)]
    [InlineData(7847, "input.txt", true)]
    public void D14test(long expected, string input, bool isPartII)
    {
      var restroom = new Restroom(@"./D14/" + input);
      var actual = restroom.Solve(isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
