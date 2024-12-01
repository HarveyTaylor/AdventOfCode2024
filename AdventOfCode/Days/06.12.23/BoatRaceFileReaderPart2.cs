namespace AdventOfCode.Days._06._12._23;

public static class BoatRaceFileReaderPart2
{
    public static Dictionary<double, double> GetBoatRaces(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var lines = reader.ReadToEnd().Split("\n");

        var timeNumbers = lines[0].Split().Where(
            line => !string.IsNullOrEmpty(line)
        ).Where(
            input => input != "Time:"
        ).ToList();
        
        var distanceNumbers = lines[1].Split().Where(
            line => !string.IsNullOrEmpty(line)
        ).Where(
            input => input != "Distance:"
        ).ToList();

        var timeString = string.Concat(timeNumbers);
        var distanceTime = string.Concat(distanceNumbers);

        var boatRaces = new Dictionary<double, double>() { { double.Parse(timeString), double.Parse(distanceTime)} };
        return boatRaces;
    }
}