using aoc.D17;

namespace aoc.test.D17
{
  public class TC01
	{
    [Theory]
    [InlineData("4,6,3,5,6,3,5,2,1,0", "initial.txt")]
    [InlineData("7,1,3,4,1,2,6,7,1", "input.txt")]
    public void D17test(string expected, string input)
    {
      var computer = new Computer(@"./D17/" + input);
      var actual = computer.PartI();
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(117440, "initial-II.txt")]
    [InlineData(109019476330651, "input.txt")]
    public void D17test2(long expected, string input)
    {
      var computer = new Computer(@"./D17/" + input);
      var actual = computer.PartII();
      Assert.Equal(expected, actual);
    }
  }
}
