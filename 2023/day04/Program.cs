using System.Runtime.InteropServices.Marshalling;

internal class Program
{
    private static void Main(string[] args)
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var cards = ReadFile();

        var total = 0;

        foreach (var card in cards)
        {
            var indexOf = card.IndexOf(":") + 1;

            var sets = card.Substring(indexOf).Split('|');

            var winningSet = sets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numberSet = sets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var number_bit = "";

            foreach(var number in numberSet)
            {
                if (winningSet.Contains(number))
                {
                    if (number_bit == "") number_bit = "1";
                    else number_bit += "0";
                }
            }

            if (number_bit != "") total += Convert.ToInt32(number_bit, 2);
        }

        Console.WriteLine("PartOne solution " + total);
    }

    private static void PartTwo()
    {
        var cards = ReadFile().ToList();

        int[] totalCards = new int[cards.Count];

        for(var i = 0; i < cards.Count; i++) 
        {
            totalCards[i] += 1;

            var card = cards[i];
            var indexOf = card.IndexOf(':') + 1;
            var sets = card.Substring(indexOf).Split('|');
            var winningSet = sets[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numberSet = sets[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var count = 0;
            foreach(var number in numberSet)
            {
                if (winningSet.Contains(number)) count++;
            }

            for (var j = 1; j <= count; j++)
            {
                totalCards[i + j] += 1 * totalCards[i];
            }
        }

        Console.WriteLine("PartTwo solution: " + totalCards.Aggregate(0, (total, next) => total + next));
    }

    private static IEnumerable<string> ReadFile() =>  File.ReadLines(Environment.ProcessPath + @"..\..\..\..\..\input.txt");
}