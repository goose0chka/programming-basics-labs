using static System.Math;

namespace Task2;

internal class Program
{
    const double Precision = 1e-6;
    static void Main(string[] args)
    {
        Console.WriteLine("Sum of first 10 elements: {0:0.000000}", CalculateFirst10());
        var value = Calculate();
        Console.WriteLine("Sum with 1e-6 precision: {0:0.000000} ({1} elements calculated)", value.Item1, value.Item2);
    }

    static double Function(int n)
        => Pow(-1, n) / (Pow(n, 2) + Pow(2, n));

    static double CalculateFirst10()
        => Enumerable.Range(1, 10).Aggregate(.0, (x, y) => x += Function(y));

    static (double, int) Calculate()
    {
        var i = 1;
        var res = .0;

        double value;
        do
        {
            value = Function(i);
            res += value;
            i++;
        } while (Abs(value) > Precision);

        return (res, i);
    }
}