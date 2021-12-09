// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText(@"demo.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

// var input = File.ReadAllText(@"input.txt")
//     .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var rows = input.Length;
var cols = input[0].Length;

var matrix = new int[rows, cols];

for (int i = 0; i < rows; i++)
{
    var row = input[i].ToCharArray();
    for (int j = 0; j < cols; j++)
    {
        matrix[i, j] = int.Parse(row[j].ToString());
    }
}

var lowPoints = new List<int>();
var lowPointsCoordinates = new List<(int, int)>();

for (var i = 0; i < matrix.GetLength(0); i++)
{
    for (var j = 0; j < matrix.GetLength(1); j++)
    {
        //compute left index from current index
        var leftIndex = j - 1;
        var rightIndex = j + 1;
        var upIndex = i - 1;
        var downIndex = i + 1;

        var compareInt = new List<int>();

        if (leftIndex >= 0)
            compareInt.Add(matrix[i, leftIndex]);

        if (rightIndex < matrix.GetLength(1))
            compareInt.Add(matrix[i, rightIndex]);

        if (upIndex >= 0)
            compareInt.Add(matrix[upIndex, j]);

        //check if down index is valid
        if (downIndex < matrix.GetLength(0))
            compareInt.Add(matrix[downIndex, j]);

        var val = matrix[i, j];

        // check if any value in compareInt is lower then val
        if (compareInt.All(x => val < x))
        {
            lowPoints.Add(val);
            lowPointsCoordinates.Add((i, j));
        }
    }

    //Console.WriteLine($"Row: {i}, Min: {string.Join(",",lowPoints)}");
}

Console.WriteLine(
    $"Part 1: Sum of all risk levels of all low points is: {lowPoints.Select(l => l += 1).ToList().Sum()}");

int[,] GeerateBasinMap(List<(int, int)> lowPointCoord)
{
    var basinMap = new int[rows, cols];

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            basinMap[i, j] = 0;
        }
    }
    
    var basinId = 1;

    foreach (var coordinate in lowPointCoord)
    {

        var row = coordinate.Item1;
        var column = coordinate.Item2;
    
        var processCoordinates = new List<(int, int)>(){(row, column)};
        var beenThere = new List<(int, int)>();
    
        while (processCoordinates.Any())
        {
            var current = processCoordinates.First();
            processCoordinates.Remove(current);
            row = current.Item1;
            column = current.Item2;
            
            if (beenThere.Any(x => x.Item1 == row && x.Item2 == column))
            {
                continue;
            }
            
            beenThere.Add((row,column));
            
            basinMap[row, column] = basinId;
            
            var leftIndex = column - 1;
            var rightIndex = column + 1;
            var upIndex = row - 1;
            var downIndex = row + 1;
            
            if (leftIndex >= 0)
            {
                if(matrix[row, leftIndex] == 9)
                    continue;
                processCoordinates.Add((row, leftIndex));
            }

            if (rightIndex < matrix.GetLength(1))
            {
                if (matrix[row, rightIndex] == 9)
                    continue;
                processCoordinates.Add((row, rightIndex));
            }

            if (upIndex >= 0)
            {
                if (matrix[upIndex, column] == 9)
                    continue;
                processCoordinates.Add((upIndex, column));
            }

            if (downIndex < matrix.GetLength(0))
            {
                if(matrix[downIndex, column] == 9)
                    continue;
                processCoordinates.Add((downIndex, column));
            }
        }

        basinId++;
    }
    
    return basinMap;
}

var map = GeerateBasinMap(lowPointsCoordinates);

var basinIdAndSize = new Dictionary<int, int>();

for(int i = 1; i<=lowPointsCoordinates.Count; i++)
{
    basinIdAndSize.Add(i,map.Cast<int>().Count(x => x == i));
}

Console.WriteLine("3 largest basins are:");

var largest = basinIdAndSize.OrderByDescending(x => x.Value).Take(3).ToList();

var result = 1;
foreach (var item in largest)
{
    Console.WriteLine($"Basin ID:{item.Key} Size:{item.Value}");
    result *= item.Value;
}

Console.WriteLine($"Part 2: 3 largest basins multiplication result is: {result}");

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++)
    {
        Console.Write(map[i, j]);
    }
    Console.WriteLine();
}


