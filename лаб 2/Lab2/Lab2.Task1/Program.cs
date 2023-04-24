namespace Lab2.Task1;

class Program
{
    static void Main()
    {
        Console.Write("x: ");
        var xStr = Console.ReadLine();
        if (!int.TryParse(xStr, out int x))
        {
            Console.WriteLine($"{xStr} is not an integer");
            return;
        }

        var y = x switch
        {
            < 0 => Func(x),
            > 21 => Product(x),
            _ => Sum(x)
        };

        Console.WriteLine($"y = {y}");
    }

    private static double Func(int x)
    {
        var top = Math.Pow(x, 2) + 3 * x + 2;
        var bottom = x;
        var y = top / bottom;
        return y;
    }

    private static double Sum(int x)
    {
        var sum = .0;
        for (var i = 0; i <= x; i++)
        {
            var next = Math.Pow(7 + i, 7);
            sum += next;
        }
        var y = 13 - sum;
        return y;
    }

    private static double Product(int x)
    {
        var res = 1.0;
        for (var i = 22; i <= x; i++)
        {
            var next = Math.Pow(i - 4, 3);
            res *= next;
        }
        var y = -2 * res;
        return y;
    }
}