using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    class LisaWorkbook
    {
        public LisaWorkbook()
        {
            System.Console.WriteLine("The lisa work book algorithm");
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
            int _page = 1;
            List<List<int>> _chapters = new List<List<int>>();

            for(int index = 0; index < chapter; index++)
            {
                List<int> _pages = new List<int>();

                if(max_problems_per_page < problems_per_chapter[index] && problems_per_chapter[index] % max_problems_per_page != 0)
                {
                    int pg = (int)Math.Ceiling((double)problems_per_chapter[index] / max_problems_per_page);
                    AlocateNumberToPages(ref _page, pg, _pages);
                }
                else if( (problems_per_chapter[index] / max_problems_per_page) == 0)
                {
                    AlocateNumberToPages(ref _page, 1, _pages);
                }
                else
                {
                    int pg = (int)Math.Ceiling((double)problems_per_chapter[index] / max_problems_per_page);
                    AlocateNumberToPages(ref _page, pg, _pages);
                }

                _chapters.Add(_pages);
            }

            List<List<int>> _list = new List<List<int>>();

            for(int chapterNumber = 0; chapterNumber < _chapters.Count; chapterNumber++)
            {
                int _num = 1;
                int iter = _num;
                List<int> _ti = new List<int>();

                while(_num <= problems_per_chapter[chapterNumber])
                {
                    _ti.Add(_num);

                    if(iter == max_problems_per_page)
                    {
                        _list.Add(_ti);
                        _num++;
                        iter = 1;
                        _ti = new List<int>();
                        continue;
                    }

                    _num++;
                    iter++;
                    
                }

                if(_ti.Count == 0)
                {
                    continue;
                }
                else
                {
                    _list.Add(_ti);
                }
            }

            int _specialNumber = 0;

            for(int index = 0; index < _chapters.Count; index++)
            {
                /*for(int page = 0; page < )
                {

                }*/
            }

            for(int i = 0; i < _list.Count; i++)
            {
                System.Console.WriteLine("page " + i);

                for (int j = 0; j <_list[i].Count; j++)
                {
                    System.Console.WriteLine("problem " + _list[i][j]);
                }
            }
        }

        private void AlocateNumberToPages(ref int page, int number_of_pages, List<int> list)
        {
            for(int index = 0; index < number_of_pages; index++)
            {
                list.Add(page);
                page++;
            }
        }
    }
}
