// See https://aka.ms/new-console-template for more information

using Day5;

// var input = File.ReadAllText(@"demo.txt")
//     .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
    
var input = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

// fill list of lines from input
var lines = new List<Line>();
foreach (var l in input)
{
    var p = l.Split(" -> ");
    
    lines.Add(new Line()
    {
        x1 = int.Parse(p[0].Split(",")[0]),
        y1 = int.Parse(p[0].Split(",")[1]),
        x2 = int.Parse(p[1].Split(",")[0]),
        y2 = int.Parse(p[1].Split(",")[1]),
    });
}

int gridSize = 1000;

var grid = new int[gridSize, gridSize];

// fill grid with 0
for (int i = 0; i < gridSize; i++)
{
    for (int j = 0; j < gridSize; j++)
    {
        grid[i, j] = 0;
    }
}

void PrintGrid()
{
    for (int i = 0; i < gridSize; i++)
    {
        for (int j = 0; j < gridSize; j++)
        {
            Console.Write(grid[i, j]);
        }

        Console.WriteLine();
    }
    Console.WriteLine("--------------------------------");
}

void DrawLine(Line l)
{
    if (l.isVertical())
    {
        var y = l.y1;
        var x1 = l.x1 > l.x2 ? l.x2 : l.x1;
        var x2 = l.x1 > l.x2 ? l.x1 : l.x2;
        
        for (int i = x1; i <= x2; i++)
        {
            grid[y,i] += 1;
        }
    }
    else if (l.isHorizontal())
    {
        var x = l.x1;
        var y1 = l.y1 > l.y2 ? l.y2 : l.y1;
        var y2 = l.y1 > l.y2 ? l.y1 : l.y2;
        
        for (int i = y1; i <= y2; i++)
        {
            grid[i, x] += 1;
        }
    }
    else
    {
        var x1 = l.x1 > l.x2 ? l.x2 : l.x1;
        var x2 = l.x1 > l.x2 ? l.x1 : l.x2;

        var y = 0;
        var step = 0;

        if (x1 == l.x1)
        {
            y = l.y1;
            if (l.y1 > l.y2)
            {
                step = -1;
            }
            else
            {
                step = 1;
            }
        }
        else
        {
            y = l.y2;
            if (l.y2 > l.y1)
            {
                step = -1;
            }
            else
            {
                step = 1;
            }
        }
        
        for (int i = x1; i <= x2; i++)
        {
            grid[y, i] += 1;
            // Console.WriteLine($"[{i},{y}]");
            y += step;
            
        }
    }
        
}

foreach (var line in lines)
{
    DrawLine(line);
}
// PrintGrid();

var count = 0;
for (int i = 0; i < gridSize; i++)
{
    for (int j = 0; j < gridSize; j++)
    {
        if (grid[i, j] >= 2)
        {
            count++;
        }
    }
}

Console.WriteLine($"Number of points where at least 2 lines overlap: {count}");


