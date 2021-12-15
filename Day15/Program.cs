// See https://aka.ms/new-console-template for more information


var input = File.ReadAllText(@"input.txt").Split('\n');

var rows = input.Length;
var cols = input[0].Length;

var matrix = new int[rows, cols];

// Fill matrix form input
for (int i = 0; i < rows; i++)
{
    var row = input[i];
    for (int j = 0; j < cols; j++)
    {
        matrix[i, j] = int.Parse(row[j].ToString());
    }
}

int[,] MakeBigger()
{
    var larger = new int[rows * 5, cols * 5];
    for (var i = 0; i < 5 * rows; i++)
    for (var j = 0; j < 5 * cols; j++)
    {
                
        if (i < rows && j < cols)
            larger[i, j] = matrix[i, j];
        else
        {
            var add = (int)Math.Floor((float)i / rows) + (int)Math.Floor((float)j / cols);
            larger[i, j] =
                matrix[i % rows, j % cols] + add > 9
                    ? matrix[i % rows, j % cols] + add - 9
                    : matrix[i % rows, j % cols] + add;

        }
    }

    return larger;
}

int CalculateLowestTotalRiskCost(int[,] matrix)
{
    var possibleDirections = new List<(int, int)>() {(0, 1), (0, -1), (1, 0), (-1, 0)};
    
    var cost = new Dictionary<(int, int), int>();

    var pq = new PriorityQueue<(int, int, int), int>();
    pq.Enqueue((0, 0, 0), 0);

    var visited = new HashSet<(int, int)>();

    while (pq.Count > 0)
    {
        var (c, row, col) = pq.Dequeue();

        if (visited.Contains((row, col)))
        {
            continue;
        }

        visited.Add((row, col));

        var key = (row, col);

        if (cost.ContainsKey(key))
        {
            cost[key] = c;
        }
        else
        {
            cost.Add(key, c);
        }

        // check if we are at the end
        if (row == rows - 1 && col == cols - 1)
            break;

        foreach (var direction in possibleDirections)
        {
            var newRow = row + direction.Item1;
            var newColumn = col + direction.Item2;
            
            //check if we are out of bounds
            if (!((0 <= newRow && newRow < rows) && (0 <= newColumn && newColumn < cols)))
            {
                continue;
            }

            pq.Enqueue((c + matrix[newRow, newColumn], newRow, newColumn), c);
        }
    }

    return cost[(rows - 1, cols - 1)];
}

var cost = CalculateLowestTotalRiskCost(matrix);

Console.WriteLine($"Part 1: Lowest total risk is: {cost}");

matrix = MakeBigger();
rows = matrix.GetLength(0);
cols = matrix.GetLength(1);

cost = CalculateLowestTotalRiskCost(matrix);

Console.WriteLine($"Part 2: Lowest total risk is: {cost}");
