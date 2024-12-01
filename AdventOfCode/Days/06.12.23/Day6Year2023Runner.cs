namespace AdventOfCode.Days._06._12._23;

public static class Day6Year2023Runner
{
    public static void Run(string filePath)
    {
        var boatRaces = BoatRaceFileReader.GetBoatRaces(filePath);
        var totalBestWays = BoatRace.NumberOfWaysToBeatAllRaces(boatRaces);
        Console.WriteLine($"Part 1: {totalBestWays}");

        //Wrong need to remove spaces and count as 1 big number
        var boatRacesPart2 = BoatRaceFileReaderPart2.GetBoatRaces(filePath);
        var totalBestWaysPart2 = BoatRace.NumberOfWaysToBeatAllRaces(boatRacesPart2);
        Console.WriteLine($"Part 2: {totalBestWaysPart2}");
    }
}