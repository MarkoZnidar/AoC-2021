// See https://aka.ms/new-console-template for more information
var input = File.ReadAllText(@"input.txt");

var data = input.Replace("target area: x=","");
var splitedData = data.Split(", y=");

var rangeY = (int.Parse(splitedData[1].Split("..")[0]),int.Parse(splitedData[1].Split("..")[1]));
var rangeX = (int.Parse(splitedData[0].Split("..")[0]),int.Parse(splitedData[0].Split("..")[1]));

var target = (x_range: rangeX, y_range: rangeY);

((int, int),(int, int)) GoThrough((int, int) position, (int, int) velocity)
{
    var newPosition = (0,0);
    var newVelocity = (0,0);
    
    newPosition.Item1 = position.Item1 + velocity.Item1;
    newPosition.Item2 = position.Item2 + velocity.Item2;
    
    newVelocity.Item2 = velocity.Item2 - 1;

    if (velocity.Item1 > 0)
    {
        newVelocity.Item1 = velocity.Item1 - 1;
    }
    
    if (velocity.Item1 < 0)
    {
        newVelocity.Item1 = velocity.Item1 + 1;
    }

    return (newPosition, newVelocity);
}

bool IsWhitin((int, int) position, ((int, int),(int, int)) target)
{
    return ((target.Item1.Item1 <= position.Item1 && position.Item1 <= target.Item1.Item2) && 
            (target.Item2.Item1 <= position.Item2 && position.Item2 <= target.Item2.Item2));  
}

bool IsPast((int, int) position, (int, int) velocity, ((int, int), (int, int)) target)
{
    if (velocity.Item1 > 0 && position.Item1 > target.Item1.Item2)
        return true;
    
    if (velocity.Item1 < 0 && position.Item1 < target.Item1.Item1)
        return true;
    
    if (velocity.Item2 < 0 && position.Item2 < target.Item2.Item1)
        return true;

    return false;
}

(bool, int?) IsHit((int, int) velocity, ((int, int), (int, int)) target)
{
    var position = (0, 0);
    var MaxY = 0;

    while (!IsPast(position,velocity,target))
    {
        MaxY = Math.Max(MaxY, position.Item2);

        if (IsWhitin(position, target))
        {
            return (true, MaxY);
        }

        (position, velocity) = GoThrough(position, velocity);
    }
    
    return (false, null);
}

// We compute the max Y velocity to avoid an infinite loop 
var maxYVelocity = Math.Abs(target.Item2.Item1);

var yVelocity = maxYVelocity;

var resultMaxY = 0;

while (yVelocity >= target.Item2.Item1)
{
    var done = false;
    for (var xVelocity = -100; xVelocity < 101; xVelocity++)
    { 
        var (works, maxY) = IsHit((xVelocity, yVelocity), target);   
        if (works)
        {
            resultMaxY = maxY.Value;
            done = true;
            break;
        }
    }

    if (done)
    {
        break;
    }
    
    yVelocity--;
}

//Part 2:
var result = 0;
yVelocity = maxYVelocity;
while (yVelocity >= target.Item2.Item1)
{
    for (var xVelocity = -100; xVelocity < 101; xVelocity++)
    { 
        var (works, maxY) = IsHit((xVelocity, yVelocity), target);   
        if (works)
        {
            result++;
        }
    }

    yVelocity--;
}

Console.WriteLine($"Part 1: Highest Y position: {resultMaxY}");
Console.WriteLine($"Part 2: Distinct initial velocity values within target area: {result}");
