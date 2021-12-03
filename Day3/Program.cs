// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText(@"demo.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

// var input = File.ReadAllText(@"input.txt")
//     .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var rows = input.Length;
var cols = input[0].Length;

var matrix = new string[rows, cols];
for (var i = 0; i < rows; i++)
{
    for (var j = 0; j < cols; j++)
    {
        matrix[i, j] = input[i][j].ToString();
    }
}

string[] GetColumn(int columnNumber, string[,] m)
{
    return Enumerable.Range(0, m.GetLength(0))
        .Select(x => m[x, columnNumber])
        .ToArray();
}

string [] GetRow(int rowNumberm, string [,] m)
{
    return Enumerable.Range(0, m.GetLength(1))
        .Select(x => m[rowNumberm, x])
        .ToArray();
}

void PrintMatrix(string[,] m)
{
    for (var i = 0; i < m.GetLength(0); i++)
    {
        for (var j = 0; j < m.GetLength(1); j++)
        {
            Console.Write(m[i, j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine("----------------");
}

(int, int) CalculateGamaAndEpsilonRate()
{
    var gamaRate = string.Empty;
    var epsilonRate = string.Empty;

    for(int i = 0; i < matrix.GetLength(1); i++)
    {
        var column = GetColumn(i, matrix);
    
        var zeroCount = column.Count(x => x == "0");
        var oneCount = column.Count(x => x == "1");

        if (zeroCount > oneCount)
        {
            gamaRate += "0";
            epsilonRate += "1";
        }
        else
        {
            gamaRate += "1";
            epsilonRate += "0";
        }
    }
    
    return (Convert.ToInt32(gamaRate, 2), Convert.ToInt32(epsilonRate, 2));
}

int CalculateRating(string[,] mat, bool fewerCriteria = false)
{
    var rating = string.Empty;
    
    for (int c = 0; c < mat.GetLength(1); c++)
    {
        mat = GetNewMatrixByCriteria(mat, c, fewerCriteria);
        PrintMatrix(mat);

        if (mat.GetLength(0) == 1)
        {
            rating = string.Join("", GetRow(0, mat));
            break;
        }
    }
    
    return Convert.ToInt32(rating, 2);;
}

string[,] GetNewMatrixByCriteria(string [,] mtr, int position, bool feverCriteria)
{
    var column = GetColumn(position, mtr);

    var zeroCount = column.Count(x => x == "0");
    var oneCount = column.Count(x => x == "1");

    var considerBit = "0";

    if (oneCount > zeroCount || oneCount == zeroCount )
    {
        considerBit = "1";
    }
    
    if (feverCriteria)
    {
        considerBit = considerBit == "1" ? "0" : "1";
    }
    
    var rows = Enumerable.Range(0, mtr.GetLength(0))
        .Where(x => mtr[x, position] == considerBit)
        .ToArray();
    
    if (rows.Length == 0)
    {
        return null;
    }

    var newMatrix = new string[rows.Length, mtr.GetLength(1)];
    for (var i = 0; i < rows.Length; i++)
    {
        for (var j = 0; j < mtr.GetLength(1); j++)
        {
            newMatrix[i, j] = mtr[rows[i], j];
        }
    }
    
    return newMatrix;
}

var (gama, epsilon) = CalculateGamaAndEpsilonRate();

Console.WriteLine($"Part 1: Gamma rate: {gama}, Epsilon rate: {epsilon}, Power consumption: {gama * epsilon}");

var oxygenGeneratorRating = CalculateRating(matrix);
var co2ScrubberRating = CalculateRating(matrix, true);

Console.WriteLine($"Part 2: Oxygen generation rate: {oxygenGeneratorRating}, CO2 scrubbing rate: {co2ScrubberRating}" +
                  $"Life support rating: {oxygenGeneratorRating * co2ScrubberRating}");





