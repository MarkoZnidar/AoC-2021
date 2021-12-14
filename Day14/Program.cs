// See https://aka.ms/new-console-template for more information
var input = File.ReadAllText(@"input.txt").Split('\n');

var template = input[0];
var rules = new List<string[]>();

for (int i = 2; i < input.Length; i++)
{
    var line = input[i];
    var r = line.Split(" -> ");
    
    rules.Add(new string[]
    {
        new(r[0]),
        new(r[1])
    });
}

var boxes = new Dictionary<string, long>();

for(int i=0; i< template.Length-1; i++)
{
    var key = template.Substring(i, 2);
    if (boxes.ContainsKey(key))
    {
        boxes[key]++;
    }
    else
    {
        boxes[key] = 1;
    }
}

Dictionary<string, long> Replace(Dictionary<string, long> bxs)
{
    var newBoxes = new Dictionary<string, long>();
    foreach (var f in bxs)
    {
        newBoxes.Add(f.Key, f.Value);
    }

    foreach (var box in bxs)
    {
        foreach (var rule in rules)
        {
            var start = rule[0];
            var end = rule[1];

            if (box.Key == start)
            {
                var occurance = bxs[box.Key];
                newBoxes[box.Key] -= occurance;
                var newKey = box.Key[0] + end;
                if (newBoxes.ContainsKey(newKey))
                {
                    newBoxes[newKey] += occurance;
                }
                else
                {
                    newBoxes.Add(newKey, occurance);
                }

                newKey = end + box.Key[1];

                if (newBoxes.ContainsKey(newKey))
                {
                    newBoxes[newKey] += occurance;
                }
                else
                {
                    newBoxes.Add(newKey, occurance);
                }
                break;
            }
        }
    }

    return newBoxes;

}

var valuesCounter = new List<long>();


for(int i=1; i<=40; i++)
{
    boxes = Replace(boxes);
    if (i == 10)
    {
         valuesCounter = CountValues();
    }
    
    
}

var valuesCounterPart2 = CountValues();

List<long> CountValues()
{
    var count = new Dictionary<string, long>();

    foreach (var key in boxes.Keys)
    {
        var newKey = key[0].ToString();
        if (count.ContainsKey(newKey))
        {
            count[newKey] += boxes[key];
        }
        else
        {
            count.Add(newKey, boxes[key]);
        }

        newKey = key[1].ToString();
        if (count.ContainsKey(newKey))
        {
            count[newKey] += boxes[key];
        }
        else
        {
            count.Add(newKey, boxes[key]);
        }
    }

    count[template[0].ToString()] += 1;
    count[template[template.Length - 1].ToString()] += 1;

    var pair = new List<long>();

    foreach (var c in count)
    {
        pair.Add(c.Value / 2);
    }

    return pair;
}



Console.WriteLine($"Part 1: Max {valuesCounter.Max()}");
Console.WriteLine($"Part 1: Min {valuesCounter.Min()}");
Console.WriteLine($"Part 1: Result: {valuesCounter.Max()-valuesCounter.Min()}");

Console.WriteLine($"Part 2: Max {valuesCounterPart2.Max()}");
Console.WriteLine($"Part 2: Min {valuesCounterPart2.Min()}");
Console.WriteLine($"Part 2: Result: {valuesCounterPart2.Max()-valuesCounterPart2.Min()}");