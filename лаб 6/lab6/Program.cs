namespace lab6;

internal class Program
{
    static void Main()
    {

        Console.WriteLine("Lupyna FIT 1-6");
        Enum.GetNames<Products>()
            .Select((x, i) => string.Format("{0} - {1}", i + 1, x))
            .ToList()
            .ForEach(Console.WriteLine);

        Console.Write("Order: ");
        var str = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(str))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        var flags = str
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new { Parsed = int.TryParse(x, out var val), Value = val })
            .Where(x => x.Parsed)
            .Select(x => (int)Math.Pow(2, x.Value - 1))
            .Where(x => Enum.IsDefined(typeof(Products), x))
            .Cast<Products>();

        if (!flags.Any())
        {
            Console.WriteLine("Invalid input");
            return;
        }

        var groups = flags
            .Select(x => new { Value = x, Type = x >= Products.LogisticsSystem1 ? "Logistics systems" : "Automation systems" })
            .GroupBy(x => x.Type);


        foreach (var group in groups)
        {
            var val = group.Select(x => x.Value).Aggregate((x, y) => x | y);
            Console.WriteLine($"{group.Key}: {val}");
        }
    }
}

[Flags]
enum Products
{
    AutomationSystem1 = 0b000001,
    AutomationSystem2 = 0b000010,
    AutomationSystem3 = 0b000100,
    LogisticsSystem1 = 0b001000,
    LogisticsSystem2 = 0b010000,
    LogisticsSystem3 = 0b100000,
}