// Puzzle: https://adventofcode.com/2023/day/11

var input = File.ReadAllLines("input.txt");

var solve = (string[] input, int expand) => 
{
    var emptyRows = new HashSet<int>();
    foreach (var i in Enumerable.Range(0, input.Length))
        emptyRows.Add(i);

    var emptyCols = new HashSet<int>();
    foreach (var i in Enumerable.Range(0, input[0].Length))
        emptyCols.Add(i);

    List<(int x, int y)> galaxies = new();

    for (var y = 0; y < input.Length; y++)
    {
        for (var x = 0; x < input[y].Length; x++)
        {
            if (input[y][x] == '#')
            {
                galaxies.Add((x, y));
                emptyRows.Remove(y);
                emptyCols.Remove(x);
            }
        }
    }

    long total = 0;

    for (var i = 0; i < galaxies.Count; i++)
    {
        var galaxy1 = galaxies[i];

        for (var j = i + 1; j < galaxies.Count; j++)
        {
            var galaxy2 = galaxies[j];

            var extraRows = emptyRows.Where(x => x > Math.Min(galaxy1.y, galaxy2.y) && x < Math.Max(galaxy1.y, galaxy2.y)).ToArray();
            var extraCols = emptyCols.Where(x => x > Math.Min(galaxy1.x, galaxy2.x) && x < Math.Max(galaxy1.x, galaxy2.x)).ToArray();

            total += Math.Abs(galaxy1.x - galaxy2.x) + (extraCols.Length * expand) +
                    Math.Abs(galaxy1.y - galaxy2.y) + (extraRows.Length * expand);
        }
    }

    return total;
};

Console.WriteLine("PartOne solution: " + solve(input, 1));
Console.WriteLine("PartTwo solution: " + solve(input, 999_999));