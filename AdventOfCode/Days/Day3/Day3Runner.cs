using System.Text.RegularExpressions;

namespace AdventOfCode.Days.Day3;

public static class Day3Runner
{
    public static void Run(string filePath)
    {
        var corruptedMemory = FileReader.Get(filePath);

        var calc = Part1(corruptedMemory);
        Console.WriteLine($"Part 1: {calc}");

        calc = Part2(corruptedMemory);
        Console.WriteLine($"Part 2: {calc}");
    }
    private static int Part1(string corruptedMemory)
    {
        const string matchMulsPattern = "mul\\(\\d{1,3},\\d{1,3}\\)";
        var allMuls = Regex.Matches(corruptedMemory, matchMulsPattern);
        return CountMuls(allMuls);
    }

    private static int CountMuls(MatchCollection allMuls)
    {
        var count = 0;
        foreach (var mull in allMuls)
        {
            var number1String = Regex.Match(mull.ToString(), "\\(.*?\\,");
            var number2String = Regex.Match(mull.ToString(), "\\,.*?\\)");

            var number1 = int.Parse(number1String.ToString().Replace("(", "").Replace(",", ""));
            var number2 = int.Parse(number2String.ToString().Replace(")", "").Replace(",", ""));

            count += number1 * number2;
        }
        return count;
    }

    private static int Part2(string corruptedMemory)
    {
        const string matchMullsAndDosAndDontsPattern = "mul\\(\\d{1,3},\\d{1,3}\\)|do\\(\\)|don\\'t\\(\\)";
        var matchMullsAndDosAndDonts = Regex.Matches(corruptedMemory, matchMullsAndDosAndDontsPattern);

        var doCount = true;
        var count = 0;
        foreach (var mull in matchMullsAndDosAndDonts)
        {
            if (mull.ToString() == "do()")
            {
                doCount = true;
                continue;
            }
            if (mull.ToString() == "don't()")
            {
                doCount = false;
                continue;
            }

            if (doCount)
            {
                var number1String = Regex.Match(mull.ToString(), "\\(.*?\\,");
                var number2String = Regex.Match(mull.ToString(), "\\,.*?\\)");

                var number1 = int.Parse(number1String.ToString().Replace("(", "").Replace(",", ""));
                var number2 = int.Parse(number2String.ToString().Replace(")", "").Replace(",", ""));

                count += number1 * number2;
            }
        }
        return count;
    }
    
}