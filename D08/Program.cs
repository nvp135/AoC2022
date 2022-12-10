
Calculate(ReadLines("input1.txt"));


char[][] ReadLines(string filePath)
{
    var lines = new List<char[]>();
    using (var reader = new StreamReader(filePath))
    {
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine().ToCharArray());
        }
    }
    return lines.ToArray();
}

void Calculate(char[][] matrix)
{
    var visibility = new bool[matrix.Length][];
    for (int i = 0; i < matrix.Length; i++)
    {
        var line = new bool[matrix[i].Length];
        visibility[i] = line;
    }

    for (int i = 0; i < visibility.Length; i++)
    {
        visibility[i][0] = true;
        visibility[i][visibility.Length-1] = true;
    }

    for (int i = 0; i < visibility[0].Length; i++)
    {
        visibility[0][i] = true;
        visibility[visibility[0].Length - 1][i] = true;
    }

    int scenicScore = 0;

    for (int y = 1; y < visibility.Length - 1; y++)
    {
        for (int x = 1; x < visibility[y].Length - 1; x++)
        {
            var isVisible = false;
            int a = 0, b = 0, c = 0, d = 0;

            for (int i = y - 1; i >= 0; i--)
            {
                a++;
                if (matrix[y][x] > matrix[i][x])
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                    break;
                }
            }

            if (isVisible)
                visibility[x][y] = true;

            for (int i = y + 1; i < visibility.Length; i++)
            {
                b++;
                if (matrix[y][x] > matrix[i][x])
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                    break;
                }
            }

            if (isVisible)
                visibility[x][y] = true;

            for (int i = x - 1; i >= 0; i--)
            {
                c++;
                if (matrix[y][x] > matrix[y][i])
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                    break;
                }
            }

            if (isVisible)
                visibility[x][y] = true;

            for (int i = x + 1; i < visibility[y].Length; i++)
            {
                d++;
                if (matrix[y][x] > matrix[y][i])
                {
                    isVisible = true;
                }
                else
                {
                    isVisible = false;
                    break;
                }
            }

            if (isVisible)
                visibility[x][y] = true;

            var scScore = a * b * c * d;

            scenicScore = Math.Max(scenicScore, scScore);
        }
    }

    var result = 0;

    for (int y = 0; y < visibility.Length; y++)
    {
        for (int x = 0; x < visibility[y].Length; x++)
        {
            if (visibility[y][x])
                result++;
        }
    }

    Console.WriteLine($"Part 1 is {result}");
    Console.WriteLine($"Part 2 is {scenicScore}");
}
