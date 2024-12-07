namespace AdventOfCode.Days.Day7;

public static class Day7Runner
{
    public static void Run(string filePath)
    {
        var bridgeCalibrations = FileReader.Get(filePath);

        var calc = Part1(bridgeCalibrations);
        Console.WriteLine($"Part 1: {calc}");

        calc = Part2(bridgeCalibrations);
        Console.WriteLine($"Part 2: {calc}");
    }

    private static double Part1(List<(double TestValue, List<double> OtherValues)> bridgeCalibrations)
    {
        double answerCount = 0;
        
        foreach (var bridgeCalibration in bridgeCalibrations)
        {
            var operationCount = bridgeCalibration.OtherValues.Count - 1;
            var operationPossibilities = ((1 << operationCount-1) * 2) - 1;
            for (var i = 0; i <= operationPossibilities; i++)
            {
                var total = bridgeCalibration.OtherValues.First();
                for(var x = 0; x < bridgeCalibration.OtherValues.Count-1; x++)
                {
                    var shifted = 1 << x;
                    if ((i & shifted) != 0)
                    {
                        total += bridgeCalibration.OtherValues[x+1];
                    }
                    else
                    {
                        total *= bridgeCalibration.OtherValues[x+1];
                    }
                }

                if (total == bridgeCalibration.TestValue)
                {
                    answerCount += bridgeCalibration.TestValue;
                    break;
                }
            }
            
        }
        
        return answerCount;
    }
    private static double Part2(List<(double TestValue, List<double> OtherValues)> bridgeCalibrations)
    {
        double answerCount = 0;
        
        foreach (var bridgeCalibration in bridgeCalibrations)
        {
            var operationCount = bridgeCalibration.OtherValues.Count - 1;
            List<List<char>> operationLists = GetAllOperationCombinations(operationCount);
            foreach (var operationList in operationLists)
            {
                if (bridgeCalibration.TestValue == Calc(new List<double>(bridgeCalibration.OtherValues), operationList))
                {
                    answerCount += bridgeCalibration.TestValue;
                    Console.WriteLine($"correct Value: {bridgeCalibration.TestValue}");
                    break;
                }
            }
        }
        
        return answerCount;
    }

    private static List<List<char>> GetAllOperationCombinations(int count)
    {
        List<char> operations = ['*', '+', '|'];
        List<List<char>> operationLists = new();

        foreach (var operation in operations)
        {
            if (count > 1)
            {
                var subOperationList = GetAllOperationCombinations(count - 1);
                var newOperationList = subOperationList.Select(subOperation => {
                    subOperation.Insert(0, operation);
                    return subOperation;
                }).ToList();
                operationLists.AddRange(newOperationList);
            }
            else
            {
                operationLists.Add([operation]);
            }
        }

        return operationLists;
    }
    
    private static double Calc(List<double> values, List<char> operations)
    {
        var total = values.First();
        for(var x = 0; x < values.Count-1; x++)
        {
            if (operations[x] == '+')
            {
                total += values[x+1];
            }
            else if (operations[x] == '*')
            {
                total *= values[x+1];
            }
            else if (operations[x] == '|')
            {
                total = double.Parse(String.Concat(total.ToString(), values[x + 1].ToString()));
                values.RemoveAt(x+1);
                operations.RemoveAt(x);
                x--;
            }
        }

        return total;
    }
}