using System.Linq;

namespace CalculatorApp;

public class MathFormulas
{
    public bool IsEven(int number) => number % 2 == 0;

    public int Diff(int x, int y) => y - x;

    public int Add(int x, int y) => x + y;

    public int Sum(params int[] values)
    {
        return values.Sum();
    }

    public double Average(params int[] values)
    {
        var sum = values.Sum();

        return sum / values.Length;
    }



  
}