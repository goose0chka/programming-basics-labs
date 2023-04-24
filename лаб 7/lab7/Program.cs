namespace lab7;

internal static class Program
{
    const string inputFile = "input.txt";
    const string outputFile = "output.txt";

    static void Main()
    {
        if (!File.Exists(inputFile))
        {
            Console.WriteLine($"File {inputFile} does not exist");
            return;
        }

        var values = File.ReadAllLines(inputFile).OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
        File.WriteAllLines(outputFile, values);
    }
}