Calculate(ReadLines("input1.txt"));

List<string> ReadLines(string filePath)
{
    var lines = new List<string>();
    using (var reader = new StreamReader(filePath))
    {
        while (reader.Peek() >= 0)
        {
            lines.Add(reader.ReadLine());
        }
    }
    return lines;
}

void Calculate(List<string> lines)
{
    var currentPath = "";
    var fileList = new Dictionary<string, int>();
    var dirList = new Dictionary<string, int>();

    foreach (var line in lines)
    {
        var parts = line.Split(' ');

        if (parts[0] == "$")
        {
            if (parts[1] == "cd")
            {
                if (parts[2] == "..")
                {
                    currentPath = currentPath.Substring(0, currentPath.LastIndexOf('/'));
                }
                else
                {
                    currentPath = $"{currentPath}/{parts[2]}";
                }
            }
        }
        else
        {
            if (parts[0] != "dir")
            {
                var filePath = $"{currentPath}/{parts[1]}";
                var size = Convert.ToInt32(parts[0]);
                if (!fileList.ContainsKey(filePath))
                {
                    fileList.Add(filePath, size);
                }

                var dirPath = currentPath.Substring(0);

                while (dirPath != "/")
                {
                    if (dirList.ContainsKey(dirPath))
                    {
                        dirList[dirPath] += size;
                    }
                    else
                    {
                        dirList.Add(dirPath, size);
                    }
                    dirPath = dirPath.Substring(0, dirPath.LastIndexOf('/'));
                }
            }
        }
    }

    long result = 0;

    foreach (var dir in dirList)
    {
        if (dir.Value <= 100000)
        {
            result += dir.Value;
        }
    }

    Console.WriteLine($"Result for Part 1 is {result}");

    var sizes = dirList.Select(x => x.Value).OrderByDescending(x => x).ToArray();

    for (int i = 1; i < sizes.Count(); i++)
    {
        if ((70_000_000 - sizes[0] + sizes[i]) < 30_000_000)
        {
            Console.WriteLine($"Result for Part 2 is {sizes[i - 1]}");
            break;
        }
    }
}