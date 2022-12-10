var rope_p1 = new List<Point> { new Point(0, 0, null) };
rope_p1.Add(new Point(0, 0, rope_p1[0]));


var rope_p2 = new List<Point> { new Point(0, 0, null) };
for (int i = 0; i < 9; i++)
{
    rope_p2.Add(new Point(0, 0, rope_p2[i]));
}

Calculate("input1.txt", rope_p1);
Calculate("input1.txt", rope_p2);

void Calculate(string filePath, List<Point> rope)
{
    using (var reader = new StreamReader(filePath))
    {
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine().Split(' ');
            var operation = new { Way = line[0][0], Steps = Convert.ToUInt32(line[1]) };

            for (int i = 0; i < operation.Steps; i++)
            {
                foreach (var p in rope)
                {
                    p.Step(operation.Way);
                }
            }
        }
        Console.WriteLine(rope.Last().VisitedCount);
    }
}

class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int prevX { get; set; }
    public int prevY { get; set; }
    
    Dictionary<string, int> points = new Dictionary<string, int>();

    public Point Head { get; set; }

    public Point(int x, int y, Point head)
    {
        X = x;
        Y = y;
        prevX = x;
        prevY = y;
        Head = head;
        points.Add(this, 1);
    }

    public void Step(char way)
    {
        if (Head == null)
        {
            prevX = X;
            prevY = Y;
            switch (way)
            {
                case 'U':
                    Y++;
                    break;
                case 'D':
                    Y--;
                    break;
                case 'L':
                    X--;
                    break;
                case 'R':
                    X++;
                    break;
                default:
                    break;
            }
            if(!points.TryAdd(this, 1))
            {
                points[this]++;
            }
        }
        else
        {
            var dx = Head.X - X;
            var dy = Head.Y - Y;
            if ( Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
            {
                prevX = X;
                prevY = Y;
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    X = Head.X - Math.Sign(dx);
                    Y = Head.Y;
                }
                else if (Math.Abs(dy) > Math.Abs(dx))
                {
                    X = Head.X;
                    Y = Head.Y - Math.Sign(dy);
                }
                else
                {
                    X = Head.X - Math.Sign(dx);
                    Y = Head.Y - Math.Sign(dy);
                }
                
                if (!points.TryAdd(this, 1))
                {
                    points[this]++;
                }
            }
        }
    }

    public int VisitedCount { get => points.Count; }

    public override string ToString()
    {
        return $"{X}-{Y}";
    }

    public static implicit operator string(Point p) => p.ToString();
}