Console.WriteLine(CalculateCalories("input1.txt").Max());
Console.WriteLine(CalculateCalories("input1.txt").OrderByDescending(x => x).Take(3).Sum());


List<int> CalculateCalories(string filePath)
{
	var elfList = new List<int>();
	
	int currentElfCalories = 0;
	using (var reader = new StreamReader(filePath))
	{
		while (reader.Peek() >= 0)
		{
			int calories;

			if (Int32.TryParse(reader.ReadLine(), out calories))
			{
				currentElfCalories += calories;
			}
			else
			{
                elfList.Add(currentElfCalories);
				currentElfCalories = 0;
			}
		}
        elfList.Add(currentElfCalories);
    }
    return elfList;
}