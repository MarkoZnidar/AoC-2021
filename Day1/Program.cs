// See https://aka.ms/new-console-template for more information

List<int> ReadInputFromFile()
{
    string text = File.ReadAllText(@"input.txt");

    List<int> ints = text.Split('\n').Select(int.Parse).ToList();
    return ints;
}

int IncreasedValueCount(List<int> depths)
{
    var total = 0;
    for (int i = 0; i < depths.Count - 1; i++)
    {
        if (depths[i] < depths[i + 1])
        {
            total++;
        }
    }

    return total;
}

var depths = ReadInputFromFile();

var count = IncreasedValueCount(depths);
Console.WriteLine($"Part 1: Number of times the depth value increases: {count}");

// part 2
var windows = new List<int>();
for (int i = 0; i < depths.Count - 2; i++)
{
    windows.Add( depths[i] + depths[i + 1] + depths[i + 2]);
}
    
count = IncreasedValueCount(windows);
Console.WriteLine($"Part 2: Three-measurement sums larger than the previous: {count}");