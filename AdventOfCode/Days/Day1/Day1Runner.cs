namespace AdventOfCode.Days.Day1;

public static class Day1Runner
{
    public static void Run(string filePath)
    {
        var locationPairs = FileReader.GetLocationPair(filePath);

        var locationList1 = locationPairs.Select(pair => pair.Location1).Order().ToList();
        var locationList2 = locationPairs.Select(pair => pair.Location2).Order().ToList();

        var differences = 0;
        for (var i = 0; i < locationList1.Count; i++)
        {
            differences += Math.Abs(locationList1[i] - locationList2[i]);
        }
        
        Console.WriteLine($"Part 1: {differences}");
        
        var similarityScore = 0;
        for (var i = 0; i < locationList1.Count; i++)
        {
            similarityScore += locationList1[i] * locationList2.Count(location => location == locationList1[i]);
        }
        
        Console.WriteLine($"Part 2: {similarityScore}");
    }
}