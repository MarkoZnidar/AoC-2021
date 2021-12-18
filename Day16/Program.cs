// See https://aka.ms/new-console-template for more information
var input = File.ReadAllText(@"input.txt");
var bytes = Convert.FromHexString(input);

var bits = string.Join("", bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

int DecodePacket(string packet, int count = -1)
{
    if (packet == "" || packet.ToCharArray().All(c => c == '0'))
        return 0;

    if (count == 0)
        return DecodePacket(packet);
    
    var tId = Convert.ToInt32(packet[3..6],2);
    var version = Convert.ToInt32(packet[0..3],2);
    
    // literal packet
    if (tId == 4)
    {
        var i = 6;

        var number = string.Empty;

        var end = false;
        while (!end)
        {
            // last packet
            if (packet[i] == '0')
                end = true;
            
            number += packet[(i + 1)..(i + 5)];
            i += 5;
        }
        
        return version + DecodePacket(packet[i..], count - 1);
        
    }
    // operator
    var lenId = packet[6];

    if (lenId == '0')
    {
        // 15 bits tell us how many packets are inside
        var nubmerOfBits = Convert.ToInt32(packet[7..22],2);
        return version + DecodePacket(packet[22..(22 + nubmerOfBits)]) + DecodePacket(packet[(22 + nubmerOfBits)..], count - 1);
    }

    // 11 bits tell us how many packets are inside
    var numberOfPacketsInside = Convert.ToInt32(packet[7..18],2);
    return version + DecodePacket(packet[18..], numberOfPacketsInside);

}

(long?, int?) DecodePacketPart2(int? i, int? j=-1)
{
    if (i == j)
    {
        return (null, null);
    }
    
    // no usefull bits anymore break out
    if (i > bits.Length -4)
    {
        return (null, null);
    }
    
    var tId = Convert.ToInt32(bits[(Range) ((i + 3)..(i + 6))],2);
    
    // literal packet
    if (tId == 4)
    {
        i += 6;

        var number = string.Empty;
        var end = false;
        
        while (!end)
        {
            // last packet
            if (bits[(int) i] == '0')
                end = true;
            
            number += bits[(Range) ((i + 1)..(i + 5))];
            i += 5;
        }
        
        var val = Convert.ToInt64(number,2);
        return (val, i);
    }
    // operator
    
    var subPacks = new List<long?>();
    int? nextStart = null;
    
    var lenId = bits[(int) (i + 6)];

    if (lenId == '0')
    {
        // 15 bits tell us how many bits are inside
        var nubmerOfBits = Convert.ToInt32(bits[(Range) ((i + 7)..(i + 22))],2);
        var end = i + 22 + nubmerOfBits;
        var index = i + 22;
        int? previousIndex = null;

        while (index.HasValue)
        {
            previousIndex = index;
            var returnResult = DecodePacketPart2(index, end);
            var val = returnResult.Item1;
            index = returnResult.Item2;
            subPacks.Add(val);
        }
        
        // remove the last null
        subPacks.RemoveAt(subPacks.Count - 1);
        nextStart = previousIndex;
    }
    else
    {
        // 11 bits tell us how many packets are inside
        var remainingSubPacks = Convert.ToInt32(bits[(Range) ((i + 7)..(i + 18))],2);
        var index = i + 18;

        while (remainingSubPacks > 0)
        {
            var returnResult = DecodePacketPart2(index);
            var val = returnResult.Item1;
            index = returnResult.Item2;
            subPacks.Add(val);
            remainingSubPacks--;
        }
        
        nextStart = index;
    }
    
    //now let's process the operations
    return (DoOperation(tId, subPacks), nextStart);
    
}

long DoOperation(int? typeId, List<long?> values)
{
    if (typeId == 0)
    {
        return (long) values.Sum();
    }
    
    if (typeId == 1)
    {
        long product = 1;
        foreach (var value in values)
        {
            product *= (long)value;
        }

        return product;
    }
    
    if (typeId == 2)
    {
        return (long) values.Min();
    }
    
    if (typeId == 3)
    {
        return (long) values.Max();
    }
    
    if (typeId == 5)
    {
        if (values.Count == 2)
            return ((long) values[0] > (long) values[1]) ? 1 : 0;
    }
    
    if (typeId == 6)
    {
        if (values.Count == 2)
            return ((long) values[0] < (long) values[1]) ? 1 : 0;
    }
    
    
    if (typeId == 7)
    {
        if (values.Count == 2)
            return ((long) values[0] == (long) values[1]) ? 1 : 0;
    }

    return 0;

}

long result = DecodePacket(bits);

Console.WriteLine($"Part 1: Packets version number sum: {result}");

result = DecodePacketPart2(0).Item1.Value;

Console.WriteLine($"Part 2: Expression result: {result}");