using System.IO;

var priorityList = new Dictionary<char, int>();

for (int i = 1; i <= 26; i++)
{
    priorityList.Add((char)(i + 96), i);
    priorityList.Add((char)(i + 64), i + 26);
}

CalculateInLine("input1.txt");
CalculateInGroup("input1.txt");

void CalculateInLine(string filePath)
{
    int result = 0;
    using (var reader = new StreamReader(filePath))
    {
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();

            var part1 = line.Substring(0, line.Length / 2);
            var part2 = line.Substring(line.Length / 2, line.Length / 2);

            foreach (var c in part1)
            {
                if (part2.Contains(c))
                {
                    result += priorityList[c];
                    part2 = part2.Replace(c, '-');
                }
            }
        }
    }
    Console.WriteLine($"Result is {result}");
}

void CalculateInGroup(string filePath)
{
    int result = 0;
    using (var reader = new StreamReader(filePath))
    {

        while (reader.Peek() >= 0)
        {
            var line1 = reader.ReadLine();
            var line2 = reader.ReadLine();
            var line3 = reader.ReadLine();

            foreach (var c in line1)
            {
                if (line2.Contains(c) && line3.Contains(c))
                {
                    result += priorityList[c];
                    line2 = line2.Replace(c, '-');
                    line3 = line3.Replace(c, '-');
                }
            }
        }
    }
    Console.WriteLine($"Result is {result}");
}