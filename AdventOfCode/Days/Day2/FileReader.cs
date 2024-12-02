namespace AdventOfCode.Days.Day1;

public static class FileReader
{
    public static List<LocationPair> GetLocationPair(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var lines = reader.ReadToEnd().Split("\n");

        var splitEachLine = lines.Select(line => line.Split()).ToList();
        var removeEmpties = splitEachLine.Select(line => line.Where(x => x != "").ToList()).ToList();
        var locationPairLists = removeEmpties.Select(line => line.Select(int.Parse).ToList()).ToList();

        return locationPairLists.Select(locationPair => new LocationPair { Location1 = locationPair[0], Location2 = locationPair[1]}).ToList();
    }

    public class LocationPair
    {
        public int Location1 { get; set; }
        public int Location2 { get; set; }
    }
}