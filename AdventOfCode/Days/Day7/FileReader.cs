using StreamReader = System.IO.StreamReader;

namespace AdventOfCode.Days.Day7;

public static class FileReader
{
    public static List<(double testValue, List<double> otherValues)> Get(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var file = reader.ReadToEnd();
        
        var lines = file.Split("\n").ToList();

        List<(double testValue, List<double> otherValues)> bridgeCalibrations = new();
        foreach (var line in lines)
        {
            var testValue = double.Parse(line.Split(':').First());
            var otherValues = line.Split(':')[1].Split(" ").Where(x => x != "").Select(double.Parse).ToList();
            bridgeCalibrations.Add((testValue, otherValues));
        }
        
        return bridgeCalibrations;
    }
}