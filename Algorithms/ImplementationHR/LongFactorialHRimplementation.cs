using System;
using System.Numerics;

namespace Algorithms
{
	class LongFactorialHRimplementation
	{

		public LongFactorialHRimplementation()
		{
			System.Console.WriteLine("enter number");
			int number = Int32.Parse(System.Console.ReadLine());
			BusinessLogic(number);
		}

		private void BusinessLogic(int n)
		{
			BigInteger bn = 1;

			for (int i = 1; i <= n; i++)
			{
				bn = bn * i;
			}

			System.Console.WriteLine(bn);
			Console.ReadKey();
		}
	}
}
