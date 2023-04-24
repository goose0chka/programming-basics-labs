using System.Text;
using System.Text.RegularExpressions;

namespace Task1;

internal class Program
{
    static void Main()
    {
        var str = "Lorem Ipsum is simply dummy text of the printing and typesetting industry";
        Console.WriteLine(str);
        Console.WriteLine(Swap12(str));

        var (mostUsedChar, mostUsedCharCount) = Count2(str);
        Console.WriteLine($"Most used character: '{mostUsedChar}' ({mostUsedCharCount})");

        var letterUsage = Count3(str);
        Console.WriteLine("Letter usage:");
        foreach ( var letter in letterUsage )
        {
            Console.WriteLine($"'{letter.Key}' - {letter.Value}");
        }
    }

    static string Swap12(string str)
    {
        var words = str.Split(' ');
        (words[0], words[^1]) = (words[^1], words[0]);
        return string.Join(' ', words);
    }

    static (char, int) Count2(string str)
    {
        return str.GroupBy(x => x)
            .Select(x => (x.Key, x.Count()))
            .MaxBy(x => x.Item2);
    }

    static Dictionary<char, int> Count3(string str)
    {
        var regex = new Regex("[A-Za-z]");
        return str.GroupBy(x => x)
            .Where(x => regex.IsMatch(x.Key.ToString()))
            .OrderByDescending(x => x.Count())
            .ToDictionary(x => x.Key, x => x.Count());
    }
}