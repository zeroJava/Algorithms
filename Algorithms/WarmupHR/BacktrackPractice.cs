using System;
using System.Collections.Generic;

namespace Algorithms.WarmupHR
{
	public static class BacktrackPractice
	{
		public static void ExecuteAlgo()
		{
			string[,] grid = new string[6, 6]
			{
				{ ".", ".", ".", ".", ".", "." },
				{ ".", "O", ".", ".", ".", "." },
				{ ".", ".", ".", ".", ".", "." },
				{ ".", "X", ".", ".", ".", "." },
				{ ".", ".", ".", ".", ".", "." },
				{ ".", ".", ".", ".", ".", "." },
			};

			List<Tuple<int, int>> opponentPieces = new List<Tuple<int, int>>();
			for (int row = 0; row < 6; row++)
			{
				for (int column = 0; column < 6; column++)
				{
					if (grid[row, column] != "O")
					{
						continue;
					}
					opponentPieces.Add(Tuple.Create(row, column));
				}
			}

			for (int row = 0; row < 6; row++)
			{
				for (int column = 0; column < 6; column++)
				{
					if (grid[row, column] == "X")
					{
						SolveAlgo(grid, row, column);
					}
				}
			}
		}

		private static int SolveAlgo(string[,] grid, int xrow, int xcolumn)
		{
			ShowLocation(grid, xrow, xcolumn);
			if (!IsSafeBoundry(grid, xrow, xcolumn))
			{
				return 0;
			}
			if (grid[xrow, xcolumn] == "O")
			{
				Console.WriteLine("FOUND IT!!!");
				return 1;
			}
			else
			{
				for (int row = xrow; row >= 0; row--)
				{

				}
			}
			return 0;
		}

		private static bool IsSafeBoundry(string[,] grid, int row, int column)
		{
			bool rowLowestPointReached = row < 0;
			bool columnLowestPointReached = column < 0;

			bool rowHighestPointReached = row >= grid.GetLength(0);
			bool columnHighestPointReached = column >= grid.GetLength(1);

			if (rowLowestPointReached ||
				rowHighestPointReached ||
				columnLowestPointReached ||
				columnHighestPointReached)
			{
				return false;
			}
			return true;
		}

		private static void ShowLocation(string[,] grid, int xrow, int xcolumn)
		{
			for (int row = 0; row < 6; row++)
			{
				for (int column = 0; column < 6; column++)
				{
					if (row == xrow && column == xcolumn)
					{
						Console.Write("H");
					}
					else
					{
						Console.Write(grid[row, column]);
					}
				}
				Console.WriteLine("");
			}
			Console.WriteLine("Finished ------");
		}
	}
}