namespace AdventOfCode.Days.Day8;

public static class Day8Runner
{
    public static void Run(string filePath)
    { 
        List<List<char>> antenna = FileReader.Get(filePath);

        var calc = Part1(antenna);
        Console.WriteLine($"Part 1: {calc}");

        calc = Part2(antenna);
        Console.WriteLine($"Part 2: {calc}");
    }

    private static double Part1(List<List<char>> antenna)
    {
        double answerCount = 0;
        var frequencies = antenna.Select(line => line.Where(c => c != '.')).SelectMany(_ => _).Distinct().ToList();
        List<(int X, int Y)> antinodePositions = [];
        
        foreach (var frequency in frequencies)
        {
            List<(int X, int Y)> frequencyPositions = [];
            for (var y = 0; y < antenna.Count; y++)
            {
                for(var x = 0; x < antenna[y].Count; x++)
                {
                    if (antenna[y][x] == frequency)
                    {
                        frequencyPositions.Add((x, y));
                    }
                }
            }

            antinodePositions.AddRange(GetAntinodes(frequencyPositions).ToList());
        }
        answerCount += antinodePositions.Distinct().Count(node => node.Y < antenna.Count && 
                                                                  node.X < antenna[0].Count &&
                                                                  node.X >= 0 &&
                                                                  node.Y >= 0);
        
        return answerCount;
    }

    private static double Part2(List<List<char>> antenna)
    {
        double answerCount = 0;
        var frequencies = antenna.Select(line => line.Where(c => c != '.')).SelectMany(_ => _).Distinct().ToList();
        List<(int X, int Y)> antinodePositions = [];
        
        foreach (var frequency in frequencies)
        {
            List<(int X, int Y)> frequencyPositions = [];
            for (var y = 0; y < antenna.Count; y++)
            {
                for(var x = 0; x < antenna[y].Count; x++)
                {
                    if (antenna[y][x] == frequency)
                    {
                        frequencyPositions.Add((x, y));
                    }
                }
            }

            antinodePositions.AddRange(GetAntinodesAndFrequencies(frequencyPositions, antenna.Count, antenna[0].Count).ToList());
            antinodePositions.AddRange(frequencyPositions);
        }
        
        answerCount += antinodePositions.Distinct().Count();
        
        return answerCount;
    }
    
    private static List<(int X, int Y)> GetAntinodes(List<(int X, int Y)> frequencyPositions)
    {
        List<(int X, int Y)> antiNodeLists = [];

        for (var i = 0; i < frequencyPositions.Count; i++)
        {
            var frequency = frequencyPositions[i];
            foreach (var calcFrequency in frequencyPositions.GetRange(i + 1, frequencyPositions.Count - i - 1))
            {
                var xDiff = calcFrequency.X - frequency.X;
                var yDiff = calcFrequency.Y - frequency.Y;
                antiNodeLists.Add((frequency.X - xDiff, frequency.Y - yDiff));
                antiNodeLists.Add((calcFrequency.X + xDiff, calcFrequency.Y + yDiff));
            }
        }

        return antiNodeLists;
    }

    private static List<(int X, int Y)> GetAntinodesAndFrequencies(List<(int X, int Y)> frequencyPositions, int length, int height)
    {
        List<(int X, int Y)> antiNodeLists = [];

        for (var i = 0; i < frequencyPositions.Count; i++)
        {
            var frequency = frequencyPositions[i];
            foreach (var calcFrequency in frequencyPositions.GetRange(i + 1, frequencyPositions.Count - i - 1))
            {
                var xDiff = calcFrequency.X - frequency.X;
                var yDiff = calcFrequency.Y - frequency.Y;

                var currentX = frequency.X - xDiff;
                var currentY = frequency.Y - yDiff;
                while (currentX >= 0 && currentY >= 0 && currentX < length && currentY < height)
                {
                    antiNodeLists.Add((currentX, currentY));
                    currentX -= xDiff;
                    currentY -= yDiff;
                }
                
                currentX = frequency.X + xDiff;
                currentY = frequency.Y + yDiff;
                while (currentX >= 0 && currentY >= 0 && currentX < length && currentY < height)
                {
                    antiNodeLists.Add((currentX, currentY));
                    currentX += xDiff;
                    currentY += yDiff;
                }
            }
        }

        return antiNodeLists;
    }
}