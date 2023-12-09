internal class Program
{
    private static void Main(string[] args)
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var games = ReadFile();

        var allowed = new Dictionary<string, int>() {{"red", 12}, {"green", 13}, {"blue", 14}};

        var gameIndex = 1;
        var total = 0;

        foreach (var game in games)
        {
            var i = game.IndexOf(':') + 2;

            var isGameValid = true;

            foreach (var set in game.Substring(i).Split(';'))
            {
                var cubes = set
                    .Split(',')
                    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => new KeyValuePair<string, string>(x[1], x[0]))
                    .ToList();

                foreach (var cube in cubes)
                {
                    var maxColorAllowed = allowed[cube.Key];

                    if (int.Parse(cube.Value) > maxColorAllowed) 
                    {
                        isGameValid = false;
                        break;
                    }
                }
            }

            if (isGameValid) total += gameIndex;

            gameIndex++;
        }

        Console.WriteLine("Solution PartOne: " + total);
    }

    private static void PartTwo()
    {
        var games = ReadFile();

        var total = 0;

        foreach (var game in games)
        {
            var i = game.IndexOf(':') + 2;

            var minValues = new Dictionary<string, int>();

            foreach (var set in game.Substring(i).Split(';'))
            {
                var cubes = set
                    .Split(',')
                    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => new KeyValuePair<string, int>(x[1], int.Parse(x[0])))
                    .ToList();

                foreach (var cube in cubes)
                {
                    if (minValues.ContainsKey(cube.Key))
                    {
                        if (minValues[cube.Key] < cube.Value) minValues[cube.Key] = cube.Value;
                    }
                    else 
                    {
                        minValues.Add(cube.Key, cube.Value);
                    }
                }
            }

            var gameTotal = 1;

            foreach (var value in minValues)
            {
                gameTotal *= value.Value; 
            }

            total += gameTotal;
        }

        Console.WriteLine("Solution PartTwo: " + total);
    }

    private static IEnumerable<string> ReadFile() => 
        File.ReadLines(Environment.ProcessPath + @"..\..\..\..\..\input.txt");

}