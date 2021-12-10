// See https://aka.ms/new-console-template for more information

var input = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var scoreRules = new Dictionary<string, int>()
{
    {")", 3},
    {"}", 1197},
    {">", 25137},
    {"]", 57}
};

var scoreCompletionRules = new Dictionary<string, int>()
{
    {"(", 1},
    {"[", 2},
    {"{", 3},
    {"<", 4}
};

var bracketPairs = new List<string>()
{
    "()", "{}", "[]", "<>"
};

int CheckLine(string line)
{
    var temp = new List<string>();
    
    foreach (var c in line)
    {
        var isGood = false;

        foreach (var pair in bracketPairs)
        {
            if (c == pair[0])
            {
                temp.Add(c.ToString());
                isGood = true;
            }
            else if (c == pair[1])
            {
                if (temp[temp.Count - 1] == pair[0].ToString())
                {
                    temp.RemoveAt(temp.Count - 1);
                    isGood = true;
                }
            }
        }

        if (!isGood)
        {
            return scoreRules[c.ToString()];
        }
    }

    return 0;
}

long CompleteBracketPair(string line)
{
    long score = 0;
    var temp = new List<string>();

    foreach (var c in line)
    {
        foreach (var pair in bracketPairs)
        {
            if (c == pair[0])
            {
                temp.Add(c.ToString());
            }
            else if (c == pair[1])
            {
                if (temp[temp.Count - 1] == pair[0].ToString())
                {
                    temp.RemoveAt(temp.Count - 1);
                }
            }
        }
    }
    
    for(var i = temp.Count - 1; i >= 0; i--)
    {
        score *= 5;
        score += scoreCompletionRules[temp[i]];
    }
    
    // Console.WriteLine(score);
    return score;

}

var sum = 0;
foreach (var line in input)
{
    sum += CheckLine(line);
}

Console.WriteLine($"Part 1: Total syntax error score is: {sum}");

var uncorrupted = new List<string>();
foreach (var line in input)
{
    if (CheckLine(line) == 0)
    {
        uncorrupted.Add(line);
    }
}

var sums = new List<long>();
foreach(var line in uncorrupted)
{
    sums.Add(CompleteBracketPair(line));
}

sums.Sort();

//Console.WriteLine($"Sorted sums: {string.Join(", ", sums)}");

var middle = sums[sums.Count / 2];

Console.WriteLine($"Part 2: The middle score of the uncorrupted lines is: {middle}");



