// Puzzle: https://adventofcode.com/2023/day/5

using System.Diagnostics;
using System.Linq;

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

        long[] valuesToMap = lines[0].Substring(7).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Int64.Parse(x)).ToArray();

        var maps = GetMaps(lines);

        foreach (var map in maps)
        {
            valuesToMap = ProcessMap(valuesToMap, map);
        }

        Console.WriteLine("PartOne solution: " + valuesToMap.Min());
    }

        private static void PartTwo()
    {
        var lines = ReadFile();

        var seeds = lines[0].Substring(7).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();

        var ranges = new List<(long from, long to)>();
        for (var i = 0; i < seeds.Length; i += 2)
            ranges.Add((from: seeds[i], to: seeds[i] + seeds[i + 1] -1));

        var maps = GetMaps(lines);

        foreach (var map in maps)
        {
            var orderedMap = map.OrderBy(x => x.Source);

            var newRanges = new List<(long from, long to)>();

            foreach(var r in ranges)
            {
                var range = r;

                foreach(var mapping in orderedMap)
                {
                    if (range.from < mapping.Source)
                    {
                        newRanges.Add((range.from, Math.Min(range.to, mapping.Source - 1)));
                        range.from = mapping.Source;
                        if (range.from > range.to) 
                            break;
                    }

                    if (range.from <= mapping.SourceEnd)
                    {
                        newRanges.Add((range.from + mapping.Adjustment, Math.Min(range.to, mapping.SourceEnd) + mapping.Adjustment));
                        range.from = mapping.SourceEnd + 1;
                        if (range.from > range.to)
                            break;
                    }
                }

                if (range.from <= range.to)
                    newRanges.Add(range);
            }

            ranges = newRanges;
        }

        Console.WriteLine("PartTwo solution: " + ranges.Min(r => r.from));
    }

    private static List<List<Map>> GetMaps(List<string> lines)
    {
        List<List<Map>> allMaps = new();

        List<Map> currentMap = new();

        for (var i = 1; i < lines.Count; i++)
        {
            if (lines[i] == String.Empty) continue;

            if (lines[i].Contains("map")) {
                currentMap = new();
                allMaps.Add(currentMap);
                continue;
            }

            var line = lines[i].Split(' ').Select(x => long.Parse(x)).ToArray();

            currentMap.Add(new Map { 
                Destination = line[0], 
                Source = line[1], 
                Range = line[2],
                Adjustment = line[0] - line[1]
                // To = line[1] + line[2] - 1
            });
        }

        return allMaps;
    }

    private static long[] ProcessMap(long[] valuesToMap, List<Map> currentMap)
    {
        for (var i = 0; i < valuesToMap.Length; i++)
        {
            foreach (var map in currentMap) 
            {   
                if (valuesToMap[i] >= map.Source && valuesToMap[i] <= map.Source + map.Range)
                {
                    var dest = map.Destination + (valuesToMap[i] - map.Source);
                    valuesToMap[i] = dest;
                    break;
                }
            }
        }

        return valuesToMap;
    }

    private static List<string> ReadFile() => File.ReadLines(Environment.ProcessPath + @"..\..\..\..\..\input.txt").ToList();
}

internal struct Map
{
    internal long Destination {get; set;}
    internal long Source {get;set;}
    internal long Range {get;set;}
    internal long SourceEnd => Source + Range - 1;
    internal long Adjustment { get;set;}
}

internal struct Range
{
    internal long Start {get;set;}
    internal long End => Start + Length;
    internal long Length {get;set;}
}