using aoc.D13;

namespace aoc.test.D13
{
  public class TC01
	{
    [Theory]
    [InlineData(480, "initial.txt", false)]
    [InlineData(29877, "input.txt", false)]
    [InlineData(875318608908, "initial.txt", true)]
    [InlineData(99423413811305, "input.txt", true)]
    public void D13test(long expected, string input, bool isPartII)
    {
      var clawMachine = new ClawMachine(@"./D13/" + input);
      var actual = clawMachine.Solve(isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
