namespace Task4;

class Program
{
    static void Main()
    {

        var initialMatrix = new int[12, 12];
        for (int j = 0; j < initialMatrix.GetLength(0); j++)
            for (int k = 0; k < initialMatrix.GetLength(1); k++)
                initialMatrix[j, k] = Random.Shared.Next(-70, 70);

        Console.WriteLine("Initial matrix:");
        PrintMatrix(initialMatrix);
        Console.WriteLine();

        Console.WriteLine("Altered matrix:");
        PrintMatrix(TransformMatrix(initialMatrix));
        Console.WriteLine();

        int[][] jagged = new int[4][];
        jagged[0] = new int[6];
        jagged[1] = new int[2];
        jagged[2] = new int[4];
        jagged[3] = new int[11];

        Console.WriteLine("Initial jagged array:");
        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                jagged[i][j] = Random.Shared.Next(-70, 70);
                Console.Write($"{jagged[i][j],3} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        var seq = GetPositiveSequences(jagged);
        Console.WriteLine("Sequences: ");
        foreach (var sequence in seq)
            Console.WriteLine(string.Join(' ', sequence));
        Console.WriteLine();
        Console.WriteLine($"Average sequence length: {seq.Average(x => x.Length):f2}");

    }

    static void PrintMatrix(int[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
                Console.Write($"{arr[i, j],3} ");
            Console.WriteLine();
        }
    }

    static int[,] TransformMatrix(int[,] array)
    {
        int val = 1;
        for (int i = 0; i < array.GetLength(0); i++)
            for (int j = 0; j < array.GetLength(1); j++)
            {
                bool isOk = (i > j && array.GetLength(1) - j - 1 < i)
                    || (i < j && array.GetLength(1) - j - 1 > i);
                array[i, j] = isOk ? val++ : 0;
            }

        return array;
    }

    static int[][] GetPositiveSequences(int[][] arr)
    {
        var res = new List<int[]>();
        var sequence = new List<int>();

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr[i].Length; j++)
            {
                var value = arr[i][j];
                if (!sequence.Any() && value > 0)
                {
                    sequence.Add(value);
                    continue;
                }

                if (sequence.Any())
                {
                    if (value <= 0)
                    {
                        res.Add(sequence.ToArray());
                        sequence.Clear();
                        continue;
                    }
                    sequence.Add(value);
                }
            }
        }

        return res.ToArray();
    }
}