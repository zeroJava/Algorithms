using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.SortHR
{
	public class CountingSort1
	{
		public CountingSort1()
		{
			//
		}

		public void Execute(string numberLine)
		{
			string[] numbersStringArray = numberLine.Split(' ');
			List<int> numbers = Array.ConvertAll<string, int>(numbersStringArray, int.Parse).ToList();
			int[] results = new int[100];
			CountingSortOneAlgorithm(numbers, results);
		}

		private void CountingSortOneAlgorithm(List<int> list, int[] array)
		{
			for (int index = 0; index < array.Length; index++)
			{
				array[index] = list.FindAll(x => x == index).Count();
				System.Console.Write(array[index] + " ");
			}
		}
	}
}
