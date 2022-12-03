using Xunit;

namespace CalculatorApp.Test;

public class CalculatorTests
{
    private readonly MathFormulas _math;

    public CalculatorTests()
    {
        _math = new MathFormulas();
    }

    [Fact]
    public void IsEvenTest()
    {
        var result = _math.IsEven(1);
        var result2 = _math.IsEven(2);

        Assert.False(result);
        Assert.True(result2);
    }


    [Theory(Skip = "Because Class and Member are better!")]
    [InlineData(1,2,3)]
    [InlineData(2,3,5)]
    [InlineData(3,2,5)]
    [InlineData(-3,-2,-5)]
    [InlineData(-3,2,-1)]
    public void AddTestTheory(int x, int y, int expected)
    {
        Assert.Equal(expected, _math.Add(x, y));
    }
    [Theory]
    [InlineData(1,2,1)]
    [InlineData(1,3,2)]
    [InlineData(3,1,-2)]
    [InlineData(-3,-1,2)]
    public void DiffTestTheory(int x, int y, int expected)
    {
        Assert.Equal(expected, _math.Diff(x, y));
    }
    [Theory]
    [InlineData(new[]{1,2,3,4,5,6,7},28)]
    [InlineData(new[]{1,2,3},6)]
    public void SumTestTheory(int[] values, int expected)
    {
        Assert.Equal(expected, _math.Sum(values));
    }
    [Theory]
    [InlineData(new[]{1,2,3,4,5,6,7},28/7)]
    [InlineData(new[]{1,2,3},6/3)]
    public void AvgTestTheory(int[] values, int expected)
    {
        Assert.Equal(expected, _math.Average(values));
    }

    [Theory]
    [MemberData(nameof(CalculatorTestData.Data),MemberType = typeof(CalculatorTestData))]
    public void AddTestMemberTheory(int x, int y, int expected)
    {
        Assert.Equal(expected, _math.Add(x, y));
    }

    [Theory]
    [ClassData(typeof(CalculatorTestData))]
    public void AddTestClassTheory(int x, int y, int expected)
    {
        Assert.Equal(expected, _math.Add(x, y));
    }
  
}