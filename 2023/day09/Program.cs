// Puzzle: https://adventofcode.com/2023/day/9

var input = File.ReadAllLines("input.txt");

var lines = input.Select(x => x.Split(" ").Select(x => int.Parse(x)));

var predictHistory = (IEnumerable<int> line) => 
{
    var values = line.Select(x => x);
    var lastNumbers = new List<int>() { line.Last() };

    while(!values.All(x => x == 0))
    {
        var listNumbers = new List<int>();

        for(var i = 1; i < values.Count(); i++)
        {
            var number = values.ElementAt(i) - values.ElementAt(i - 1);
            listNumbers.Add(number);
        }
        
        values = listNumbers;
        lastNumbers.Add(listNumbers.Last());
    }

    return lastNumbers.Aggregate((x , y) => x + y);
};

var findHistory = (IEnumerable<IEnumerable<int>> lines) => 
{
    long partOneTotal = 0, partTwoTotal = 0;

    foreach (var line in lines)
    {
        partOneTotal += predictHistory(line);
        partTwoTotal += predictHistory(line.Reverse());
    }
    
    return (partOneTotal, partTwoTotal);
};

var (solution1, solution2 ) = findHistory(lines);

Console.WriteLine("PartOne solution: " + solution1);
Console.WriteLine("PartTwo solution: " + solution2);