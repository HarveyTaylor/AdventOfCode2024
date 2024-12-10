
using AdventOfCode.Days._06._12._23;
using AdventOfCode.Days.Day1;
using AdventOfCode.Days.Day10;
using AdventOfCode.Days.Day2;
using AdventOfCode.Days.Day3;
using AdventOfCode.Days.Day4;
using AdventOfCode.Days.Day5;
using AdventOfCode.Days.Day6;
using AdventOfCode.Days.Day7;
using AdventOfCode.Days.Day8;
using AdventOfCode.Days.Day9;
using AdventOfCode.Infrastructure.Configuration;

var configuration = ConfigBuilder.GetConfiguration();

switch (configuration.Day)
{
    case 1 :
        Day1Runner.Run(configuration.PathToInputFile);
        break;
    
    case 2 :
        Day2Runner.Run(configuration.PathToInputFile);
        break;
    
    case 3:
        Day3Runner.Run(configuration.PathToInputFile);
        break;
    
    case 4:
        Day4Runner.Run(configuration.PathToInputFile);
        break;
    
    case 5:
        Day5Runner.Run(configuration.PathToInputFile);
        break;
    
    case 6:
        Day6Runner.Run(configuration.PathToInputFile);
        break;
    
    case 7:
        Day7Runner.Run(configuration.PathToInputFile);
        break;
    
    case 8:
        Day8Runner.Run(configuration.PathToInputFile);
        break;
    
    case 9:
        Day9Runner.Run(configuration.PathToInputFile);
        break;
    
    case 10:
        Day10Runner.Run(configuration.PathToInputFile);
        break;
    
    case 26 : //Practice from year 2023 (6th)
        Day6Year2023Runner.Run(configuration.PathToInputFile);
        break;
    
    default:
        Console.WriteLine("Day Not Handled yet");
        break;
}