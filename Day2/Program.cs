// See https://aka.ms/new-console-template for more information

var directions = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

int CalculateHorizontalAndDepth(bool part2)
{
    var horizontal = 0;
    var depth = 0;
    var aim = 0;
    
    foreach (var direction in directions)
    {
        var directionParsed = direction.Split(' ');
        var directionType = directionParsed[0];
        var directionValue = int.Parse(directionParsed[1]);

        switch (directionType)
        {
            case "up":
                if (!part2)
                    depth -= directionValue;

                aim -= directionValue;
                break;
            case "down":
                if (!part2)
                    depth += directionValue;
                aim += directionValue;
                break;
            case "forward":
                horizontal += directionValue;
                if (part2)
                    depth += (aim * directionValue);
                break;
        }
    }

    return horizontal * depth;
}

Console.WriteLine($"Part 1: Final horizontal position multiplied by depth: {CalculateHorizontalAndDepth(false)}");
Console.WriteLine($"Part 2: Final horizontal position multiplied by depth: {CalculateHorizontalAndDepth(true)}");


