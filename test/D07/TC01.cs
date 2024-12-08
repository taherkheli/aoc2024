using aoc.D07;

namespace aoc.test.D07
{
  public class TC01
	{
    [Theory]
    [InlineData(3749, "initial.txt", false)]
    [InlineData(20281182715321, "input.txt", false)]
    [InlineData(11387, "initial.txt", true)]
    [InlineData(159490400628354, "input.txt", true)]
    public void D7test(long expected, string input, bool isPartII)
    {
      var actual = Equations.Solve(@"./D07/" + input, isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
