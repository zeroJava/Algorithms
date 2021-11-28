using System;

namespace Algorithms.ImplementationHR
{
	public class SherlockandSquare
	{
		private int _testcase;

		public SherlockandSquare()
		{
			System.Console.WriteLine("Enter testcase");
			_testcase = Int32.Parse(System.Console.ReadLine());
		}

		public void ExcuteLogic()
		{
			for (int te = 0; te < _testcase; te++)
			{
				int numA = int.Parse(Console.ReadLine());
				int numB = int.Parse(Console.ReadLine());
				System.Console.WriteLine();
			}
		}

		public int Number(int numA, int numB)
		{
			int number = (int)Math.Floor((Math.Sqrt(numB)) + 1) - (int)Math.Ceiling((Math.Sqrt(numA)));
			return number;
		}
	}
}
