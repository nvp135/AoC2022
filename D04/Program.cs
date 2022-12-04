
CalculateContainsCount("input1.txt");

void CalculateContainsCount(string filePath)
{
    int result_p1 = 0;
    int result_p2 = 0;
    using (var reader = new StreamReader(filePath))
    {
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();
            var pairs = line.Split(',').Select(x => x.Split('-').Select(Int32.Parse).ToArray()).ToArray();
            if ((pairs[0][0] >= pairs[1][0] && pairs[0][1] <= pairs[1][1]) || 
                (pairs[1][0] >= pairs[0][0] && pairs[1][1] <= pairs[0][1]))
            {
                result_p1++;
            }
            if ((pairs[0][0] >= pairs[1][0] && pairs[0][0] <= pairs[1][1]) ||
                (pairs[0][1] >= pairs[1][0] && pairs[0][1] <= pairs[1][1]) ||
                (pairs[1][0] >= pairs[0][0] && pairs[1][0] <= pairs[0][1]) ||
                (pairs[1][1] >= pairs[0][0] && pairs[1][1] <= pairs[0][1]))
            {
                result_p2++;
            }
        }
    }
    Console.WriteLine($"Result part 1 is {result_p1}{Environment.NewLine}Result part 2 is {result_p2}");
}