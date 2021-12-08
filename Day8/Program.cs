// See https://aka.ms/new-console-template for more information

var digitsSegmentsDict = new Dictionary<string, int>();
digitsSegmentsDict.Add("abcefg", 0);
digitsSegmentsDict.Add("cf", 1);
digitsSegmentsDict.Add("acdeg", 2);
digitsSegmentsDict.Add("acdfg", 3);
digitsSegmentsDict.Add("bcdf", 4);
digitsSegmentsDict.Add("abdfg", 5);
digitsSegmentsDict.Add("abdefg", 6);
digitsSegmentsDict.Add("acf", 7);
digitsSegmentsDict.Add("abcdefg", 8);
digitsSegmentsDict.Add("abcdfg", 9);

// var input = File.ReadAllText(@"example.txt").Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToArray();
//var input = File.ReadAllText(@"demo.txt").Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToArray();
 var input = File.ReadAllText(@"input.txt").Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToArray();

int CalculatePart1(string[] strings)
{
    var count = 0;
    foreach (var line in strings)
    {
        var output = line.Split("|")[1].TrimStart().Split(" ").ToArray();

        for (int i = 0; i < output.Length; i++)
        {
            var value = digitsSegmentsDict.Where(x => x.Key.Length == output[i].Length).Select(x => x.Value)
                .FirstOrDefault();
            if (new[] {1, 4, 7, 8}.Contains(value))
            {
                count++;
            }
        }
    }

    return count;
}

List<string> GetPosibleSegmentsByCommonDiff(string[] segments,  string compareToSegment, int commonSegmentsCount)
{
    var possibleSegments = new List<string>();
    
    for (int i = 0; i < segments.Length; i++)
    {
        var commonCount = 0;

        foreach (var letter in compareToSegment)
        {
            if (segments[i].Contains(letter))
            {
                commonCount++;
            }
        }

        if (commonCount == commonSegmentsCount)
        {
            possibleSegments.Add(segments[i]);
        }
    }

    return possibleSegments;
}

int CalculatePart2(string[] strings)
{
    var sum = 0;
    
    var segmentsMap = new string[10];

    foreach (var line in strings)
    {
        var leftPart = line.TrimEnd().Split("|")[0].TrimEnd().Split(" ").ToArray();
        
        for (int i = 0; i < leftPart.Length; i++)
        {
            // find 1, 4,7,8
            var value = digitsSegmentsDict.Where(x => x.Key.Length == leftPart[i].Length).Select(x => x.Value)
                .FirstOrDefault();

            if (new[] {1, 4, 7, 8}.Contains(value))
            {
                switch (leftPart[i].Length)
                {
                    case 2:
                        segmentsMap[1] = leftPart[i];
                        break;
                    case 3:
                        segmentsMap[7] = leftPart[i];
                        break;
                    case 4:
                        segmentsMap[4] = leftPart[i];
                        break;
                    case 7:
                        segmentsMap[8] = leftPart[i];
                        break;
                }
            }
        }
    
        var NineSixZero = leftPart.Where(l => l.Length == 6).ToArray();
        var twoThreeFive = leftPart.Where(l => l.Length == 5).ToArray();
    
        // out of 6,9 and 0, 0 and 6 have 3 segments in common with four
        var posible = GetPosibleSegmentsByCommonDiff(NineSixZero, segmentsMap[4], 3);
    
        // get zero by comparing common parts with four and seven
        segmentsMap[0] = GetPosibleSegmentsByCommonDiff(posible.ToArray(), segmentsMap[7], 3)[0];
    
        // six
        segmentsMap[6] = posible.Where(x => !x.Contains(segmentsMap[0])).ToArray()[0];
    
        // nine 
        segmentsMap[9] = NineSixZero.Where(x => !x.Contains(segmentsMap[0]) && !x.Contains(segmentsMap[6])).ToArray()[0];
        
        // 3 has 3 segments common with 7
        segmentsMap[3] = GetPosibleSegmentsByCommonDiff(twoThreeFive,segmentsMap[7],3)[0];
     
        // 5 and 3 have 3 segments common with 7
        posible = GetPosibleSegmentsByCommonDiff(twoThreeFive,segmentsMap[4],3);
    
        segmentsMap[5] = posible.Where(x=>!x.Contains(segmentsMap[3])).ToArray()[0];

        segmentsMap[2] = twoThreeFive.Where(x => !x.Contains(segmentsMap[5]) && !x.Contains(segmentsMap[3])).ToArray()[0];
        
        //Console.WriteLine($"1={segmentsMap[1]}\n2={segmentsMap[2]}\n3={segmentsMap[3]}\n4={segmentsMap[4]}\n5={segmentsMap[5]}\n6={segmentsMap[6]}\n7={segmentsMap[7]}\n8={segmentsMap[8]}\n9={segmentsMap[9]}\n0={segmentsMap[0]}");
        
        var output = line.Split("|")[1].TrimStart().Split(" ").ToArray();

        var outputDigit = string.Empty;
        
        foreach (var o in output)
        {
            var digit = string.Empty;
            for (int i = 0; i < segmentsMap.Length; i++)
            {
                var item = segmentsMap[i];
                
                //sort item
                var sortedItem = string.Join("", item.OrderBy(x => x));
                
                //sort output
                var sortedOutput = string.Join("", o.OrderBy(x => x));
                
                if (sortedItem == sortedOutput)
                {
                    digit = i.ToString();
                    break;
                }
            }
            
            //Console.WriteLine($"{o}={digit}");
            
            outputDigit += digit;
        }
        
        //Console.WriteLine($"{string.Join(" ", output)}: {int.Parse(outputDigit)}");
        sum += int.Parse(outputDigit);
    }
    
    //Console.WriteLine($"Part 2: {sum}");
    return sum;

}

var counter = CalculatePart1(input);
Console.WriteLine($"Part 1: Digits 1, 4, 7, 8 appear {counter} times");

var sum = CalculatePart2(input);
Console.WriteLine($"Part 2 Sum of output values is: {sum}");