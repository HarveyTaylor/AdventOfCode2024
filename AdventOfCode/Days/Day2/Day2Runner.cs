namespace AdventOfCode.Days.Day2;

public static class Day2Runner
{
    public static void Run(string filePath)
    {
        var levelReports = FileReader.GetLevelReports(filePath);

        var passes = PassesPart1(levelReports);
        Console.WriteLine($"Part 1: {passes}");

        passes = PassesPart2(levelReports);
        Console.WriteLine($"Part 2: {passes}");
    }

    private static bool IsLevelSafe(bool increasing, int levelBefore, int level)
    {
        var difference = levelBefore - level;
        
        if (increasing && difference is <= -1 and >= -3)
        {
            return true;
        }
        return !increasing && difference is >= 1 and <= 3;
    }

    private static int WhichLevelToRemove(List<int> levels, int currentLevel, bool increasing)
    {
        if (currentLevel == 1)
        {
            //should remove current level
            if (IsLevelSafe(levels[currentLevel -1] - levels[currentLevel+1] < 1, levels[currentLevel - 1], levels[currentLevel + 1]))
            {
                return currentLevel;
            }
            
            //should remove first level
            if (IsLevelSafe(levels[currentLevel] - levels[currentLevel+1] < 1, levels[currentLevel], levels[currentLevel + 1]))
            {
                return currentLevel-1;
            }

            return currentLevel;
        }

        if (currentLevel == levels.Count - 1)
        {
            return currentLevel;
        }

        if (currentLevel == 2)
        {
            var linesAfterIncreasing = levels[currentLevel + 1] - levels[currentLevel + 2] < 1;
            if (linesAfterIncreasing != increasing)
            {
                if (IsLevelSafe(linesAfterIncreasing, levels[currentLevel - 1], levels[currentLevel]))
                {
                    return currentLevel - 2;
                }
                if (IsLevelSafe(linesAfterIncreasing, levels[currentLevel - 2], levels[currentLevel]))
                {
                    return currentLevel - 1;
                }
            }
        }
        
        if (IsLevelSafe(increasing, levels[currentLevel -1], levels[currentLevel+1]))
        {
            return currentLevel;
        }
        
        //should remove first level
        if (IsLevelSafe(increasing, levels[currentLevel-2], levels[currentLevel]))
        {
            return currentLevel-1;
        }
        
        return currentLevel;
    }

    private static int PassesPart1(List<FileReader.LevelReport> levelReports)
    {
        var passes = 0;
        foreach(var report in levelReports)
        {
            var pass = true;
            var increasing = report.Levels[0] - report.Levels[1] < 1;

            for (var i = 1; i < report.Levels.Count; i++)
            {
                var difference = report.Levels[i - 1] - report.Levels[i];

                if (increasing && difference is > -1 or < -3)
                {
                    pass = false;
                    break;
                }
                
                if (!increasing && difference is < 1 or > 3)
                {
                    pass = false;
                    break;
                }
            }

            if (pass)
            {
                passes++;
            }
        }

        return passes;
    }

    private static int PassesPart2(List<FileReader.LevelReport> levelReports)
    {
        var passes = 0;
        foreach(var report in levelReports)
        {
            //Console.WriteLine($"{String.Join(", ", report.Levels)} Original");
            var pass = true;
            var increasing = report.Levels[0] - report.Levels[1] < 1;

            for (var i = 1; i < report.Levels.Count; i++)
            {
                pass = IsLevelSafe(increasing, report.Levels[i - 1], report.Levels[i]);
                if (!pass)
                {
                    report.Levels.RemoveAt(WhichLevelToRemove(report.Levels, i, increasing));
                    
                    increasing = report.Levels[0] - report.Levels[1] < 1;
                    for (var y = 1; y < report.Levels.Count; y++)
                    {
                        pass = IsLevelSafe(increasing, report.Levels[y - 1], report.Levels[y]);
                        if(!pass) break;
                    }
                    break;
                }
                if(!pass) break;
            }

            if (pass)
            {
                passes++;
            }
        }

        return passes;
    }
    
}