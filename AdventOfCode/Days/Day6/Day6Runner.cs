using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days.Day6;

public static class Day6Runner
{
    public static void Run(string filePath)
    {
        var guardMap = FileReader.Get(filePath);

        var calc = Part1(guardMap);
        Console.WriteLine($"Part 1: {calc}");

        calc = Part2(guardMap);
        Console.WriteLine($"Part 2: {calc}");
    }

    private static int Part1(List<List<char>> guardMap)
    {
        var escaped = false;
        var currentLine = guardMap.FindIndex(line => line.Any(c => c is '^' or '>' or '<' or 'v'));
        var currentIndex = guardMap[currentLine].FindIndex(c => c is '^' or '>' or '<' or 'v');
        var guardMapCopy = new List<List<char>>(guardMap.Select(line => new List<char>(line)));

        DirectionOfTravel direction = guardMap[currentLine][currentIndex] switch
        {
            '^' => DirectionOfTravel.Up,
            '>' => DirectionOfTravel.Right,
            '<' => DirectionOfTravel.Left,
            'v' => DirectionOfTravel.Down,
            _ => throw new Exception("Invalid direction")
        };
        
        while (!escaped)
        {
            guardMapCopy[currentLine][currentIndex] = 'X';
            switch (direction)
            {
                case DirectionOfTravel.Up:
                    if (guardMap[currentLine - 1][currentIndex] == '#')
                    {
                        currentIndex++;
                        direction = DirectionOfTravel.Right;
                        break;
                    }
                    currentLine--;
                    break;
                case DirectionOfTravel.Down:
                    if (guardMap[currentLine + 1][currentIndex] == '#')
                    {
                        currentIndex--;
                        direction = DirectionOfTravel.Left;
                        break;
                    }
                    currentLine++;
                    break;
                case DirectionOfTravel.Left:
                    if (guardMap[currentLine][currentIndex-1] == '#')
                    {
                        currentLine--;
                        direction = DirectionOfTravel.Up;
                        break;
                    }
                    currentIndex--;
                    break;
                case DirectionOfTravel.Right:
                    if (guardMap[currentLine][currentIndex+1] == '#')
                    {
                        currentLine++;
                        direction = DirectionOfTravel.Down;
                        break;
                    }
                    currentIndex++;
                    break;
            }
            
            if (   ( currentLine == guardMap.Count-1 && direction == DirectionOfTravel.Down )
                   || ( currentIndex == guardMap[0].Count-1  && direction == DirectionOfTravel.Right)
                   || ( currentLine == -1  && direction == DirectionOfTravel.Up)
                   || ( currentIndex == -1 && direction == DirectionOfTravel.Left))
            {
                guardMapCopy[currentLine][currentIndex] = 'X';
                escaped = true;
            }
        }

        return guardMapCopy.Select(line => line.Count(c => c == 'X')).Sum();
    }

    enum DirectionOfTravel
    {
        Up,
        Down,
        Right,
        Left
    }

    private static int Part2(List<List<char>> guardMap)
    {
        var copy = new List<List<char>>(guardMap.Select(line => new List<char>(line)));
        var count = 0;
        for (var y = 0; y < guardMap.Count; y++)
        {
            for (var x = 0; x < copy[y].Count; x++)
            {
                if (copy[y][x] == '.' && !Escapes(new List<List<char>>(guardMap.Select(line => new List<char>(line))), (x, y)))
                {
                    Console.WriteLine($"Obstruction Causes Loop: {x.ToString()}, {y.ToString()}");
                    count++;
                }

                if (copy[y][x] != '.')
                {
                    var currentPlace = copy[y][x];
                    Console.WriteLine($"Obstruction can be placed: {x.ToString()}, {y.ToString()} : {currentPlace}");
                }
            }
        }

        return count;
    }
    
    
    private static bool Escapes(List<List<char>> guardMap, (int x, int y) newObstructionPosition)
    {
        var currentLine = guardMap.FindIndex(line => line.Any(c => c is '^' or '>' or '<' or 'v'));
        var currentIndex = guardMap[currentLine].FindIndex(c => c is '^' or '>' or '<' or 'v');
        var obstructionsHit = new List<(int x, int y, DirectionOfTravel direction)>();
        guardMap[newObstructionPosition.y][newObstructionPosition.x] = '#';

        DirectionOfTravel direction = guardMap[currentLine][currentIndex] switch
        {
            '^' => DirectionOfTravel.Up,
            '>' => DirectionOfTravel.Right,
            '<' => DirectionOfTravel.Left,
            'v' => DirectionOfTravel.Down,
            _ => throw new Exception("Invalid direction")
        };
        
        while (true)
        {
            switch (direction)
            {
                case DirectionOfTravel.Up:
                    if (guardMap[currentLine - 1][currentIndex] == '#')
                    {
                        obstructionsHit.Add((currentIndex, currentLine - 1, DirectionOfTravel.Up));
                        direction = DirectionOfTravel.Right;
                        break;
                    }
                    currentLine--;
                    break;
                case DirectionOfTravel.Down:
                    if (guardMap[currentLine + 1][currentIndex] == '#')
                    {
                        obstructionsHit.Add((currentIndex, currentLine + 1, DirectionOfTravel.Down));
                        direction = DirectionOfTravel.Left;
                        break;
                    }
                    currentLine++;
                    break;
                case DirectionOfTravel.Left:
                    if (guardMap[currentLine][currentIndex-1] == '#')
                    {
                        obstructionsHit.Add((currentIndex - 1, currentLine, DirectionOfTravel.Left));
                        direction = DirectionOfTravel.Up;
                        break;
                    }
                    currentIndex--;
                    break;
                case DirectionOfTravel.Right:
                    if (guardMap[currentLine][currentIndex+1] == '#')
                    {
                        obstructionsHit.Add((currentIndex + 1, currentLine, DirectionOfTravel.Right));
                        direction = DirectionOfTravel.Down;
                        break;
                    }
                    currentIndex++;
                    break;
            }

            if (obstructionsHit.Count != obstructionsHit.Distinct().Count())
            {
                return false; // if hit same obstruction in same direction its a loop
            }
            
            if (( currentLine == guardMap.Count-1 && direction == DirectionOfTravel.Down )
                   || ( currentIndex == guardMap[0].Count-1  && direction == DirectionOfTravel.Right)
                   || ( currentLine == 0  && direction == DirectionOfTravel.Up)
                   || ( currentIndex == 0 && direction == DirectionOfTravel.Left))
            {
                return true;
            }
        }
    }
    
}