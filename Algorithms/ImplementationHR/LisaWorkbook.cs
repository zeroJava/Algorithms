using System;
using System.Collections.Generic;

namespace Algorithms.ImplementationHR
{
	class LisaWorkbook
	{
		public LisaWorkbook()
		{
			System.Console.WriteLine("The lisa workbook algorithm");
			System.Console.WriteLine("Please enter the number of chapters and the max problem page e.g. 5 3");
			string[] _array = System.Console.ReadLine().Split(' ');
			int[] _input = Array.ConvertAll(_array, Int32.Parse);

			System.Console.WriteLine("Enter problems per chapter e.g. 4 2 10 6 7");
			string[] _array2 = System.Console.ReadLine().Split(' ');
			int[] _input2 = Array.ConvertAll(_array2, Int32.Parse);

			Execute(_input2.Length, _input[1], _input2);
			System.Console.ReadKey();
		}

		private void Execute(int chapter, int max_problems_per_page, int[] problems_per_chapter)
		{
			List<List<int>> _pagesWithProblemsAssigned = new List<List<int>>();
			this.AddingProblemsToEachPage(_pagesWithProblemsAssigned, max_problems_per_page, problems_per_chapter);

			System.Console.WriteLine(this.NumberOfSpecialNumber(_pagesWithProblemsAssigned));
		}

		private void AddingProblemsToEachPage(List<List<int>> pagesWithProblems, int max_problems_per_page, int[] problems_per_chapter)
		{
			for (int chapterNumber = 0; chapterNumber < problems_per_chapter.Length; chapterNumber++)
			{
				int _num = 1;
				int _iteration = _num;
				List<int> _problems = new List<int>();

				while (_num <= problems_per_chapter[chapterNumber])
				{
					_problems.Add(_num);

					if (_iteration == max_problems_per_page)
					{
						pagesWithProblems.Add(_problems);
						_num++;
						_iteration = 1;
						_problems = new List<int>();
						continue;
					}

					_num++;
					_iteration++;
				}

				if (_problems.Count == 0)
				{
					continue;
				}
				else
				{
					pagesWithProblems.Add(_problems);
				}
			}
		}

		private int NumberOfSpecialNumber(List<List<int>> pagesWithProblems)
		{
			int _noOfSpecialNumbers = 0;

			int i = 0;
			while (i < pagesWithProblems.Count)
			{
				int _page = i + 1;
				if (pagesWithProblems[i].Contains(_page))
				{
					_noOfSpecialNumbers++;
				}

				i++;
			}

			return _noOfSpecialNumbers;
		}
	}
}
