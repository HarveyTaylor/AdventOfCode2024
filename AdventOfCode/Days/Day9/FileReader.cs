using StreamReader = System.IO.StreamReader;

namespace AdventOfCode.Days.Day9;

public static class FileReader
{
    public static List<int> Get(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var file = reader.ReadToEnd();
        
        return file.Select(c => int.Parse(c.ToString())).ToList();
    }
}