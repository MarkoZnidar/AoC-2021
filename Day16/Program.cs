// See https://aka.ms/new-console-template for more information
var input = File.ReadAllText(@"input.txt");
var bytes = Convert.FromHexString(input);

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

var bits = string.Join("", bytes.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));

var result = DecodePacket(bits);

Console.WriteLine($"Packets version number sum: {result}");