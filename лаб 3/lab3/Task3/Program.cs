namespace Task3;

class Program
{
    static void Main()
    {
        const int size = 120;
        var arr = Enumerable.Range(0, size)
            .Select(_ => Random.Shared.Next(-70, 70))
            .ToArray();

        Console.WriteLine("Initial array");
        for (int i = 0; i < arr.Length;)
        {
            Console.Write($"{arr[i],3} ");
            i++;

            if (i % 15 == 0)
            {
                Console.WriteLine();
            }
        }
        Console.WriteLine();

        var seq = GetPositiveSequences(arr);

        Console.WriteLine("Sequences: ");
        Console.WriteLine("First Last Length Sequence");
        foreach (var sequence in seq)
        {
            Console.WriteLine($"{sequence.Offset,-5} " +
                $"{sequence.Offset + sequence.Count - 1,-4} " +
                $"{sequence.Count,-6} " +
                $"{string.Join(' ', sequence)}");
        }
        Console.WriteLine();
        Console.WriteLine($"Average sequence length: {seq.Average(x => x.Count):f2}");
    }

    static List<ArraySegment<int>> GetPositiveSequences(int[] arr)
    {
        var sequences = new List<ArraySegment<int>>();
        int sequenceStart = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (sequenceStart == -1 && arr[i] > 0)
            {
                sequenceStart = i;
                continue;
            }

            if (sequenceStart != -1 && arr[i] <= 0)
            {
                sequences.Add(new(arr, sequenceStart, i - sequenceStart));
                sequenceStart = -1;
            }
        }
        return sequences;
    }
}