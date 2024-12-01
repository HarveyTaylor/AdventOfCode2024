namespace AdventOfCode.Days._06._12._23;

public static class BoatRace
{
    public static int NumberOfWaysToBeatAllRaces(Dictionary<int, int> boatRaceBestDistances)
    {
        var numberOfWays = 1;
        
        foreach (var boatRaceBestDistance in boatRaceBestDistances)
        {
            numberOfWays *= NumberOfChancesToBeatCurrentDistance(boatRaceBestDistance.Key, boatRaceBestDistance.Value);
        }

        return numberOfWays;
    }
    public static int NumberOfWaysToBeatAllRaces(Dictionary<double, double> boatRaceBestDistances)
    {
        var numberOfWays = 1;
        
        foreach (var boatRaceBestDistance in boatRaceBestDistances)
        {
            numberOfWays *= NumberOfChancesToBeatCurrentDistance(boatRaceBestDistance.Key, boatRaceBestDistance.Value);
        }

        return numberOfWays;
    }

    private static int NumberOfChancesToBeatCurrentDistance(int totalTime, int currentBestDistance)
    {
        var numberOfChances = 0;
        
        for (var i = 1; i < totalTime; i++)
        {
            if (DoesButtonHoldTimeBeatDistance(i, totalTime, currentBestDistance))
            {
                numberOfChances++;
            }
        }
        
        return numberOfChances;
    }

    private static int NumberOfChancesToBeatCurrentDistance(double totalTime, double currentBestDistance)
    {
        var numberOfChances = 0;
        
        for (var i = 1; i < totalTime; i++)
        {
            if (DoesButtonHoldTimeBeatDistance(i, totalTime, currentBestDistance))
            {
                numberOfChances++;
            }
        }
        
        return numberOfChances;
    }

    private static bool DoesButtonHoldTimeBeatDistance(double buttonHoldTime, double totalTime, double distance)
    {
        return buttonHoldTime * (totalTime - buttonHoldTime) > distance;
    }
}