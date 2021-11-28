using System;
using System.Collections.Generic;

namespace Algorithms.ImplementationHR
{
	public class LarrysArray
	{
		private List<int[]> _arrayList = new List<int[]>();
		//private const string _input = "";

		public void Execute()
		{
			System.Console.WriteLine("Enter testcase");
			int testcase = int.Parse(Console.ReadLine());
			for (int cycle = 0; cycle < testcase; cycle++)
			{
				System.Console.WriteLine("Enter size\n. And enter array value");
				int size = int.Parse(Console.ReadLine());
				_arrayList.Add(Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse));
				//_arrayList.Add(Array.ConvertAll(_input.Split(' '), int.Parse));
			}
			this.ExecuteAlgo();
		}

		private void ExecuteAlgo()
		{
			foreach (int[] array in _arrayList)
				this.ScanAndRotateIndicesArray(array);
		}

		private void ScanAndRotateIndicesArray(int[] array)
		{
			bool finalChecked = false;
			int cycle = 0;
			while (cycle < array.Length - 2)
			{
				if (cycle < 0)
					cycle++;

				this.RotateIndices(cycle, array);

				if (cycle > 0)
				{
					if (array[cycle - 1] > array[cycle])
					{
						finalChecked = false;
						cycle--;
						continue;
					}
				}

				if (cycle == array.Length - 3 && !finalChecked)
				{
					if (!CheckThreeIndicesAligned(cycle, array))
					{
						finalChecked = true;
						continue;
					}
				}

				cycle++;
			}

			System.Console.WriteLine(CheckAligned(array) ? "YES" : "NO");
			DisplayArray(array);
		}

		private bool CheckThreeIndicesAligned(int startIndex, int[] array)
		{
			int endIndex = startIndex + 2;
			int limit = endIndex < array.Length ? endIndex : array.Length - 1;

			for (int scanIndex = startIndex; scanIndex < limit; scanIndex++)
			{
				if (array[scanIndex] > array[scanIndex + 1])
				{
					return false;
				}
			}

			return true;
		}

		private bool RotateIndices(int startIndex, int[] array)
		{
			int endIndex = startIndex + 2;
			int limit = endIndex < array.Length ? endIndex : array.Length - 1;

			if (endIndex < array.Length)
			{
				for (int cycle = 0; cycle < 3; cycle++)
				{
					int smallestNumber = this.SmallestNumber(startIndex, array);

					int indexOne = array[startIndex];
					int indexTwo = array[startIndex + 1];
					int indexThree = array[startIndex + 2];

					if (smallestNumber == array[startIndex + 1])
					{
						array[startIndex] = indexTwo;
						array[startIndex + 1] = indexThree;
						array[startIndex + 2] = indexOne;
					}
					else if (smallestNumber == array[startIndex + 2])
					{
						array[startIndex] = indexThree;
						array[startIndex + 1] = indexOne;
						array[startIndex + 2] = indexTwo;
					}

					if (array[startIndex] < array[startIndex + 1])
						return true;
				}
			}

			return false;
		}

		private int SmallestNumber(int startIndex, int[] array)
		{
			int smallestNumber = 0;

			if (array[startIndex] < array[startIndex + 1])
				smallestNumber = array[startIndex];
			else
				smallestNumber = array[startIndex + 1];

			if (smallestNumber > array[startIndex + 2])
				smallestNumber = array[startIndex + 2];

			return smallestNumber;
		}

		private bool CheckAligned(int[] array)
		{
			bool perfectAligned = true;

			for (int index = 0; index < array.Length - 1; index++)
			{
				if (array[index] > array[index + 1])
					return false;
			}

			return perfectAligned;
		}

		private void DisplayArray(int[] array)
		{
			foreach (int number in array)
				System.Console.Write(number + " ");
			System.Console.Write("\n");
		}

		[Obsolete("Incomplete")]
		private void AlgoExecuteMatrix()
		{
			foreach (int[] array in _arrayList)
			{
				int[,] matrix = this.CreateMatrix(array);
				this.RotateMatrix(matrix);
			}
		}

		[Obsolete("Incomplete")]
		private int[,] CreateMatrix(int[] array)
		{
			int columnSize = 3;
			int rowSize = (int)Math.Ceiling((double)(array.Length / 2));
			int[,] matrix = new int[rowSize, columnSize];

			int arrayIndex = 0;

			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				for (int column = 0; column < matrix.GetLength(1); column++)
				{
					if (arrayIndex < array.Length)
						matrix[row, column] = array[arrayIndex];
					arrayIndex++;
				}
			}

			return matrix;
		}

		[Obsolete]
		private void RotateMatrix(int[,] matrix)
		{
			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				int[] tempArray = new int[] { matrix[row, 0], matrix[row, 1], matrix[row, 3] };

				if (!CheckAligned(tempArray))
					this.RotateIndices(0, tempArray);

				matrix[row, 0] = tempArray[0];
				matrix[row, 1] = tempArray[1];
				matrix[row, 2] = tempArray[2];
			}
		}

		[Obsolete]
		private void MatrixReAlignment(int[,] matrix)
		{
			int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
		}
	}
}
