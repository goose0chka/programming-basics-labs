using System.Text;

namespace Task4;

internal class Program
{
    static void Main()
    {
        Console.Write("Target: ");
        if (!int.TryParse(Console.ReadLine(), out var value))
        {
            Console.WriteLine("Target is not an integer");
            return;
        }

        Analyze(value);
    }

    static void Analyze(int value)
    {
        for (int tens = 0; tens <= value / 10; tens++)
            for (int fives = 0; fives <= (value - 10 * tens) / 5; fives++)
                for (int twos = 0; twos <= (value - 10 * tens - 5 * fives) / 2; twos++)
                {
                    int ones = value - 10 * tens - 5 * fives - 2 * twos;

                    var values = new[] { (1, ones), (2, twos), (5, fives), (10, tens) }
                        .Where(x => x.Item2 != 0)
                        .Select((x) => $"{x.Item1} * {x.Item2}");

                    var str = new StringBuilder()
                        .Append(value)
                        .Append(" = ")
                        .AppendJoin(" + ", values);

                    Console.WriteLine(str);
                }
    }
}