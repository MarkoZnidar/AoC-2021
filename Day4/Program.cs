// See https://aka.ms/new-console-template for more information

using Day4;

// var input = File.ReadAllText(@"demo.txt")
//     .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var input = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var drawnNumbers = input[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

Console.WriteLine($"Drawn numbers: {string.Join(", ", drawnNumbers)}");

var boards = new List<Board>();

var b = new List<int[]>();
var bId = 1;
for(var i = 1; i < input.Length; i++)
{
    var line = input[i];
    var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    b.Add(numbers);

    if(i % 5 == 0)
    {
        boards.Add(new Board(b, drawnNumbers, bId));
        b = new List<int[]>();
        bId++;
    }
}

Console.WriteLine($"Boards count is: {boards.Count}");

var lowestNumberOfDrawsTillWin = boards.Min(b => b.nuberOfDrawsTillWin);
var winingBoard = boards.FindAll(b => b.nuberOfDrawsTillWin == lowestNumberOfDrawsTillWin).FirstOrDefault();
Console.WriteLine($"Part 1: Board {winingBoard.BoardId}, Number of draws till win: {winingBoard.nuberOfDrawsTillWin}, " +
                  $"Sum of undrawn numbers: {winingBoard.undrawnNumbersSum} " +
                  $"Last draw: {winingBoard.lastDraw} Score: {winingBoard.lastDraw * winingBoard.undrawnNumbersSum}");

var highestNumberOfDrawsTillWin = boards.Max(b => b.nuberOfDrawsTillWin);
var losingBoard = boards.FindAll(b => b.nuberOfDrawsTillWin == highestNumberOfDrawsTillWin).FirstOrDefault();
Console.WriteLine($"Part 2: Board {losingBoard.BoardId}, Number of draws till win: {losingBoard.nuberOfDrawsTillWin}, " +
                  $"Sum of undrawn numbers: {losingBoard.undrawnNumbersSum} " +
                  $"Last draw: {losingBoard.lastDraw} Score: {losingBoard.lastDraw * losingBoard.undrawnNumbersSum}");



