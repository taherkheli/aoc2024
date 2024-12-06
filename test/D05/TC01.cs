using aoc.D05;

namespace aoc.test.D05
{
  public class TC01
	{
    [Theory]
    [InlineData(143, "initial.txt", false)]
    [InlineData(5588, "input.txt", false)]
    [InlineData(123, "initial.txt", true)]
    [InlineData(5331, "input.txt", true)]
    public void D5test(int expected, string input, bool isPartII)
    {
      var actual = Printer.AddPageNumsAfterOrdering(@"./D05/" + input, isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
