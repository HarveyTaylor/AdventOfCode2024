namespace AdventOfCode.Days.Day4;

public static class FileReader
{
    public static string Get(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var file = reader.ReadToEnd();
        return file;
    }
}