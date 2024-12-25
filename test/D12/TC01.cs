using aoc.D12;

namespace aoc.test.D12
{
  public class TC01
	{
    [Theory]
    [InlineData(1930, "initial.txt", false)]
    [InlineData(1533644, "input.txt", false)]
    [InlineData(1206, "initial.txt", true)]
    [InlineData(936718, "input.txt", true)]
    public void D12test(long expected, string input, bool isPartII)
    {
      var garden = new Garden(@"./D12/" + input);
      var actual = garden.GetFencingCosts(isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
