using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Zero
{
	internal class EightPuzzleAStar
	{
		private readonly int[,] tiles;
		private readonly int[,] goal;

		public EightPuzzleAStar(int[,] tiles, int[,] goal)
		{
			this.tiles = tiles;
			this.goal = goal;
		}

		public void Solve()
		{
			//
		}

		public int GetNumberOfMoves()
		{
			return 0;
		}

		public static void Execute()
		{
			int[,] GOAL = new int[,]
			{
				{ 1, 2, 3, },
				{ 4, 5, 6, },
				{ 7, 8, 0, },
			};
		}
	}

	internal class Board
	{
		private readonly int[,] tiles;
		private readonly int[,] goal;

		public int[,] Tiles
		{
			get
			{
				return tiles;
			}
		}

		public int G_Score { get; set; }
		public int F_Score { get; set; }
		public Board Previous { get; set; }

		/// <summary>
		/// Gets size of the gri
		/// </summary>
		/// <returns></returns>
		public int Size
		{
			get
			{
				return tiles.GetLength(0) * tiles.GetLength(1);
			}
		}

		public Board(int[,] tiles, int[,] goal)
		{
			this.tiles = tiles;
			this.goal = goal;
		}

		/// <summary>
		/// Return tile at row i, column j (or 0 if blank)
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public int TileAt(int row, int column)
		{
			return tiles[row, column];
		}

		/// <summary>
		/// Number of tiles out of place
		/// </summary>
		/// <returns></returns>
		public int Hamming()
		{
			return 0;
		}

		/// <summary>
		/// Sum of Manhattan distances between tiles and goal
		/// </summary>
		/// <returns></returns>
		public int Manhattan()
		{
			List<int> hvalues = new List<int>();
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					int row;
					int column;
					GetTileCoOrdinates(tiles[i, j], out row, out column);
					var hval = Math.Abs(row - i) + Math.Abs(column - j);
					hvalues.Add(hval);
				}
			}
			return hvalues.Sum();
		}

		private bool GetTileCoOrdinates(int tile, out int row, out int column)
		{
			row = 0;
			column = 0;
			for (int i = 0; i < goal.GetLength(0); i++)
			{
				for (int j = 0; j < goal.GetLength(1); j++)
				{
					if (tile == goal[i, j])
					{
						row = i;
						column = j;
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Is this board the goal board?
		/// </summary>
		/// <returns></returns>
		public bool IsGoal()
		{
			for (int i = 0; i < goal.GetLength(0); i++)
			{
				for (int j = 0; j < goal.GetLength(1); j++)
				{
					if (tiles[i, j] != goal[i, j])
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Is this board solvable?
		/// // explaination https://math.stackexchange.com/questions/293527/how-to-check-if-a-8-puzzle-is-solvable
		/// </summary>
		/// <returns></returns>
		public bool IsSolvable()
		{
			List<int> linearList = new List<int>();
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					if (tiles[i, j] == 0)
					{
						continue;
					}
					linearList.Add(tiles[i, j]);
				}
			}
			int invcount = 0;
			for (int i = 0; i < linearList.Count; i++)
			{
				for (int j = i + 1; j < linearList.Count; j++)
				{
					if (linearList[j] > linearList[i])
					{
						invcount++;
					}
				}
			}
			return (invcount % 2 == 0);
		}

		/// <summary>
		/// All neighboring boards
		/// </summary>
		/// <returns></returns>
		public List<Board> Neighbours()
		{
			int row;
			int column;
			GetEmptyPoint(out row, out column);
			List<Board> neighbours = new List<Board>();
			if (IsSafe(row - 1, column))
			{
				neighbours.Add(CreateNeighbour(row, column, row - 1, column));
			}
			if (IsSafe(row, column + 1))
			{
				neighbours.Add(CreateNeighbour(row, column, row, column + 1));
			}
			if (IsSafe(row + 1, column))
			{
				neighbours.Add(CreateNeighbour(row, column, row + 1, column));
			}
			if (IsSafe(row, column - 1))
			{
				neighbours.Add(CreateNeighbour(row, column, row, column - 1));
			}
			return neighbours;
		}

		private bool GetEmptyPoint(out int row, out int column)
		{
			row = 0;
			column = 0;
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					if (tiles[i, j] == 0)
					{
						row = i;
						column = j;
						return true;
					}
				}
			}
			return false;
		}

		private bool IsSafe(int row, int column)
		{
			try
			{
				return row > -1 && row < tiles.GetLength(0) &&
					column > -1 && column < tiles.GetLength(1);
			}
			catch
			{
				return false;
			}
		}

		private Board CreateNeighbour(int rw1, int clm1, int rw2,
			int clm2)
		{
			var copy = new int[tiles.GetLength(0), tiles.GetLength(1)];
			Array.Copy(tiles, copy, copy.Length);
			var obj1 = copy[rw1, clm1];
			var obj2 = copy[rw2, clm2];
			copy[rw1, clm1] = obj2;
			copy[rw2, clm2] = obj1;
			return new Board(copy, goal)
			{
				G_Score = int.MaxValue,
				F_Score = int.MaxValue,
			};
		}

		public override bool Equals(object obj)
		{
			Board board = obj as Board;
			if (board == null)
			{
				return false;
			}
			if (tiles == null && board.tiles == null)
			{
				return true;
			}
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					if (tiles[i, j] == board.tiles[i, j])
					{
						return false;
					}
				}
			}
			return true;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < tiles.GetLength(0); i++)
			{
				for (int j = 0; j < tiles.GetLength(1); j++)
				{
					builder.Append($"{ tiles[i, j] } ");
				}
				builder.AppendLine();
			}
			return builder.ToString();
		}
	}
}

// https://www.cs.princeton.edu/courses/archive/fall15/cos226/assignments/8puzzle.html
// https://github.com/Jeet0204/8-Puzzle-using-AStar-Algorithm
// https://sandipanweb.wordpress.com/2017/03/16/using-uninformed-informed-search-algorithms-to-solve-8-puzzle-n-puzzle/