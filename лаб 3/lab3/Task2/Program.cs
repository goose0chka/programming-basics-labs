namespace Task2;

internal class Program
{
    static void Main()
    {
        var initialArray = Enumerable.Range(0, 30)
            .Select(_ => Random.Shared.Next(0, 100))
            .ToArray();

        var copy = new int[initialArray.Length];

        Console.WriteLine("Initial array:");
        PrintArray(initialArray);
        Console.WriteLine();

        Console.WriteLine("Sorted array (Bubble Sort): ");
        initialArray.CopyTo(copy, 0);
        PrintArray(BubbleSort(copy));
        Console.WriteLine();

        Console.WriteLine("Sorted array (Insertion Sort): ");
        initialArray.CopyTo(copy, 0);
        PrintArray(InsertionSort(initialArray));
    }

    static void PrintArray(int[] arr)
    {
        for (int i = 0; i < arr.Length;)
        {
            Console.Write($"{arr[i],2} ");
            i++;

            if (i % 10 == 0)
            {
                Console.WriteLine();
            }
        }
    }

    static int[] BubbleSort(int[] data)
    {
        for (int i = 1; i < data.Length; i++)
        {
            for (int j = 0; j < data.Length - i; j++)
            {
                if (data[j] > data[j + 1])
                {
                    (data[j + 1], data[j]) = (data[j], data[j + 1]);
                }
            }
        }

        return data;
    }

    static int[] InsertionSort(int[] data)
    {
        for (int i = 1; i < data.Length; i++)
        {
            int key = data[i];
            int j = i - 1;

            while (j >= 0 && key < data[j])
            {
                data[j + 1] = data[j];
                --j;
            }

            data[j + 1] = key;
        }

        return data;
    }
}