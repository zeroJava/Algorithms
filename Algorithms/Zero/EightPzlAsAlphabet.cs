using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms.Zero
{
	internal sealed class EightPzlAsAlphabet
	{
		private static readonly string[,] target = new string[,]
		{
			{ "A", "B", "C", },
			{ "H", "*", "D", },
			{ "G", "F", "E", }
		};

		private static readonly Dictionary<string, Tuple<int, int>> keyValuePairs
			= new Dictionary<string, Tuple<int, int>>()
			{
				{ "A", new Tuple<int, int>(0, 0) },
				{ "B", new Tuple<int, int>(0, 1) },
				{ "C", new Tuple<int, int>(0, 2) },
				{ "H", new Tuple<int, int>(1, 0) },
				{ "*", new Tuple<int, int>(1, 1) },
				{ "D", new Tuple<int, int>(1, 2) },
				{ "G", new Tuple<int, int>(2, 0) },
				{ "F", new Tuple<int, int>(2, 1) },
				{ "E", new Tuple<int, int>(2, 2) },
			};

		public static readonly string[,] eightPzl;

		static EightPzlAsAlphabet()
		{
			eightPzl = Generate8PzRandomGrid();
		}

		private static string[,] Generate8PzRandomGrid()
		{
			var cache = new HashSet<int>();
			var grid = new string[3, 3];
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					while (true)
					{
						var rndIndex = new Random().Next(0, 9);
						if (cache.Contains(rndIndex))
						{
							continue;
						}
						var str = AStarAlgorithm.CharDict[rndIndex];
						cache.Add(rndIndex);
						grid[i, j] = str;
						break;
					}
				}
			}
			return grid;
		}

		public static void Execute()
		{
			Display8PzGrids();
			FindPaths();
		}

		private static void Display8PzGrids()
		{
			Console.Write("8 PUZZLE");
			Console.Write(new string(' ', 2));
			Console.Write("TARGET\n\n");
			for (int i = 0; i < eightPzl.GetLength(0); i++)
			{
				for (int j = 0; j < eightPzl.GetLength(1); j++)
				{
					Console.Write($"{eightPzl[i, j]} ");
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
			Stopwatch sw = Stopwatch.StartNew();
			var visits = AStar();
			sw.Stop();
			Console.WriteLine($"Time: {sw.Elapsed}");
			DisplayNode(visits.FirstOrDefault());
			Console.Write("\n");
		}

		private static bool GetEmptyPoint(string[,] pzl, out int x, out int y)
		{
			x = 0;
			y = 0;
			for (int i = 0; i < pzl.GetLength(0); i++)
			{
				for (int j = 0; j < pzl.GetLength(1); j++)
				{
					if (pzl[i, j] == AStarAlgorithm.CharDict[0])
					{
						x = i;
						y = j;
						return true;
					}
				}
			}
			return false;
		}

		private static List<State> AStar()
		{
			var opened = new Dictionary<string, State>();
			var closed = new Dictionary<string, State>();
			State first = new State
			{
				Name = CrtPzlName(eightPzl),
				G_Score = 0,
				F_Score = Heuristic(eightPzl),
				CPuzzle = eightPzl,
				Previous = null,
			};
			opened.Add(first.Name, first);
			bool finished = false;
			bool showDebug = true;
			while (!finished)
			{
				Console.WriteLine("\n" + new string('-', 40));
				if (opened.Count == 0)
				{
					finished = true;
				}
				else
				{
					State current = opened.MinBy(n => n.Value.F_Score).Value;
					if (current.Previous != null)
					{
						Console.WriteLine("\nDisplay previous state");
						Console.WriteLine("");
						DisplayDebug(current.Previous);
					}
					Console.WriteLine("\nDisplay current state");
					Console.WriteLine("");
					DisplayDebug(current);
					if (PzlMatch(current.CPuzzle, target))
					{
						finished = true;
						closed.Add(current.Name, current);
					}
					else
					{
						int x = 0;
						int y = 0;
						GetEmptyPoint(current.CPuzzle, out x, out y);
						var neighbours = GetNeighbours(current, x, y,
							current.Previous);
						Console.WriteLine("\nDisplaying neighbours");
						opened.Remove(current.Name);
						foreach (var neighbour in neighbours)
						{
							if (neighbour.Name == current.Previous?.Name)
							{
								continue;
							}
							Console.WriteLine("");
							DisplayDebug(neighbour);
							var newGScore = current.G_Score + 1;
							if (opened.ContainsKey(neighbour.Name) &&
								newGScore < opened[neighbour.Name].G_Score)
							{
								UpdateExistingNeigbour(opened, neighbour, newGScore,
									current);
							}
							else if (closed.ContainsKey(neighbour.Name) &&
								newGScore < closed[neighbour.Name].G_Score)
							{
								UpdateExistingNeigbour(closed, neighbour, newGScore,
									current);
							}
							else if (!opened.ContainsKey(neighbour.Name) &&
								!closed.ContainsKey(neighbour.Name))
							{
								UpdateNeighbour(neighbour, newGScore, current);
								opened.Add(neighbour.Name, neighbour);
							}
							if (NearlyDone(neighbour.CPuzzle) && showDebug)
							{
								Console.WriteLine("\nNearl done.");
								DisplayDebug(neighbour);
								showDebug = false;
							}
						}
					}
				}
			}
			var path = new Stack<State>();
			Console.WriteLine($"\nVisit Count {closed.Count}");
			GetPath(path, closed.Values.LastOrDefault());
			return path.ToList();
		}

		private static int Heuristic(string[,] crtpzl)
		{
			List<int> hvalues = new List<int>();
			for (int i = 0; i < crtpzl.GetLength(0); i++)
			{
				for (int j = 0; j < crtpzl.GetLength(1); j++)
				{
					var tupl = keyValuePairs[crtpzl[i, j]];
					var hval = Math.Abs(tupl.Item1 - i) + Math.Abs(tupl.Item2 - j);
					hvalues.Add(hval);
				}
			}
			return hvalues.Sum();
		}

		private static bool PzlMatch(string[,] pzl, string[,] trgt)
		{
			for (int i = 0; i < pzl.GetLength(0); i++)
			{
				for (int j = 0; j < pzl.GetLength(1); j++)
				{
					if (pzl[i, j] != trgt[i, j])
					{
						return false;
					}
				}
			}
			return true;
		}

		private static List<State> GetNeighbours(State cState, int x, int y,
			State previous)
		{
			var neighbours = new List<State>();
			var currentPzl = cState.CPuzzle;
			if (IsSafe(x - 1, y, currentPzl))
			{
				var neighbour = CreateNeighbour(cState, x, y, x - 1, y);
				neighbours.Add(neighbour);
			}
			if (IsSafe(x, y + 1, currentPzl))
			{
				var neighbour = CreateNeighbour(cState, x, y, x, y + 1);
				neighbours.Add(neighbour);
			}
			if (IsSafe(x + 1, y, currentPzl))
			{
				var neighbour = CreateNeighbour(cState, x, y, x + 1, y);
				neighbours.Add(neighbour);
			}
			if (IsSafe(x, y - 1, currentPzl))
			{
				var neighbour = CreateNeighbour(cState, x, y, x, y - 1);
				neighbours.Add(neighbour);
			}
			return neighbours;
		}

		private static bool IsSafe(int i, int j, string[,] pzl)
		{
			try
			{
				return i > -1 && i < pzl.GetLength(0) && j > -1 &&
					j < pzl.GetLength(1);
			}
			catch
			{
				return false;
			}
		}

		private static State CreateNeighbour(State pState, int x, int y, int n,
			int m)
		{
			var copy = new string[3, 3];
			Array.Copy(pState.CPuzzle, copy, copy.Length);
			var str1 = copy[x, y];
			var str2 = copy[n, m];
			copy[x, y] = str2;
			copy[n, m] = str1;
			var cpstr = CrtPzlName(copy);
			return new State
			{
				Name = cpstr,
				G_Score = int.MaxValue,
				F_Score = int.MaxValue,
				CPuzzle = copy,
				//Previous = pState,
			};
		}

		private static void UpdateExistingNeigbour(Dictionary<string, State> keyValues,
			State neighbour,
			int score,
			State current)
		{
			keyValues.Remove(neighbour.Name);
			UpdateNeighbour(neighbour, score, current);
			keyValues.Add(neighbour.Name, neighbour);
		}

		private static void UpdateNeighbour(State neighbour, int score,
			State current)
		{
			neighbour.G_Score = score;
			neighbour.F_Score = score + Heuristic(neighbour.CPuzzle);
			neighbour.Previous = current;
		}

		private static string CrtPzlName(string[,] pzl)
		{
			var builder = new StringBuilder();
			for (int i = 0; i < pzl.GetLength(0); i++)
			{
				for (int j = 0; j < pzl.GetLength(1); j++)
				{
					builder.Append($"{pzl[i, j]} ");
				}
				builder.AppendLine();
			}
			return builder.ToString();
		}

		private static void GetPath(Stack<State> states, State cState)
		{
			if (cState?.Previous != null)
			{
				GetPath(states, cState.Previous);
				states.Push(cState);
			}
			else if (cState != null)
			{
				states.Push(cState);
			}
		}

		private static void DisplayNode(State state)
		{
			if (state != null)
			{
				if (state.Previous != null)
				{
					DisplayNode(state.Previous);
					Console.WriteLine("\nPrevious:");
					DisplayDebug(state.Previous);
				}
				else
				{
					Console.WriteLine("\nCurrent:");
					DisplayDebug(state);
				}
			}
		}

		/*private static void DisplayDebug(string[,] pzl)
		{
			for (int i = 0; i < pzl.GetLength(0); i++)
			{
				for (int j = 0; j < pzl.GetLength(1); j++)
				{
					Console.Write($"{pzl[i, j]} ");
				}
				Console.Write(new string(' ', 3));
				for (int j = 0; j < eightPzl.GetLength(1); j++)
				{
					Console.Write($"{eightPzl[i, j]} ");
				}
				Console.Write(new string(' ', 3));
				for (int j = 0; j < target.GetLength(1); j++)
				{
					Console.Write($"{target[i, j]} ");
				}
				Console.WriteLine();
			}
		}*/

		private static void DisplayDebug(State state)
		{
			for (int i = 0; i < state.CPuzzle.GetLength(0); i++)
			{
				for (int j = 0; j <  state.CPuzzle.GetLength(1); j++)
				{
					Console.Write($"{ state.CPuzzle[i, j]} ");
				}
				Console.WriteLine();
			}
		}

		private static bool NearlyDone(string[,] pzl)
		{
			int count = 0;
			for (int i = 0; i < pzl.GetLength(0); i++)
			{
				for (int j = 0; j < pzl.GetLength(1); j++)
				{
					if (pzl[i, j] == target[i, j])
					{
						count++;
					}
				}
			}
			return count > 6;
		}

		private class State
		{
			public string Name { get; set; }
			public int F_Score { get; set; }
			public int G_Score { get; set; }
			public string[,] CPuzzle { get; set; }
			public State Previous { get; set; }
		}
	}
}