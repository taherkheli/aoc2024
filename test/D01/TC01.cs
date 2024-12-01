using aoc.D01;

namespace aoc.test.D01
{
  public class TC01
	{
    [Theory]
    [InlineData(11, "initial.txt", false)]
    [InlineData(3574690, "input.txt", false)]
    [InlineData(31, "initial.txt", true)]
    [InlineData(22565391, "input.txt", true)]
    public void D1test(int expected, string input, bool isPartII)
    {
      var actual = Parser.Parse(@"./D01/" + input, isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
