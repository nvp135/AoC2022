Calculate("input1.txt", 4);
Calculate("input1.txt", 14);

void Calculate(string filePath, int distinctCharacters)
{
    distinctCharacters--;
    using (var reader = new StreamReader(filePath))
    {
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();

            for (int i = distinctCharacters; i < line.Length; i++)
            {
                var substr = line.Substring(i - distinctCharacters, distinctCharacters + 1);
                var isFirst = true;
                for (int j = 0; j < distinctCharacters; j++)
                {
                   if(substr.Count(x => x == substr[j]) > 1)
                    {
                        isFirst = false;
                        break;
                    }
                }
                if (isFirst)
                {
                    Console.WriteLine($"Result is: {i + 1}");
                    break;
                }
            }
        }
    }
}

//( ^ ) != 0