// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var size = input.Length;
var part1Matrix = new int[size, size];
var part2Matrix = new int[size, size];

// Fill matrix form input
for (int i = 0; i < size; i++)
{
    var row = input[i];
    for (int j = 0; j < size; j++)
    {
        part1Matrix[i, j] = int.Parse(row[j].ToString());
        part2Matrix[i, j] = int.Parse(row[j].ToString());
    }
}

// print matrix
void PrintMatrix(int[,] m)
{
    for (int i = 0; i < size; i++)
    {
        for (int j = 0; j < size; j++)
        {
            Console.Write(m[i, j]);
        }

        Console.WriteLine();
    }

    Console.WriteLine("-----");
}

PrintMatrix(part1Matrix);


int Part1(int[,] matrix)
{
    var flashCount = 0;
    for (var step = 1; step <= 100; step++)
    {
        var flash = new bool[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                flash[i, j] = false;
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] += 1;
            }
        }

        while (true)
        {
            var shouldContinue = false;

            var change = new int[size, size];

            // fill change with 0
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    change[i, j] = 0;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!flash[i, j] && matrix[i, j] > 9)
                    {
                        flashCount += 1;
                        flash[i, j] = true;
                        shouldContinue = true;

                        for (int posI = -1; posI < 2; posI++)
                        for (int posJ = -1; posJ < 2; posJ++)
                        {
                            // skip or you are fucked!!!
                            if (posI == 0 && posJ == 0)
                                continue;

                            var newColumn = j + posJ;
                            var newRow = i + posI;

                            // check if we are outside bounds od the matrix
                            if (!(0 <= newRow && newRow < size && 0 <= newColumn && newColumn < size))
                                continue;

                            change[newRow, newColumn] += 1;
                        }
                    }
                }
            }

            // apply changes
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] += change[i, j];
                }
            }

            if (!shouldContinue)
                break;
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (flash[i, j])
                {
                    matrix[i, j] = 0;
                }
            }
        }
    }

    return flashCount;
}

int Part2(int[,] matrix)
{
    var allFlashingOctopusCount = size * size;
    
    var step = 1;
    while (true)
    {
        var flash = new bool[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                flash[i, j] = false;
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] += 1;
            }
        }

        while (true)
        {
            var shouldContinue = false;

            var change = new int[size, size];

            // fill change with 0
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    change[i, j] = 0;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!flash[i, j] && matrix[i, j] > 9)
                    {
                        flash[i, j] = true;
                        shouldContinue = true;

                        for (int posI = -1; posI < 2; posI++)
                        for (int posJ = -1; posJ < 2; posJ++)
                        {
                            // skip or you are fucked
                            if (posI == 0 && posJ == 0)
                                continue;

                            var newColumnt = j + posJ;
                            var newRow = i + posI;

                            // check if we are outside bounds od the matrix
                            if (!(0 <= newRow && newRow < size && 0 <= newColumnt && newColumnt < size))
                                continue;

                            change[newRow, newColumnt] += 1;
                        }
                    }
                }
            }

            // apply changes
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] += change[i, j];
                }
            }

            if (!shouldContinue)
                break;
        }
        
        var flashCount = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (flash[i, j])
                {
                    matrix[i, j] = 0;
                    flashCount++;
                }
            }
        }

        if (flashCount == allFlashingOctopusCount)
        {
            PrintMatrix(matrix);
            break;
        }
        
        step++;
    }

    return step;

}

Console.WriteLine($"Part 1: After 100 steps there are {Part1(part1Matrix)} flashes");
Console.WriteLine($"Part 2: Step on which all octopuses flash: {Part2(part2Matrix)}");