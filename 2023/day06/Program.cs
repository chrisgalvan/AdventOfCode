// Puzzle: https://adventofcode.com/2023/day/6

internal class Program
{
    private static void Main(string[] args)
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var input = ReadFile().ToArray();

        var races = new List<(int time, int recordDistance)>();

        var times = input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        var recordDistances = input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

        for(var i = 0; i < times.Count(); i++)
        {
            races.Add(new (times[i], recordDistances[i]));
        }

        var total = 1;
        foreach (var race in races)
        {
            var waysToWin = 0;
            for (var hold = 1; hold < race.time; hold++)
            {
                var distance = (race.time - hold) * hold;
                if (distance > race.recordDistance)
                    waysToWin++;
            }

            total *= waysToWin;
        }

        Console.WriteLine("PartOne solution: " + total);
    }

    private static void PartTwo()
    {
        var input = ReadFile().ToArray();

        var time = long.Parse(input[0].Split(':')[1].Replace(" ", ""));
        var recordDistance = long.Parse(input[1].Split(':')[1].Replace(" ", ""));

        var waysToWin = 0;
        for (var hold = 1; hold < time; hold++)
        {
            var distance = (time - hold) * hold;
            if (distance > recordDistance)
                waysToWin++;
        }

        Console.WriteLine("PartTwo solution: " + waysToWin);
    }

    private static IEnumerable<string> ReadFile() => File.ReadLines(Environment.ProcessPath + @"..\..\..\..\..\input.txt");
}