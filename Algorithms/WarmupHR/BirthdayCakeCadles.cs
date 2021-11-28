using System;
using System.Linq;

namespace Algorithms.WarmupHR
{
	public class BirthdayCakeCadles
	{
		public static void Execute()
		{
			Console.WriteLine("Enter number of testcase");
			int testCase = int.Parse(Console.ReadLine());

			for (int cycle = 0; cycle < testCase; cycle++)
			{
				Console.WriteLine("Enter candle height. Format (2 5 2 6)");
				int[] array = Array.ConvertAll(
						  Console.ReadLine().Split(' '), Int32.Parse);
				Console.WriteLine(NumberHigestBirthdayCandles(array));
			}
		}

		public static int NumberHigestBirthdayCandles(int[] array)
		{
			switch (array.Length)
			{
				case 0:
					return 0;
				case 1:
					return 1;
			}

			int highestCandlle = array.Max();
			return array.Where(c => c == highestCandlle).Count();
		}
	}
}
