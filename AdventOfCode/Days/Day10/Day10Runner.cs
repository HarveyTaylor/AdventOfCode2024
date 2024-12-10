namespace AdventOfCode.Days.Day10;

public static class Day10Runner
{
    public static void Run(string filePath)
    { 
        List<List<int>> trailMap = FileReader.Get(filePath);

        var calc = Part1(trailMap);
        Console.WriteLine($"Part 1: {calc}");

        List<List<int>> trailMap2 = FileReader.Get(filePath);
        calc = Part2(trailMap2);
        Console.WriteLine($"Part 2: {calc}");
    }

    private static double Part1(List<List<int>> trailMap)
    {
        double answerCount = 0;

        var trailHeads = GetTrailHeads(trailMap);

        foreach (var trailHead in trailHeads)
        {
            answerCount += GetScore(trailHead, trailMap);
        }

        return answerCount;
    }

    private static double Part2(List<List<int>> trailMap)
    {
        double answerCount = 0;

        var trailHeads = GetTrailHeads(trailMap);

        foreach (var trailHead in trailHeads)
        {
            answerCount += GetScorePart2(trailHead, trailMap);
        }

        return answerCount;
    }

    private static int GetScore((int x, int y) positionStart, List<List<int>> trailMap)
    {
        List<(int x, int y)> endPositions = [];
        endPositions.AddRange(TrailHunt(positionStart, trailMap));
        return endPositions.Distinct().Count();
    }

    private static int GetScorePart2((int x, int y) positionStart, List<List<int>> trailMap)
    {
        List<(int x, int y)> endPositions = [];
        endPositions.AddRange(TrailHunt(positionStart, trailMap));
        return endPositions.Count();
    }

    private static List<(int x, int y)> TrailHunt((int x, int y) positionStart, List<List<int>> trailMap)
    {
        var positionValue = trailMap[positionStart.y][positionStart.x];
        List<(int x, int y)> endPositions = [];
        if (positionStart.x > 0 && trailMap[positionStart.y][positionStart.x-1] == positionValue+1)
        {
            if (positionValue + 1 == 9)
            {
                endPositions.Add((positionStart.x-1, positionStart.y));
            }
            else
            {
                endPositions.AddRange(TrailHunt((positionStart.x-1, positionStart.y), trailMap));
            }
        }

        if (positionStart.x+1 < trailMap[0].Count && trailMap[positionStart.y][positionStart.x+1] == positionValue+1)
        {
            if (positionValue + 1 == 9)
            {
                endPositions.Add((positionStart.x+1, positionStart.y));
            }
            else
            {
                endPositions.AddRange(TrailHunt((positionStart.x+1, positionStart.y), trailMap));
            }
        }

        if (positionStart.y > 0 && trailMap[positionStart.y-1][positionStart.x] == positionValue+1)
        {
            if (positionValue + 1 == 9)
            {
                endPositions.Add((positionStart.x, positionStart.y-1));
            }
            else
            {
                endPositions.AddRange(TrailHunt((positionStart.x, positionStart.y-1), trailMap));
            }
        }

        if (positionStart.y + 1 < trailMap.Count && trailMap[positionStart.y+1][positionStart.x] == positionValue+1)
        {
            if (positionValue + 1 == 9)
            {
                endPositions.Add((positionStart.x, positionStart.y+1));
            }
            else
            {
                endPositions.AddRange(TrailHunt((positionStart.x, positionStart.y+1), trailMap));
            }
        }

        return endPositions;
    }

    private static List<(int x, int y)> GetTrailHeads(List<List<int>> trailMap)
    {
        List<(int x, int y)> positions = [];
        for (var y = 0; y < trailMap.Count; y++)
        {
            for (var x = 0; x < trailMap[y].Count; x++)
            {
                if (trailMap[y][x] == 0)
                {
                    positions.Add((x, y));
                }
            }
        }

        return positions;
    }
}