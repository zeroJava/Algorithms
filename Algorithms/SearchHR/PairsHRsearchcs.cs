using System;

namespace Algorithms
{
	class PairsHRsearchcs
	{
		public PairsHRsearchcs()
		{
			int[] _array = { 1, 5, 3, 4, 2, 8, 10, 6 };
			int _dif = 2;
			System.Console.WriteLine(GetNumberOfPairs(_array, _dif));
			System.Console.ReadKey();
		}

		private int GetNumberOfPairs(int[] array, int differenceOf)
		{
			int _numberOfPairs = 0;

			Array.Sort(array);

			for (int index = 0; index < array.Length - 1; index++)
			{
				for (int index2 = index + 1; index2 < array.Length; index2++)
				{
					int _result = array[index2] - array[index];

					if (_result == differenceOf)
					{
						_numberOfPairs++;
					}
					else if (_result > differenceOf)
					{
						break;
					}
				}
			}

			return _numberOfPairs;
		}
	}
}
