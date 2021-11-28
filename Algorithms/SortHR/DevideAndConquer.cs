using System.Collections.Generic;
using System.Linq;

namespace Algorithms.SortHR
{
	class DevideAndConquer
	{
		public DevideAndConquer()
		{
			int[] _array = { 9, 8, 6, 7, 3, 5, 9, 4, 1, 2 };
			_array = QuickSort(_array);
		}

		private void Partition(int[] array)
		{
			int _pivot = array[0]; // here we create the pivot, which helps play the part of central point. Using this, we know which direction to place the elements in. 

			IList<int> _left = new List<int>(); // we create a dynamic collection to add data.
			IList<int> _equal = new List<int>();
			IList<int> _right = new List<int>();

			for (int index = 0; index < array.Length; index++)
			{
				int _currentNumber = array[index];

				// In this loop, we are going throught the array and partitioning it, and placing each element either to the right or left of the pivot.

				// Here place each element in the correct section.
				if (_currentNumber < _pivot)
				{
					_left.Add(array[index]);
				}
				else if (_currentNumber == _pivot)
				{
					_equal.Add(array[index]);
				}
				else if (_currentNumber > _pivot)
				{
					_right.Add(array[index]);
				}
			}

			int[] _newArray = _left.Concat(_equal).Concat(_right).ToArray(); // we merge the three seperate list into a new array.
			System.Console.WriteLine(string.Join(" ", _newArray));
		}

		public int[] QuickSort(int[] array)
		{
			if (array.Length <= 1) // We check if array.length is less than 1, because if it is, there would no point in sorting, because it is already sorted.
			{
				return array; // If condition is met, then we leave this function, as there is no point of continueing.
			}

			int _pivot = array[0]; // here we create the pivot, which helps play the part of central point. Using this, we know which direction to place the elements in. 

			IList<int> _left = new List<int>(); // we create a dynamic collection to add data.
			IList<int> _equal = new List<int>();
			IList<int> _right = new List<int>();

			for (int index = 0; index < array.Length; index++)
			{
				int _currentNumber = array[index];

				// In this loop, we are going throught the array and partitioning it, and placing each element either to the right or left of the pivot.

				// Here place each element in the correct section.
				if (_currentNumber < _pivot)
				{
					_left.Add(array[index]);
				}
				else if (_currentNumber == _pivot)
				{
					_equal.Add(array[index]);
				}
				else if (_currentNumber > _pivot)
				{
					_right.Add(array[index]);
				}
			}

			int[] _leftPart = QuickSort(_left.ToArray()); // We recurse through Quicksort method, until the array has partitioned and sorted.         
			int[] _equalPart = _equal.ToArray();
			int[] _rightPart = QuickSort(_right.ToArray());

			int[] _newArray = _leftPart.Concat(_equal).Concat(_rightPart).ToArray(); // We create a new array.
			System.Console.WriteLine(string.Join(" ", _newArray));
			return _newArray; // return the value of the new array.
		}
	}
}
