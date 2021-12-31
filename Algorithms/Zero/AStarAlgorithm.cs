using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Zero
{
	internal sealed class AStarAlgorithm
	{
		private static readonly string[,] target = new string[,]
		{
			{ "A", "B", "C", "D", "E", },
			{ "J", "I", "H", "G", "F", },
			{ "K", "L", "M", "N", "O", },
			{ "T", "S", "R", "Q", "P", },
			{ "U", "V", "W", "X", "Y", },
			{ "Z", "1", "2", "3", "4", },
		};

		public static readonly Dictionary<int, string> CharDict = new Dictionary<int, string>()
		{
			{ 0, "*" },
			{ 1, "A" },
			{ 2, "B" },
			{ 3, "C" },
			{ 4, "D" },
			{ 5, "E" },
			{ 6, "F" },
			{ 7, "G" },
			{ 8, "H" },
			{ 9, "I" },
			{ 10, "J" },
			{ 11, "K" },
			{ 12, "L" },
			{ 13, "M" },
			{ 14, "N" },
			{ 15, "O" },
			{ 16, "P" },
			{ 17, "Q" },
			{ 18, "R" },
			{ 19, "S" },
			{ 20, "T" },
			{ 21, "U" },
			{ 22, "V" },
			{ 23, "W" },
			{ 24, "X" },
			{ 25, "Y" },
			{ 26, "Z" },
			{ 27, "1" },
			{ 28, "2" },
			{ 29, "3" },
			{ 30, "4" },
		};

		private static readonly string[,] aSortedGrid;

		static AStarAlgorithm()
		{
			aSortedGrid = GenerateRandomGrid();
		}

		private static string[,] GenerateRandomGrid()
		{
			var cache = new HashSet<int>();
			var grid = new string[6, 5];
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					while (true)
					{
						var rndIndex = new Random().Next(1, 31);
						if (cache.Contains(rndIndex))
						{
							continue;
						}
						var str = CharDict[rndIndex];
						cache.Add(rndIndex);
						grid[i, j] = str;
						break;
					}
				}
			}
			return grid;
		}

		#region Standard A* Demo
		public static void Execute()
		{
			DisplayGrids();
			FindPaths();
		}

		private static void DisplayGrids()
		{
			Console.Write("START");
			Console.Write(new string(' ', 8));
			Console.Write("TARGET\n\n");
			for (int i = 0; i < aSortedGrid.GetLength(0); i++)
			{
				for (int j = 0; j < aSortedGrid.GetLength(1); j++)
				{
					Console.Write($"{aSortedGrid[i, j]} ");
				}
				Console.Write(new string(' ', 3));
				for (int j = 0; j < target.GetLength(1); j++)
				{
					Console.Write($"{target[i, j]} ");
				}
				Console.WriteLine();
			}
		}

		private static void FindPaths()
		{
			//Dictionary<string, List<Node>> visitCache = new Dictionary<string, List<Node>>();
			for (int i = 0; i < aSortedGrid.GetLength(0); i++)
			{
				for (int j = 0; j < aSortedGrid.GetLength(1); j++)
				{
					int x;
					int y;
					if (!GetTargetPoint(aSortedGrid[i, j], out x, out y))
					{
						throw new ApplicationException("Could not find target");
					}
					var visits = GetMoves(i, j, x, y);
					Console.Write("\n" + visits.Count + ": ");
					DisplayNode(visits.FirstOrDefault());
					Console.Write("\n");
				}
			}
		}

		private static bool GetTargetPoint(string c, out int x, out int y)
		{
			x = 0;
			y = 0;
			for (int i = 0; i < target.GetLength(0); i++)
			{
				for (int j = 0; j < target.GetLength(1); j++)
				{
					if (target[i, j] == c)
					{
						x = i;
						y = j;
						return true;
					}
				}
			}
			return false;
		}

		private static void DisplayNode(Node n)
		{
			if (n != null)
			{
				if (n.Previous != null)
				{
					DisplayNode(n.Previous);
					Console.Write(n.Name + ", ");
				}
				else
				{
					Console.Write(n.Name + ", ");
				}
			}
		}

		private static List<Node> GetMoves(int cx, int cy, int tx, int ty)
		{
			var visited = new Dictionary<string, Node>();
			var unVisited = new Dictionary<string, Node>();
			for (int r = 0; r < aSortedGrid.GetLength(0); r++)
			{
				for (int c = 0; c < aSortedGrid.GetLength(1); c++)
				{
					var n = new Node
					{
						Name = aSortedGrid[r, c],
						F_Score = int.MaxValue,
						G_Score = int.MaxValue,
						Row = r,
						Column = c,
						Previous = null,
					};
					unVisited.Add(aSortedGrid[r, c], n);
				}
			}
			Node start = unVisited[aSortedGrid[cx, cy]];
			Node taregt = unVisited[aSortedGrid[tx, ty]];
			start.G_Score = 0;
			start.F_Score = Heuristic(cx, cy, tx, ty);
			bool finished = false;
			while (!finished)
			{
				if (unVisited.Count == 0)
				{
					finished = true;
				}
				else
				{
					Node current = unVisited.MinBy(n => n.Value.F_Score).Value;
					if (current.Name == taregt.Name)
					{
						finished = true;
						visited.Add(current.Name, current);
					}
					else
					{
						List<string> neighbours = GetNeighbours(current.Row,
							current.Column);
						foreach (var neighbour in neighbours)
						{
							if (!visited.ContainsKey(neighbour))
							{
								var newGScore = current.G_Score + 1; // + distance from neighbour
								if (newGScore < unVisited[neighbour].G_Score)
								{
									unVisited[neighbour].G_Score = newGScore;
									unVisited[neighbour].F_Score = newGScore +
										Heuristic(unVisited[neighbour].Row,
										unVisited[neighbour].Column,
										tx,
										ty);
									unVisited[neighbour].Previous = current;
								}
							}
						}
						visited.Add(current.Name, current);
						unVisited.Remove(current.Name);
					}
				}
			}
			var path = new Stack<Node>();
			GetPath(path, visited.Values.LastOrDefault());
			return path.ToList();
		}

		private static int Heuristic(int x1, int y1, int x2, int y2)
		{
			return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
			//return Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
		}

		private static List<string> GetNeighbours(int x, int y)
		{
			var neighbours = new List<string>();
			if (IsSafe(x - 1, y))
			{
				neighbours.Add(aSortedGrid[x - 1, y]);
			}
			if (IsSafe(x, y + 1))
			{
				neighbours.Add(aSortedGrid[x, y + 1]);
			}
			if (IsSafe(x + 1, y))
			{
				neighbours.Add(aSortedGrid[x + 1, y]);
			}
			if (IsSafe(x, y - 1))
			{
				neighbours.Add(aSortedGrid[x, y - 1]);
			}
			return neighbours;
		}

		private static bool IsSafe(int x, int y)
		{
			try
			{
				return x > -1 && x < aSortedGrid.GetLength(0) && y > -1 &&
					y < aSortedGrid.GetLength(1);
			}
			catch
			{
				return false;
			}
		}

		private static void GetPath(Stack<Node> nodes, Node cNode)
		{
			if (cNode?.Previous != null)
			{
				GetPath(nodes, cNode.Previous);
				nodes.Push(cNode);
			}
			else if (cNode != null)
			{
				nodes.Push(cNode);
			}
		}

		private class Node
		{
			public string Name { get; set; }
			public int F_Score { get; set; }
			public int G_Score { get; set; }
			public int Row { get; set; }
			public int Column { get; set; }
			public Node Previous { get; set; }
		}
		#endregion
	}
}