namespace Task1;

internal class Program
{
    static void Main()
    {
        Console.Write("Input value: ");
        if (!long.TryParse(Console.ReadLine(), out var value))
        {
            Console.WriteLine("Input is not an integer");
            return;
        }

        Console.WriteLine("Sum of number's digits: {0}", DigitSum(value));
    }

    public static int DigitSum(long value)
    {
        var res = 0;
        while (value > 0)
        {
            res += (int)(value % 10);
            value /= 10;
        }
        return res;
    }
}