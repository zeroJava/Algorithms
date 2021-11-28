using System;

namespace Algorithms
{
	class GridsearchHRimplementation
	{
		public GridsearchHRimplementation()
		{

			// [row, column]
			string[,] str = { { "7", "2", "8", "3", "4", "5", "5", "8", "6", "4" },
										{ "6", "7", "3", "1", "1", "5", "8", "6", "1", "9" },
										{ "8", "9", "8", "8", "2", "4", "2", "6", "4", "3" },
										{ "3", "8", "3", "0", "5", "8", "9", "3", "2", "4" },
										{ "2", "2", "2", "9", "5", "0", "5", "8", "1", "3" },
										{ "5", "6", "3", "3", "8", "4", "5", "3", "7", "4" },
										{ "6", "4", "7", "3", "5", "3", "0", "2", "9", "3" },
										{ "7", "0", "5", "3", "1", "0", "6", "6", "0", "1" },
										{ "0", "8", "3", "4", "2", "8", "2", "9", "5", "6" },
										{ "4", "6", "0", "7", "9", "2", "4", "1", "3", "7" }
									 };

			string[,] fin = { { "9", "5", "0", "5" },
										{ "3", "8", "4", "5" },
										{ "3", "5", "3", "0" }
									 };

			string endResult = Result(fin);
			string re = "NO";

			for (int rowI = 0; rowI < str.GetLength(0); rowI++)
			{
				for (int columnI = 0; columnI < str.GetLength(1); columnI++)
				{
					string df = ConstrctionPatterStr(str, fin, rowI, columnI);

					if (df.Equals(endResult))
					{
						re = "YES";
						goto end;
					}
				}
				//System.Console.WriteLine("y");
			}
		end:

			if (re.Equals("YES"))
			{
				System.Console.WriteLine(re);
			}
			else
			{
				System.Console.WriteLine(re);
			}

			Console.ReadKey();
		}

		private string ConstrctionPatterStr(string[,] strMatrix, string[,] find, int pointerRow, int pointerColumn)
		{
			int constraintRow = pointerRow + find.GetLength(0);
			int constraintColumn = pointerColumn + find.GetLength(1);

			string val = "";

			if ((constraintRow > (strMatrix.GetLength(0))) || (constraintColumn > (strMatrix.GetLength(1))))
			{
				return val;
			}

			for (int row = pointerRow; row < constraintRow; row++)
			{
				for (int column = pointerColumn; column < constraintColumn; column++)
				{
					val = val + strMatrix[row, column];
				}
			}

			return val;
		}

		private string Result(string[,] str)
		{
			string value = "";

			for (int row = 0; row < str.GetLength(0); row++)
			{
				for (int column = 0; column < str.GetLength(1); column++)
				{
					value = value + str[row, column];
				}
			}

			return value;
		}
	}
}
