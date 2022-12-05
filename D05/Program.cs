LoadStack("input1.txt");

void LoadStack(string filePath)
{
    var stacksPart1 = new List<Stack<char>>();
    var stacksPart2 = new List<Stack<char>>();
    
    using (var reader = new StreamReader(filePath))
    {
        var stackConfig = new Stack<string>();
        var stacksCount = 0;
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();
            if (line[1] != '1')
            { 
                stackConfig.Push(line);
            }
            else
            {
                stacksCount = (int)Math.Ceiling(line.Length / 4.0);
                reader.ReadLine();
                break;
            }
        }

        for (int i = 0; i < stacksCount; i++)
        {
            stacksPart1.Add(new Stack<char>());
            stacksPart2.Add(new Stack<char>());
        }

        while(stackConfig.Count > 0)
        {
            var l = stackConfig.Pop();
            for (int i = 0, k = 1; i < stacksCount; i++, k+=4)
            {
                if (l[k] != ' ')
                {
                    stacksPart1[i].Push(l[k]);
                    stacksPart2[i].Push(l[k]);
                }
            }
        }

        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();

            var parts = line.Split(' ');

            var count = Convert.ToInt32(parts[1]);
            var from = Convert.ToInt32(parts[3]) - 1;
            var to = Convert.ToInt32(parts[5]) - 1;

            var tmpStack = new Stack<char>();

            for (int i = 0; i < count; i++)
            {
                tmpStack.Push(stacksPart2[from].Pop());
            }

            for (int i = 0; i < count; i++)
            {
                stacksPart1[to].Push(stacksPart1[from].Pop());
                stacksPart2[to].Push(tmpStack.Pop());

            }

        }

        foreach (var s in stacksPart1)
        {
            Console.Write(s.Pop());
        }

        Console.WriteLine();

        foreach (var s in stacksPart2)
        {
            Console.Write(s.Pop());
        }
    }


            //Stack<char> stack = new Stack<char>();
}