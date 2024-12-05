using System.Text.RegularExpressions;

namespace AdventOfCode.Days.Day4;

public static class Day4Runner
{
    public static void Run(string filePath)
    {
        var wordSearch = FileReader.Get(filePath);

        var calc = Part1(wordSearch);
        Console.WriteLine($"Part 1: {calc}");

        calc = Part2(wordSearch);
        Console.WriteLine($"Part 2: {calc}");
    }
    private static int Part1(string wordSearch)
    {
        var horizontalCount = Regex.Count(wordSearch, "XMAS");
        horizontalCount += Regex.Count(wordSearch, "SAMX");
        
        var verticalWordSearch = TurnStringVertical(wordSearch);
        var verticalCount = Regex.Count(verticalWordSearch, "XMAS");
        verticalCount += Regex.Count(verticalWordSearch, "SAMX");
        
        var diagonal1WordSearch = TurnStringDiagonal1(wordSearch);
        var diagonal1Count = Regex.Count(diagonal1WordSearch, "XMAS");
        diagonal1Count += Regex.Count(diagonal1WordSearch, "SAMX");
        
        var diagonal2WordSearch = TurnStringDiagonal2(wordSearch);
        var diagonal2Count = Regex.Count(diagonal2WordSearch, "XMAS");
        diagonal2Count += Regex.Count(diagonal2WordSearch, "SAMX");
        
        return horizontalCount + verticalCount + diagonal1Count + diagonal2Count;
    }
    
    private static int Part2(string wordSearch)
    {
        var maxLength = wordSearch.Split('\n')[0].Length;
        var diagonal1String = TurnStringDiagonal1(wordSearch);
        var diagonal2String = TurnStringDiagonal2(wordSearch);
        
        var diagonal1Matches = Regex.Matches(diagonal1String, "MAS");
        var diagonal1Matches2 = Regex.Matches(diagonal1String, "SAM");

        List <(int x, int y)> diagonal1coords = [];
        foreach (Match match in diagonal1Matches)
        {
            diagonal1coords.Add(GetCenterCoordsFromDiagonal1(match.Index, diagonal1String, maxLength));
        }
        foreach (Match match in diagonal1Matches2)
        {
            diagonal1coords.Add(GetCenterCoordsFromDiagonal1(match.Index, diagonal1String, maxLength));
        }
        
        var diagonal2Matches = Regex.Matches(diagonal2String, "MAS");
        var diagonal2Matches2 = Regex.Matches(diagonal2String, "SAM");
        
        List <(int x, int y)> diagonal2coords = [];
        foreach (Match match in diagonal2Matches)
        {
            diagonal2coords.Add(GetCenterCoordsFromDiagonal2(match.Index, diagonal2String, maxLength));
        }
        foreach (Match match in diagonal2Matches2)
        {
            diagonal2coords.Add(GetCenterCoordsFromDiagonal2(match.Index, diagonal2String, maxLength));
        }

        var count = diagonal1coords.Count(coord => diagonal2coords.Contains<(int x, int y)>(coord));
        
        return count;
    }

    private static (int x, int y) GetCenterCoordsFromDiagonal1(int startIndex, string diagonal1String, int maxLength)
    {
        var totalNumberOfLines = diagonal1String.Count( x => x == '\n');
        var numberOfLines = diagonal1String.Substring(0, startIndex).Count( x => x == '\n');
        var lines = diagonal1String.Split('\n').ToList();
        var countUpToLine = lines.Where((x, i) => i < numberOfLines).Select(x => x.Length).Sum();
        var positionInLine = startIndex - numberOfLines - countUpToLine +1;
        
        // If First Half
        if (numberOfLines <= totalNumberOfLines/2)
        {
            return (positionInLine + 1, numberOfLines + 3 - positionInLine);
        }
        else
        {
            return (numberOfLines - (totalNumberOfLines / 2) + positionInLine + 1, maxLength - positionInLine);
        }
    }

    private static (int x, int y) GetCenterCoordsFromDiagonal2(int startIndex, string diagonal1String, int maxLength)
    {
        var totalNumberOfLines = diagonal1String.Count( x => x == '\n');
        var numberOfLines = diagonal1String.Substring(0, startIndex).Count( x => x == '\n');
        var lines = diagonal1String.Split('\n').ToList();
        var countUpToLine = lines.Where((x, i) => i < numberOfLines).Select(x => x.Length).Sum();
        var positionInLine = startIndex - numberOfLines - countUpToLine +1;
        
        // If First Half
        if (numberOfLines <= totalNumberOfLines / 2)
        {
            return (maxLength - 2 - numberOfLines + positionInLine, positionInLine + 1);
        }
        else
        {
            return ( positionInLine + 1, numberOfLines + 1 - (totalNumberOfLines/2) + positionInLine);
        }
    }

    private static string TurnStringVertical(string wordSearch)
    {
        var lines = wordSearch.Split('\n');

        var verticalWordSearch = new List<string>();
        for(var i=0; i < lines[0].Length; i++)
        {
            verticalWordSearch.Add(string.Concat(lines.Select(x=>x[i])));
            verticalWordSearch[i] += "\n";
        }

        return string.Concat(verticalWordSearch);
    }

    private static string TurnStringDiagonal1(string wordSearch)
    {
        var lines = wordSearch.Split('\n');

        var diagonal1WordSearch = new List<string>();
        for(var i=2; i < lines.Length; i++)
        {
            var charsToAdd = new List<char>();
            for (var y = 0; y <= i; y++)
            {
                charsToAdd.Add(lines[i-y][y]);
            }
            diagonal1WordSearch.Add(string.Concat(charsToAdd));
            diagonal1WordSearch.Add("\n");
        }
        
        for(var i=lines.Length; i > 3; i--)
        {
            var charsToAdd = new List<char>();
            var x = lines.Length-1;
            for (var y = lines.Length - i+1; y < lines[0].Length; y++)
            {
                charsToAdd.Add(lines[x][y]);
                x--;
            }
            
            diagonal1WordSearch.Add(string.Concat(charsToAdd));
            diagonal1WordSearch.Add("\n");
        }

        return string.Concat(diagonal1WordSearch);
    }
    
    private static string TurnStringDiagonal2(string wordSearch)
    {
        var lines = wordSearch.Split('\n');

        var diagonal1WordSearch = new List<string>();
        for(var i=lines[0].Length-3; i >= 0; i--)
        {
            var charsToAdd = new List<char>();
            for (var y = i; y < lines.Length; y++)
            {
                charsToAdd.Add(lines[y - i][y]);
            }
            diagonal1WordSearch.Add(string.Concat(charsToAdd));
            diagonal1WordSearch.Add("\n");
        }
        
        for(var i=1; i < lines.Length - 2; i++)
        {
            var charsToAdd = new List<char>();
            for (var y = 0; y < lines.Length - i; y++)
            {
                charsToAdd.Add(lines[i+y][y]);
            }
            diagonal1WordSearch.Add(string.Concat(charsToAdd));
            diagonal1WordSearch.Add("\n");
        }

        return string.Concat(diagonal1WordSearch);
    }
    
}