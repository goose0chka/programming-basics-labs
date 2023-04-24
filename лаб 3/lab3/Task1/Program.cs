namespace Task1;

class Program
{
    static void Main()
    {
        var arr = Enumerable.Range(0, 50)
            .Select(_ => Random.Shared.Next(-1000, 1000) / 100.0)
            .ToArray();

        Console.WriteLine("Initial array:");
        PrintArray(arr);
        Console.WriteLine();

        Console.WriteLine($"Sum of positive numbers: {SumPos(arr):f2}");
        Console.WriteLine("Product of elements between the absolute minimum " +
            $"and absolute maximum: {MinMaxProduct(arr):f2}");
        Console.WriteLine();

        Console.WriteLine("Sorted array:");
        PrintArray(SortByDescending(arr));
    }

    static void PrintArray(double[] arr)
    {
        for (int i = 0; i < arr.Length;)
        {
            Console.Write($"{arr[i],5} ");
            i++;

            if (i % 10 == 0)
            {
                Console.WriteLine();
            }
        }
    }

    static double SumPos(double[] data)
        => data.Where(x => x > 0).Sum();

    static double MinMaxProduct(double[] data)
    {
        var absIndex = data.Select((x, i) => new { Value = Math.Abs(x), Index = i });
        var min = absIndex.MinBy(x => x.Value)!;
        var max = absIndex.MaxBy(x => x.Value)!;

        if (min.Index > max.Index)
        {
            (min, max) = (max, min);
        }

        var range = data[(min.Index + 1)..max.Index];
        return range.Length == 0 ? 0 : range.Aggregate(1.0, (x, y) => x *= y);
    }

    static double[] SortByDescending(double[] data)
    {
        for (int i = 1; i < data.Length; i++)
        {
            for (int j = 0; j < data.Length - i; j++)
            {
                if (data[j] < data[j + 1])
                {
                    (data[j + 1], data[j]) = (data[j], data[j + 1]);
                }
            }
        }

        return data;
    }
}