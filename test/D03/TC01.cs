using aoc.D03;

namespace aoc.test.D03
{
  public class TC01
	{
    [Theory]
    [InlineData(161, "initial.txt", false)]
    [InlineData(189527826, "input.txt", false)]
    [InlineData(48, "initial-II.txt", true)]
    [InlineData(63013756, "input.txt", true)]
    public void D3test(int expected, string input, bool isPartII)
    {
      var actual = Parser.Parse(@"./D03/" + input, isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
