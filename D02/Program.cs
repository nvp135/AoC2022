var rps_p1 = new int[][] { 
	new int[3] {4, 8, 3}, 
	new int[3] {1, 5, 9}, 
	new int[3] {7, 2, 6}, 
};

var rps_p2 = new int[][] {
    new int[3] {3, 4, 8},
    new int[3] {1, 5, 9},
    new int[3] {2, 6, 7},
};

CalculatePoints("input1.txt");

void CalculatePoints(string filePath)
{
	int res_p1 = 0;
	int res_p2 = 0;
	using (var reader = new StreamReader(filePath))
	{
		while (reader.Peek() >= 0)
		{
			var line = reader.ReadLine();

			res_p1 += rps_p1[line[0] - 'A'][line[2] - 'X'];
			res_p2 += rps_p2[line[0] - 'A'][line[2] - 'X'];
		}
    }
	Console.WriteLine($"Part 1 is {res_p1}");
	Console.WriteLine($"Part 2 is {res_p2}");
}