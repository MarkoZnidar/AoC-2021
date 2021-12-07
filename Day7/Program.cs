// See https://aka.ms/new-console-template for more information

// var input = File.ReadAllText(@"demo.txt")
//     .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x=> int.Parse(x)).ToArray();

var input = File.ReadAllText(@"input.txt")
    .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x=> int.Parse(x)).ToArray();

int PartialSum(int n)
{
    return n*(n+1)/2;
}

var max = input.Max();

var positions = new int[max+1];
for (int v = 0; v < positions.Length; v++)
{
    positions[v] = v;
}

Dictionary<int, int> Part1FuelCost()
{
    var dictionary = new Dictionary<int, int>();


    foreach (var move in positions)
    {
        var cost = 0;
        for (int i = 0; i < input.Length; i++)
        {
            cost += Math.Abs(input[i] - move);
        }

        dictionary.Add(move, cost);
    }

    return dictionary;
}

Dictionary<int, int> Part2FuelCost()
{
    var dictionary = new Dictionary<int, int>();


    foreach (var move in positions)
    {
        var cost = 0;
        for (int i = 0; i < input.Length; i++)
        {
            cost += PartialSum(Math.Abs(input[i] - move));
        }

        dictionary.Add(move, cost);
    }

    return dictionary;
}

var fuelCost = Part1FuelCost();
var lowestCostKey = fuelCost.OrderBy(x => x.Value).First().Key;

Console.WriteLine($"Part 1: Horizontal position with lowest cost is: {lowestCostKey}, it costs: {fuelCost[lowestCostKey]} fuel.");

fuelCost = Part2FuelCost();
lowestCostKey = fuelCost.OrderBy(x => x.Value).First().Key;

Console.WriteLine($"Part 2: Horizontal position with lowest cost is: {lowestCostKey}, it costs: {fuelCost[lowestCostKey]} fuel.");
    