

using System.Text.RegularExpressions;

var parseLine = (string line) =>
{
    var parts = line.Split("=");
    var nextNodes = parts[1].Trim().Replace("(", "").Replace(")", "").Split(",");

    return new KeyValuePair<string, (string left, string rigth)>(parts[0].Replace(" ", ""), (nextNodes[0].Replace(" ", ""), nextNodes[1].Replace(" ", "")));
};

var getMap = (IEnumerable<string> input, string startingNodeRegex) => 
{
    var map = new Dictionary<string, (string left, string right)>();
    var startingNodes = new List<string>();
    var regex = new Regex(startingNodeRegex);

    foreach (var line in input)
    {
        var item = parseLine(line);
        map.Add(item.Key, item.Value);
        if (regex.IsMatch(item.Key))
            startingNodes.Add(item.Key);
    }

    return (map, startingNodes.ToArray());
};

var factor = (int number) => 
{
    var factors = new List<int>();

    int max = (int)Math.Sqrt(number);

    for (int factor = 1; factor <= max; ++factor)
    {
        if (number % factor == 0)
        {
            factors.Add(factor);
            if (factor != number / factor)
                factors.Add(number / factor);
        }
    }

    return factors;
};

var getStepCount = (string[] input, string startingNode, string endingNode) => 
{
    var instructions = input[0];
    var (map, locations) = getMap(input.Skip(2), startingNode);
    var steps = new List<int>();

    for (var i = 0; i < locations.Length; i++)
    {
        var location = locations[i];
        var instructionCount = 0;
        var endingLocationFound = false;

        while(!endingLocationFound)
        {
            var instruction = instructions[instructionCount % instructions.Length];

            location = instruction == 'L' ? map[location].left : map[location].right;

            instructionCount++;

            endingLocationFound = Regex.IsMatch(location, endingNode);
        }

        steps.Add(instructionCount);
    }

    var lcm = steps.SelectMany(x => factor(x).Where(w => w != x)).Select(x => (long)x).Distinct();
    return steps.Count == 1 ? steps.First() : lcm.Aggregate((x, y) => x*y) ;
};

var input = File.ReadAllLines("input.txt");

var solution1 = getStepCount(input, "AAA", "ZZZ");
Console.WriteLine("PartOne solution: " + solution1);

var solution2 = getStepCount(input, "..A", "..Z");
Console.Write("PartTwo solution: " + solution2);