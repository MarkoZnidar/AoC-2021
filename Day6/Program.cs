// See https://aka.ms/new-console-template for more information

// var input = File.ReadAllText(@"demo.txt")
//     .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var input = File.ReadAllText(@"input.txt")
    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

var fishes = input[0].Split(',').Select(x => int.Parse(x)).ToList();

void Part1()
{
    Console.WriteLine($"Initial state: {string.Join(",", fishes)}");
    
    var day = 1;
    var createNewFishOnNextDay = 0;
    do
    {
        for (int element = 0; element < fishes.Count; element++)
        {
            if (fishes[element] == 0)
            {
                fishes[element] = 6;
                createNewFishOnNextDay++;
            }
            else
            {
                fishes[element]--;
            }
        }

        if (createNewFishOnNextDay > 0)
        {
            for (int z = 0; z < createNewFishOnNextDay; z++)
            {
                fishes.Add(8);
            }

            createNewFishOnNextDay = 0;
        }
        
        Console.Clear();
        Console.WriteLine($"Day {day}");


        day++;
    } while (day <= 256);

    Console.WriteLine($"Part 1: After {day - 1} days there are: {fishes.Count} fishes!");

}

void Part2()
{
    long[] initialState = File.ReadAllText(@"input.txt").Split(',').Select(x => long.Parse(x)).ToArray();

    var days = 256;
    
    long[] boxes = new long[9];

    foreach(int fishNum in initialState)
    {
        boxes[fishNum]++;
    }

    for (int day = 1; day <= days; day++)
    {
        long new_fish = boxes[0];

        for (int i = 0; i < boxes.Length-1; i++)
        {
            boxes[i] = boxes[i+1];
        }

        boxes[6] += new_fish;
        boxes[8] = new_fish;
    }

    long sum = 0;

    for (int boxNumber = 0; boxNumber < boxes.Length; boxNumber++)
    {
        Console.WriteLine($"Fishes in box {boxNumber}: {boxes[boxNumber]}");
        sum += boxes[boxNumber];
    }

    Console.WriteLine($"Total god damn fishes: {sum}");
}

//Part1();
Part2();



