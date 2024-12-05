using aoc.D02;

namespace aoc.test.D02
{
  public class TC01
	{
    [Theory]
    [InlineData(2, "initial.txt", false)]
    [InlineData(639, "input.txt", false)]
    [InlineData(4, "initial.txt", true)]
    [InlineData(674, "input.txt", true)]
    public void D1test(int expected, string input, bool isPartII)
    {
      var actual = SafetyChecker.GetSafeRecordsCount(@"./D02/" + input, isPartII);
      Assert.Equal(expected, actual);
    }
  }
}
