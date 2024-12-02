namespace AdventOfCode.Days.Day2;

public static class FileReader
{
    public static List<LevelReport> GetLevelReports(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var lines = reader.ReadToEnd().Split("\n");

        var splitEachLine = lines.Select(line => line.Split()).ToList();
        var removeEmpties = splitEachLine.Select(line => line.Where(x => x != "").ToList()).ToList();
        var reports = removeEmpties.Select(line => line.Select(int.Parse).ToList()).ToList();

        return reports.Select(report => new LevelReport { Levels = report }).ToList();
    }

    public class LevelReport
    {
        public List<int> Levels { get; set; }
    }
}