using StreamReader = System.IO.StreamReader;

namespace AdventOfCode.Days.Day10;

public static class FileReader
{
    public static List<List<int>> Get(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var file = reader.ReadToEnd();
        return file.Split('\n').Select(line => line.Select(c => int.Parse(c.ToString())).ToList()).ToList();
    }
}