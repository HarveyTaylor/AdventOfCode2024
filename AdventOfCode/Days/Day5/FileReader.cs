namespace AdventOfCode.Days.Day5;

public static class FileReader
{
    public static PageOrderingRules Get(string inputFile)
    {
        var reader = new StreamReader(inputFile);
        var file = reader.ReadToEnd();

        var pageOrderingRules = new PageOrderingRules();

        var rulesPart = file.Split("\n\n")[0];
        var rulesString = rulesPart.Split("\n");

        foreach (var rule in rulesString)
        {
            var pageBefore = int.Parse(rule.Split('|')[0]);
            var pageAfter = int.Parse(rule.Split('|')[1]);
            
            pageOrderingRules.PageRules.Add((pageBefore, pageAfter));
        }
        
        var updatesPart = file.Split("\n\n")[1];
        var updatesString = updatesPart.Split('\n');

        foreach (var update in updatesString)
        {
            var updatePages = update.Split(',').Select(int.Parse).ToList();
            pageOrderingRules.Updates.Add(updatePages);
        }
        
        return pageOrderingRules;
    }
}

public class PageOrderingRules
{
    public List<(int PageBefore, int PageAfter)> PageRules { get; set; } = new List<(int PageBefore, int PageAfter)>();
    public List<List<int>> Updates { get; set; } = new List<List<int>>();
}