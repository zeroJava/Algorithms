using System;

namespace Algorithms.SortHR
{
	public class QuickSortInPlace
	{
		public void Execute(string numbers)
		{
			int[] array = Array.ConvertAll(numbers.Split(' '), x => int.Parse(x));
			Sorting(array, 0, array.Length - 1);
		}

		public void Sorting(int[] array, int li, int hi)
		{
			if (array.Length < 2)
			{
				System.Console.WriteLine(string.Join(" ", array));
			}

			if (li < hi)
			{
				int partition = Partition(array, li, hi);
				Sorting(array, li, partition - 1);
				Sorting(array, partition + 1, hi);
			}
		}

		private int Partition(int[] array, int li, int hi)
		{
			int pivot = array[hi];
			int j = li; // represents the index of the iteration
			int i = j; // represent the position that will be swapped

			while (j < hi)
			{
				if (array[j] < pivot)
				{
					Swap(i, j, array);
					j++;
					i++;
					continue;
				}

				j++;
			}

			Swap(hi, i, array);
			System.Console.WriteLine(string.Join(" ", array));

			return i;
		}

		private void Swap(int first, int second, int[] array)
		{
			int tempNumberOne = array[first];
			int tempNumberTwo = array[second];
			array[first] = tempNumberTwo;
			array[second] = tempNumberOne;
		}
	}
}
