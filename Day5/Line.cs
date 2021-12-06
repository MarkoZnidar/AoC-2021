namespace Day5;

public class Line
{
    public int x1;
    public int y1;
    public int x2;
    public int y2;

    public bool isVertical()
    {
        return y1 == y2;
    }
    
    public bool isHorizontal()
    {
        return x1 == x2;
    }

    public string PrintHorizontal()
    {
        if (isHorizontal())
        {
            return "yes";
        }
        
        return "no";
    }
    
    public string PrintVertical()
    {
        if (isVertical())
        {
            return "yes";
        }
        
        return "no";
    }

    public void Print()
    {
        Console.WriteLine($"{x1},{y1} -> {x2},{y2}");
    }
    
}