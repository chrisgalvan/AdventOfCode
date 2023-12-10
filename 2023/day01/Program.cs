// Puzzle: https://adventofcode.com/2023/day/1

using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne() 
    {
        var lines = ReadFile();

        var total = 0;

        foreach (var line in lines)
        {

            int first;
            for (var i = 0; i < line.Length; i++)
            {
                if (int.TryParse(line.Substring(i, 1), out first))
                {
                    total += first * 10;
                    break;
                }
            }

            int last;
            for (var j = line.Length; j > 0; j--)
            {
                if (int.TryParse(line.Substring(j - 1, 1), out last))
                {
                    total += last;
                    break;
                }
            }
        }

        Console.WriteLine("PartOne solution: " + total);
    }

    private static void PartTwo()
    {
        var numbersAsText = new Dictionary<int, string>() {{ 1, "one" }, { 2, "two"}, { 3, "three"}, { 4, "four"}, { 5, "five"}, { 6, "six"}, { 7, "seven"}, {8, "eight"}, {9, "nine"} };
        var lines = ReadFile();

        var fulltotal = 0;
        var lineIndex  = 0;

        foreach (var line in lines)
        {
            var firstNumber = 0;
            var secondNumber = 0;

            lineIndex++;

            int first;
            var shouldContinue = true;
            var i = 0;
            while (shouldContinue && i < line.Length) 
            {
                if (int.TryParse(line.Substring(i, 1), out first))
                {
                    firstNumber += first * 10;
                    shouldContinue = false;
                    break;
                }
                else 
                {
                    foreach(var number in numbersAsText) {
                        if (i + number.Value.Length <= line.Length && line.Substring(i, number.Value.Length) == number.Value) {
                            firstNumber += number.Key * 10;
                            shouldContinue = false;
                            break;
                        }
                    }
                }

                i++;
            }

            int last;
            shouldContinue = true;
            var j = line.Length;
            while (shouldContinue && j > 0)
            {
                if (int.TryParse(line.Substring(j - 1, 1), out last))
                {
                    secondNumber += last;
                    shouldContinue = false;
                    break;
                }
                else 
                {
                    foreach(var number in numbersAsText) {
                        if (j - number.Value.Length > 0 && line.Substring(j - number.Value.Length, number.Value.Length) == number.Value) {
                            secondNumber += number.Key;
                            shouldContinue = false;
                            break;
                        }
                    }
                }

                j--;
            }

            fulltotal += firstNumber + secondNumber;
        }

        Console.WriteLine("PartTwo solution: " + fulltotal);
    }

    private static IEnumerable<string> ReadFile() => File.ReadLines("input.txt");
}