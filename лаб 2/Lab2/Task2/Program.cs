namespace Lab2.Task2;

class Program
{
    static void Main()
    {
        Console.Write("Count: ");
        var cStr = Console.ReadLine();
        if (!int.TryParse(cStr, out int c))
        {
            Console.WriteLine($"{cStr} is not an integer");
            return;
        }

        if (c < 1)
        {
            Console.WriteLine("Error: x must be 1 or greater");
            return;
        }

        var f0 = 0;
        var f1 = 1;

        for (var i = 0; i < c; i++)
        {
            Console.Write($"{f1} ");
            var next = f0 + f1;
            f0 = f1;
            f1 = next;
        }

        Console.WriteLine();
    }
}
