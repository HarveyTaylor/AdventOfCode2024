
using AdventOfCode.Days._06._12._23;
using AdventOfCode.Days.Day1;
using AdventOfCode.Infrastructure.Configuration;

var configuration = ConfigBuilder.GetConfiguration();

switch (configuration.Day)
{
    case 1 :
        Day1Runner.Run(configuration.PathToInputFile);
        break;
    
    case 26 : //Practice from year 2023 (6th)
        Day6Year2023Runner.Run(configuration.PathToInputFile);
        break;
    
    default:
        Console.WriteLine("Day Not Handled yet");
        break;
}