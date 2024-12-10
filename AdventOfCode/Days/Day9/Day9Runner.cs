namespace AdventOfCode.Days.Day9;

public static class Day9Runner
{
    public static void Run(string filePath)
    { 
        List<int> diskMap = FileReader.Get(filePath);

        var calc = Part1(diskMap);
        Console.WriteLine($"Part 1: {calc}");

        List<int> diskMap2 = FileReader.Get(filePath);
        calc = Part2(diskMap2);
        Console.WriteLine($"Part 2: {calc}");
    }

    private static double Part1(List<int> diskMap)
    {
        List<FileOnDisk> disks = [];
        var id = 0;
        for (int i = 0; i < diskMap.Count; i+=2)
        {
            disks.Add(new ()
            {
                Id = id,
                Blocks = diskMap[i],
                FreeSpace = i+1 < diskMap.Count ? diskMap[i+1] : null
            });
            id++;
        }

        List<int> diskFreeSpaceMapped = [];

        for (int i = 0; i < disks.Count; i ++)
        {
            diskFreeSpaceMapped.AddRange(Enumerable.Repeat(disks[i].Id, disks[i].Blocks));

            if (i == disks.Count - 1)
            {
                break;
            }
            for (int x = 0; x < disks[i].FreeSpace; x++)
            {
                diskFreeSpaceMapped.Add(disks[^1].Id);
                disks[^1].Blocks -= 1;
                if (disks[^1].Blocks == 0)
                {
                    disks.RemoveAt(disks.Count-1);
                }
            }
        }
        
        double answerCount = 0;
        for (int i = 0; i < diskFreeSpaceMapped.Count; i++)
        {
            answerCount += i * diskFreeSpaceMapped[i];
        }

        return answerCount;
    }

    private static double Part2(List<int> diskMap)
    {
        List<FileOnDisk> disks = [];
        var id = 0;
        for (int i = 0; i < diskMap.Count; i+=2)
        {
            disks.Add(new ()
            {
                Id = id,
                Blocks = diskMap[i],
                FreeSpace = i+1 < diskMap.Count ? diskMap[i+1] : null
            });
            id++;
        }

        List<int?> diskFreeSpaceNotMapped = [];
        for (int i = 0; i < disks.Count; i++)
        {
            diskFreeSpaceNotMapped.AddRange(Enumerable.Repeat((int?)disks[i].Id, disks[i].Blocks));
            diskFreeSpaceNotMapped.AddRange(Enumerable.Repeat((int?)null, disks[i].FreeSpace ?? 0));
        }
        
        
        List<int?> diskFreeSpaceMapped = new List<int?>(diskFreeSpaceNotMapped);

        for (int x = disks.Count-1; x >= 0; x--)
        {
            for (int i = 0; i < x; i++)
            {
                if (disks[x].Blocks != 0 && disks[x].Blocks <= disks[i].FreeSpace)
                {
                    var indexId = diskFreeSpaceNotMapped.FindIndex(y => y == disks[i].Id);
                    var indexToReplace = diskFreeSpaceMapped.GetRange(indexId, diskFreeSpaceNotMapped.Count-indexId).FindIndex(y => y == null) + indexId;
                    var indexToRemove = diskFreeSpaceNotMapped.FindIndex(y => y == disks[x].Id);

                    for(var y = 0; y < disks[x].Blocks; y++)
                    {
                        diskFreeSpaceMapped[indexToReplace] = disks[x].Id;
                        indexToReplace++;
                    }

                    for (var y = 0; y < disks[x].Blocks; y++)
                    {
                        diskFreeSpaceMapped[indexToRemove] = null;
                        indexToRemove++;
                    }
                    
                    disks[i].FreeSpace -= disks[x].Blocks;
                    disks[x].Blocks = 0;
                }
            }
        }
        
        double answerCount = 0;
        for (int i = 0; i < diskFreeSpaceMapped.Count; i++)
        {
            answerCount += i * diskFreeSpaceMapped[i] ?? 0;
        }

        var print = String.Join(", ", diskFreeSpaceMapped.Select(x => x == null ? "." : x.ToString()));
        Console.WriteLine(print);
        return answerCount;
    }

    public class FileOnDisk
    {
        public int Id { get; set; }
        public int Blocks { get; set; }
        public int? FreeSpace { get; set; }
    }
}