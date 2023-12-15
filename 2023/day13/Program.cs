// Puzzle: https://adventofcode.com/2023/day/13

var input = File.ReadAllText("input.txt").Split("\r\n\r\n").Select(x => x.Split("\r\n"));

var transpose = (string[] lines) => 
{
    List<string> temp = new();
    for (var col = 0; col < lines[0].Length; col++)
    {
        var rowVal = "";
        for (var row = 0; row < lines.Length; row++)
        {
            rowVal += lines[row][col];
        }
        temp.Add(rowVal);
    }

    return temp.ToArray();
};

var checkMirror = (string a, string b, int smudgesAllowed) =>
{
    int matches = Enumerable.Range(0, a.Length).Count(x => a[x] == b[x]);

    if (matches == a.Length) return (true, 0);
    if (matches + smudgesAllowed == a.Length) return (true, smudgesAllowed);

    return (false, 0);
};

var checkPattern = (string[] lines, int smudgesAllowed) =>
{
    var mirror_row = -1;
    var smudgesUsed = 0;

    for (var row = 0; row < lines.Length - 1; row++)
    {
        var (isMirror, smudges) = checkMirror(lines[row], lines[row+1], smudgesAllowed);
        smudgesUsed += smudges;

        if (isMirror && smudgesUsed == smudgesAllowed)
        {
            mirror_row = row;

            var i = 1;

            while (true)
            {
                var before = mirror_row - i * 1;
                var after = mirror_row + 1 + i;

                if (before < 0 || after >= lines.Length) break;

                if (lines[before] != lines[after])
                {
                    mirror_row = -1;
                    break;
                }
                i++;
            }

            if (mirror_row != -1) break;
        }
    }

    return mirror_row + 1;
};



var solve = (ushort smudgesAllowed) =>
{
    var total = 0;

    foreach (var pattern in input)
    {
        var col = checkPattern(transpose(pattern), smudgesAllowed);

        if (col > 0) total += col;
        else
        {
            var row = checkPattern(pattern, smudgesAllowed);
            if (row > 0)
                total += 100 * row;
        }
    }

    return total;
};

Console.WriteLine("PartOne solution: " + solve(0));
Console.WriteLine("PartTwo solution: " + solve(1));