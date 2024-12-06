using System.Text.RegularExpressions;

namespace AdventOfCode.Days.Day5;

public static class Day5Runner
{
    public static void Run(string filePath)
    {
        var pageOrderingRules = FileReader.Get(filePath);

        var calc = Part1(pageOrderingRules);
        Console.WriteLine($"Part 1: {calc}");

        calc = Part2(pageOrderingRules);
        Console.WriteLine($"Part 2: {calc}");
    }
    private static int Part1(PageOrderingRules pageOrderingRules)
    {
        return pageOrderingRules.Updates
            .Where(update => CheckRules(update, pageOrderingRules.PageRules))
            .Sum(update => update[(update.Count / 2)]);
    }
    
    private static int Part2(PageOrderingRules pageOrderingRules)
    {
        return pageOrderingRules.Updates
            .Where(update => !CheckRules(update, pageOrderingRules.PageRules))
            .Sum(update => CorrectUpdate(update, pageOrderingRules.PageRules)[(update.Count / 2)]);
    }

    private static bool CheckRules(List<int> update, List<(int PageBefore, int PageAfter)> rules)
    {
        var rulesThatApply = rules.Where(rule => update.Contains(rule.PageBefore) && update.Contains(rule.PageAfter)).ToList();

        foreach (var rule in rulesThatApply)
        {
            var pageBeforeIndex = update.FindIndex(x => x == rule.PageBefore);
            var pageAfterIndex = update.FindIndex(x => x == rule.PageAfter);

            if (pageBeforeIndex > pageAfterIndex)
            {
                return false;
            }
        }
        
        return true;
    }

    private static List<int> CorrectUpdate(List<int> update, List<(int PageBefore, int PageAfter)> rules)
    {
        var rulesThatApply = rules.Where(rule => update.Contains(rule.PageBefore) && update.Contains(rule.PageAfter)).ToList();
        var orderedRules = OrderRules(rulesThatApply);

        var correctedUpdate = update;
        
        foreach (var rule in orderedRules)
        {
            var pageBeforeIndex = correctedUpdate.FindIndex(x => x == rule.PageBefore);
            var pageAfterIndex = correctedUpdate.FindIndex(x => x == rule.PageAfter);

            if (pageBeforeIndex > pageAfterIndex)
            {
                correctedUpdate[pageAfterIndex] = rule.PageBefore;
                correctedUpdate[pageBeforeIndex] = rule.PageAfter;
            }
        }
        
        if(!CheckRules(correctedUpdate, orderedRules))
        {
            Console.WriteLine($"original rules \n: {string.Join("\n", orderedRules)}\n original list: [{string.Join(",", update)}]\n corrected list: [{string.Join(",", correctedUpdate)}]");
        }
        
        return correctedUpdate;
    }

    private static List<(int PageBefore, int PageAfter)> OrderRules(List<(int PageBefore, int PageAfter)> rules)
    {
        var ruleGroups = rules.GroupBy(rule => rule.PageBefore).ToList();

        var newRuleGroups = new List<IGrouping<int, (int PageBefore, int PageAfter)>> { ruleGroups.First()};

        for(var i = 1; i < ruleGroups.Count; i++)
        {
            var index = newRuleGroups.FindIndex(group => ruleGroups[i].Select(rule => rule.PageAfter).Contains(group.Key));
            index = index == -1 ? newRuleGroups.Count : index;
            newRuleGroups.Insert(index, ruleGroups[i]);
        }

        var newOrderedRule = new List<(int PageBefore, int PageAfter)>();

        foreach (var group in newRuleGroups)
        {
            newOrderedRule.AddRange(group.ToList());
        }

        return newOrderedRule;
    }
}