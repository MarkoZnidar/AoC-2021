namespace Day4;

public class Board
{
    public int [,] Numbers;
    public int[] DrawnNumbers;
    public int nuberOfDrawsTillWin;
    public int BoardId;
    public int undrawnNumbersSum;
    public int lastDraw;

    public Board(List<int[]> nums, int[] drawnNums, int bId)
    {
        Numbers = new int[nums.Count, nums.Count];
        for (int i = 0; i < nums.Count; i++)
        {
            for (int j = 0; j < nums.Count; j++)
            {
                Numbers[i, j] = nums[i][j];
            }
        }
        
        DrawnNumbers = drawnNums;
        BoardId = bId;
        nuberOfDrawsTillWin = 999999;
        
        CalculateNuberOfDrawsTillWin(DrawnNumbers);
    }
    
    int[] GetColumn(int columnNumber, int[,] m)
    {
        return Enumerable.Range(0, m.GetLength(0))
            .Select(x => m[x, columnNumber])
            .ToArray();
    }

    int [] GetRow(int rowNumberm, int [,] m)
    {
        return Enumerable.Range(0, m.GetLength(1))
            .Select(x => m[rowNumberm, x])
            .ToArray();
    } 
    
    public void CalculateNuberOfDrawsTillWin(int[] drawnNums)
    {

        var nubersDrawn = new List<int>() {drawnNums[0], drawnNums[1], drawnNums[2], drawnNums[3]};
        var bingo = false;
        for (int i = 4; i < drawnNums.Length; i++)
        {
            nubersDrawn.Add(drawnNums[i]);

            for (int j = 0; j < 5; j++)
            {
                var column = GetColumn(j, Numbers);
                var row = GetRow(j, Numbers);
                // are all numbers in column or row drawn?
                if (column.All(x => nubersDrawn.Contains(x)) || row.All(x => nubersDrawn.Contains(x)))
                {
                    nuberOfDrawsTillWin = nubersDrawn.Count;
                    undrawnNumbersSum =  SumAllUndrawnNumbers(nubersDrawn);
                    lastDraw = nubersDrawn.Last();
                    return;
                }
            }
        }
        
        nuberOfDrawsTillWin = nubersDrawn.Count();
    }

    private int SumAllUndrawnNumbers(List<int> nubersDrawn)
    {
        var sum = 0;
        for (int i = 0; i < Numbers.GetLength(0); i++)
        {
            for (int j = 0; j < Numbers.GetLength(1); j++)
            {
                if (!nubersDrawn.Contains(Numbers[i, j]))
                {
                    sum += Numbers[i, j];
                }
            }
        }

        return sum;
    }
}