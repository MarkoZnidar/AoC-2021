// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText(@"input.txt").Split('\n');

var points = new HashSet<(int, int)>();
var foldInstructions = new List<(int, int)>();
var parseFolds = false;
// parse input
foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        parseFolds = true;
        continue;
    }
    
    if (!parseFolds)
    {
        var c = line.Split(",");
        points.Add(new (int.Parse(c[0]), int.Parse(c[1])));
    }
    else
    {
        var f = line.Replace("fold along ", "");
        if (f[0] == 'y')
        {
            foldInstructions.Add(new (0, int.Parse(f.Substring(2))));
        }
        else
        {
            foldInstructions.Add(new (int.Parse(f.Substring(2)),0));
        }
    }
}

(int, int) MirrorPointOverLine((int, int) point, (int, int) line)
{
    if (line.Item1 != 0)
    {
        return (2* line.Item1 - point.Item1, point.Item2);
    }
    return (point.Item1,2* line.Item2 - point.Item2);
}

var foldCount = 0;
var firstFoldDotsCount = 0;
var newPoints = new HashSet<(int, int)>();

foreach (var fold in foldInstructions)
{
    newPoints = new HashSet<(int, int)>();
    foldCount++;
    foreach (var point in points)
    {
        if (fold.Item1 != 0)
        {
            // vertical fold
            if (point.Item1 > fold.Item1)
            {
                newPoints.Add(MirrorPointOverLine(point,fold));
            }
            else
            {
                newPoints.Add(point);
            }
        }
        else
        {
            // horizontal fold
            if (point.Item2 > fold.Item2)
            {
                newPoints.Add(MirrorPointOverLine(point,fold));
            }
            else
            {
                newPoints.Add(point);
            }
        }
    }
    
    points = newPoints;

    if (foldCount == 1)
        firstFoldDotsCount = newPoints.Count;
}

Console.WriteLine($"Part 1: Visible dots after first fold: {firstFoldDotsCount}");
Console.WriteLine($"Part 2:");

for (int y = 0; y <= 6; y++)
{
    for (int x = 0; x <= 50; x++)
    {
        if (newPoints.Contains((x,y)))
        {
            Console.Write("##");
        }
        else
        {
            Console.Write("..");
        }
    }
    Console.WriteLine();
}


