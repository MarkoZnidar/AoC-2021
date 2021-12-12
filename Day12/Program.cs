// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);


var parsedInput = new List<string[]>();

foreach (var line in input)
{
    var splitedLine = line.Split("-");
    parsedInput.Add(splitedLine);
}

var pathMap = new Dictionary<string, string[]>();

foreach (var line in parsedInput)
{
    var from = line[0];
    var to = line[1];
    
    // we add the path to dict
    if (!pathMap.ContainsKey(from))
    {
        pathMap[from] = new string[] {to};
    }
    else
    {
        pathMap[from] = pathMap[from].Append(to).ToArray();
    }
    
    // we add the reverse path also
    if (!pathMap.ContainsKey(to))
    {
        pathMap[to] = new string[] {from};
    }
    else
    {
        pathMap[to] = pathMap[to].Append(from).ToArray();
    }
    
}

var visited = new List<string>();
var distinctVisited = new Dictionary<string, int>();

var pathCount = 0;
var pathCount2 = 0;

bool AreWeInSmallCave(string cave)
{
    return cave == cave.ToLower();
}

// Part 1:
void Explore(string location)
{
    if (location == "end")
    {
        pathCount++;
        return;
    }

    if (AreWeInSmallCave(location) && visited.Contains(location))
        return;

    if (AreWeInSmallCave(location))
    {
        visited.Add(location);
    }
    
    foreach (var path in pathMap[location])
    {
        //We don't want to start again
        if (path == "start")
        {
            continue;
        }

        Explore(path);
    }
    
    if (AreWeInSmallCave(location))
    {
        visited.Remove(location);
    }
}

// Part 2:
void ExploreSmallCaveUpToOnceTwice(string location)
{
    if (location == "end")
    {
        pathCount2++;
        return;
    }

    if (AreWeInSmallCave(location))
    {
        
        if (!distinctVisited.ContainsKey(location))
        {
            distinctVisited[location] = 1;
        }
        else
        {
            distinctVisited[location] ++;
        }

        var moreThenOnce = 0;
        foreach (var key in distinctVisited.Keys)
        {
            moreThenOnce += distinctVisited[key] > 1 ? 1 : 0;
            
            // we can't wisit a small cave more then twice
            if (distinctVisited[key] > 2)
            {
                
                distinctVisited[location] --;
                return;
            }
        }
        
        if (moreThenOnce > 1)
        {
            distinctVisited[location] --;
            return;
        }

    }
    
    foreach (var path in pathMap[location])
    {
        //We don't want to start again
        if (path == "start")
        {
            continue;
        }

        ExploreSmallCaveUpToOnceTwice(path);
    }
    
    if (AreWeInSmallCave(location))
    {
        distinctVisited[location]--;
    }
}

Explore("start");
ExploreSmallCaveUpToOnceTwice("start");

Console.WriteLine($"Part 1: Number of paths:: {pathCount}");
Console.WriteLine($"Part 2: Number of paths: {pathCount2}");
