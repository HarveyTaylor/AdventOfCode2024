namespace AdventOfCode.Days._06._12._23;

public static class BoatRaceFileReader
{
    public static Dictionary<int, int> GetBoatRaces(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var lines = reader.ReadToEnd().Split("\n");

        var timeNumbers = lines[0].Split().Where(
            line => !string.IsNullOrEmpty(line)
        ).Where(
            input => input != "Time:"
        ).Select(
            int.Parse
        ).ToList();
        
        var distanceNumbers = lines[1].Split().Where(
            line => !string.IsNullOrEmpty(line)
        ).Where(
            input => input != "Distance:"
        ).Select(
            int.Parse
        ).ToList();

        var boatRaces = new Dictionary<int, int>();
        for(var i = 0; i < timeNumbers.Count; i++) {
            boatRaces.Add(timeNumbers[i], distanceNumbers[i]);
        }

        return boatRaces;
    }
}