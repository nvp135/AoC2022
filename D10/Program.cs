using Extentions;

namespace D10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sol = new Solution();
            sol.Calculate("input1.txt");
            sol.Part1("input1.txt");
            
            sol.Part2("input1.txt");
            
            Console.ReadKey();
        }
    }

    internal class Solution
    {
        public void Calculate(string filePath)
        {
            var cycleOccurs = new List<int>() { 20, 60, 100, 140, 180, 220 };
            using (var reader = new StreamReader(filePath))
            {
                int signalStrengths = 0, registerValue = 1, cycleCount = 0;

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Split(' ');

                    cycleCount++;

                    if (cycleOccurs.Contains(cycleCount))
                    {
                        signalStrengths += registerValue * cycleCount;
                        Console.WriteLine($"{cycleCount} * {registerValue} = {cycleCount * registerValue}");
                    }

                    if (line.Length > 1)
                    {

                        cycleCount++;

                        if (cycleOccurs.Contains(cycleCount))
                        {
                            signalStrengths += registerValue * cycleCount;
                            Console.WriteLine($"{cycleCount} * {registerValue} = {cycleCount * registerValue}");
                        }
                        registerValue += Convert.ToInt32(line[1]);
                    }
                }
                Console.WriteLine(signalStrengths);
            }
        }

        record Command(string name, int arg = 0);
        record VmState(int x, int cycle);

        Command[] ReadInput(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var lines = reader.ReadToEnd().Split(Environment.NewLine);
                return lines
                    .Select(l => l.Split(' '))
                    .Select(x => x.Length == 1 ? new Command(x[0], 0) : new Command(x[0], int.Parse(x[1])))
                    .ToArray<Command>();
            }
        }

        IEnumerable<VmState> RunVm(Command[] commands)
        {
            var cycle = 0;
            var x = 1;
            foreach (var cmd in commands)
            {
                switch (cmd)
                {
                    case ("addx", var arg):
                        yield return new VmState(x, cycle++); 
                        yield return new VmState(x, cycle++);
                        x += arg;
                        break;
                    case ("noop", _):
                        yield return new VmState(x, cycle++);
                        break;
                    default:
                        throw new NotSupportedException($"This command ({cmd.ToString()}) doesn't support!");
                }
            }
        }

        public void Part1(string inputPath)
        {
            var result = RunVm(ReadInput(inputPath))
                .EveryNth(40, startFrom: 19)
                .Sum(st => st.Out().x * (st.cycle + 1))
                .Out("Part 1: ");
        }

        public void Part2(string inputPath)
        {
            var result = RunVm(ReadInput(inputPath))
                .GroupBy(40)
                .Select(gr => gr.Select(st => Math.Abs(st.x - (st.cycle % 40)) <= 1 ? "##" : "  ").ToList())
                .Out("Part 2: \r\n");
        }
    }
}