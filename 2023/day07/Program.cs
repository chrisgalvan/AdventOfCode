// Puzzle: https://adventofcode.com/2023/day/7

using System.ComponentModel;

internal class Program
{
    private static void Main(string[] args)
    {
        PartOne();
        PartTwo();
    }

    private static void PartOne()
    {
        var ranks = "23456789TJQKA";

        var lines = ReadFile().ToArray();

        var hands = new List<(string hand, int bid, HandType handType, int weigth)>();

        for (var i = 0; i < lines.Count(); i++)
        {
            var line = lines[i].Split(" ");

            var hand = line[0];
            var bid = int.Parse(line[1]);
            var (handType, weigth) = GetHandType(hand, ranks, "");

            hands.Add((hand, bid, handType, weigth));
        }

        var orderedHands = hands.OrderBy(x => x.handType).ThenBy(x => x.weigth).ToArray();

        var total = 0;

        for (var i = 0; i < orderedHands.Count(); i++)
        {
            total += orderedHands[i].bid * (i + 1);
        }

        Console.WriteLine("PartOne solution: " + total);
    }

    private static void PartTwo()
    {
        var ranks = "J23456789TQKA";

        var lines = ReadFile().ToArray();

        var hands = new List<(string hand, int bid, HandType handType, int weigth)>();

        for (var i = 0; i < lines.Count(); i++)
        {
            var line = lines[i].Split(" ");

            var hand = line[0];
            var bid = int.Parse(line[1]);
            var (handType, weigth) = GetHandType(hand, ranks, "J");

            hands.Add((hand, bid, handType, weigth));
        }

        var orderedHands = hands.OrderBy(x => x.handType).ThenBy(x => x.weigth).ToArray();

        var total = 0;

        for (var i = 0; i < orderedHands.Count(); i++)
        {
            total += orderedHands[i].bid * (i + 1);
        }

        Console.WriteLine("PartTwo solution: " + total);
    }

    private static (HandType, int) GetHandType(string hand, string cardsRank, string joker)
    {
        var buckets = new Dictionary<char, BucketType>();

        var newHand = joker == "" ? hand : hand.Replace(joker, "");

        for (var i = 0; i < newHand.Length; i++)
        {
            if (buckets.ContainsKey(newHand[i]))
                buckets[newHand[i]] = (BucketType)buckets[newHand[i]] + 1;
            else
                buckets.Add(newHand[i], BucketType.OneOfAKind);
        }

        var weigth = hand.Select((card,i) => cardsRank.IndexOf(card) << (4*(5-i))).Sum();

        var key = buckets.OrderByDescending(x => x.Value).Select(x => x.Key).FirstOrDefault();
        if (key != Char.MinValue) buckets[key] = (BucketType)buckets[key] + (hand.Length - newHand.Length);

        var handType = buckets switch
        {
            var b when b.Count == 0 => HandType.FiveOfAKind,
            var b when b.Count == 1 => HandType.FiveOfAKind,
            var b when b.Count == 2 && b.ContainsValue(BucketType.FourOfAKind) => HandType.FourOfAKind,
            var b when b.Count == 2 && b.ContainsValue(BucketType.ThreeOfAKind) => HandType.FullHouse,
            var b when b.Count == 3 && b.ContainsValue(BucketType.ThreeOfAKind) => HandType.ThreeOfAKind,
            var b when b.Count == 3 => HandType.TwoPairs,
            var b when b.Count == 4 => HandType.OnePair, 
            _ => HandType.HighCard
        };
        
        return (handType, weigth);
    }

    private static IEnumerable<string> ReadFile() => File.ReadLines(Environment.ProcessPath + @"..\..\..\..\..\input.txt");
}

enum BucketType
{
    OneOfAKind = 1, 
    TwoOfAKind = 2,
    ThreeOfAKind = 3,
    FourOfAKind = 4,
    FiveOfAKind = 5
}

enum HandType 
{
    HighCard = 1,
    OnePair = 2,
    TwoPairs = 3,
    ThreeOfAKind = 4,
    FullHouse = 5,
    FourOfAKind = 6,
    FiveOfAKind = 7,
}