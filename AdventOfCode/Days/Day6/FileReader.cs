using StreamReader = System.IO.StreamReader;

namespace AdventOfCode.Days.Day6;

public static class FileReader
{
    public static List<List<char>> Get(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var file = reader.ReadToEnd();
        
        var lines = file.Split("\n").ToList();
        
        
        return lines.Select(line => line.ToCharArray().ToList()).ToList();
    }
}