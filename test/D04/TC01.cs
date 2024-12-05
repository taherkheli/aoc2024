using aoc.D04;

namespace aoc.test.D04
{
  public class TC01
	{
    [Theory]
    [InlineData(18, "initial.txt", false)]
    [InlineData(2633, "input.txt", false)]
    [InlineData(9, "initial.txt", true)]
    [InlineData(1936, "input.txt", true)]
    public void D4test(int expected, string input, bool isPartII)
    {
      var actual = Xmas.GetCount(@"./D04/" + input, isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
