using System.Collections;

namespace CalculatorApp.Test;

public class CalculatorTestData : IEnumerable<object[]>
{
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, 2, 3 },
            new object[] { -4, -6, -10 },
            new object[] { -2, 2, 0 },
            new object[] { int.MinValue, -1, int.MaxValue },
        };
    
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] {1, 2, 3};
        yield return new object[] {-4, -6, -10};
        yield return new object[] {-2, 2, 0};
        yield return new object[] {int.MinValue, -1, int.MaxValue};
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}