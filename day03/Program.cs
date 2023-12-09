using System.Text.RegularExpressions;

internal class Program
{
    static List<char> symbols = new List<char>() { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '=', '+', '-', '?', '/' };

    private static void Main(string[] args)
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var lines = ReadFile().ToArray();
        var total = 0;

        for (var r = 0; r < lines.Length; r++) 
        {
            var matches = FindNumbers(lines[r]);

            var linesToCheck = new List<string>();

            if (r - 1 > 0) linesToCheck.Add(lines[r - 1]);

            linesToCheck.Add(lines[r]);

            if (r + 1 < lines.Length) linesToCheck.Add(lines[r + 1]);

            for (var i = 0; i < matches.Count; i++ )
            {
                var indexOf = matches[i].Index;
                var length = matches[i].Length;

                var verify = VerifyLines(linesToCheck, indexOf, length);

                if (verify) { 
                    total += int.Parse(matches[i].Value);
                }
            }
        }

        Console.WriteLine("PartOne solution: " + total);
    }

    private static void PartTwo()
    {
        var lines = ReadFile().ToArray();
        var total = 0;

        for (var r = 0; r < lines.Length; r++) 
        {
            var matches = FindEngines(lines[r]);

            var linesToCheck = new List<string>();

            if (r - 1 >= 0) linesToCheck.Add(lines[r - 1]);

            linesToCheck.Add(lines[r]);

            if (r + 1 < lines.Length) linesToCheck.Add(lines[r + 1]);


            for (var i = 0; i < matches.Count; i++)
            {
                var indexOf = matches[i].Index;

                var numbers = FindAdjacentNumbers(linesToCheck, indexOf);

                if (numbers.Count > 1) {
                    total += numbers.Aggregate(1, (total, next) =>  total * next);
                }
            }
        }

        Console.WriteLine("PartTwo solution: " + total);
    }

    private static IEnumerable<string> ReadFile() => File.ReadLines(Environment.ProcessPath + @"..\..\..\..\..\input.txt");

    private static MatchCollection FindNumbers(string s)
    {
        var regex = new Regex(@"\d+");
        return regex.Matches(s);
    }

    private static bool VerifyLines (List<string> lines, int indexOf, int length)
    {
        bool foundAdjacentSymbol = false;

        foreach (var line in lines)
        {
            var i = indexOf == 0 ? 0 : indexOf - 1;
            var l = indexOf + length + 1 > line.Length ? line.Length - indexOf : length + 2;

            var s = line.Substring(i, l);

            if (FindSymbol(s)) {
                foundAdjacentSymbol = true;
                break;
            }
        }

        return foundAdjacentSymbol;
    }

    private static bool FindSymbol(string s)
    {
        var symbolFound = false;
        foreach (var symbol in symbols)
        {
            if (s.Contains(symbol))
            {
                symbolFound = true;
                break;
            }
        }

        return symbolFound;
    }

    private static MatchCollection FindEngines(string s)
    {
        var regex = new Regex(@"\*");
        return regex.Matches(s);
    }

    private static List<int> FindAdjacentNumbers(List<string> lines, int indexOf)
    {
        var regex = new Regex(@"\d+");
        var numbers = new List<int>();

        foreach (var line in lines)
        {
            var matches = regex.Matches(line);

            for (var i = 0; i < matches.Count; i++)
            {

                if (
                    (matches[i].Index <= indexOf - 1 && indexOf <= matches[i].Index + matches[i].Length) ||
                    (matches[i].Index <= indexOf + 1 && indexOf + 1 <= matches[i].Index + matches[i].Length)
                )
                {
                    numbers.Add(int.Parse(matches[i].Value));
                }
            }
        }

        return numbers;
    }
}