using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SortHR
{
    class SherlockPairs
    {
        public SherlockPairs()
        {
            /*int testcase = int.Parse(Console.ReadLine());

            for(int index = 0; index < testcase; index++)
            {
                int size = int.Parse(Console.ReadLine());
                int[] array = Array.ConvertAll<string, int>(Console.ReadLine().Split(' '), int.Parse);
            }*/
        }

        private void Execute(int[] array)
        {
            if(array.Length == 1)
            {
                Console.WriteLine(array.Count());
            }

            int highest = array.Max();
            int lowest = array.Min();

            Dictionary<int, int> sumCount = new Dictionary<int, int>();
            CreateElements(sumCount, lowest, highest); // https://www.youtube.com/watch?v=TTnvXY82dtM
        }

        private void CreateElements(Dictionary<int, int> dictionary, int start, int end)
        {
            for(int index = start; index <= end; index++)
            {
                dictionary.Add(index, 0);
            }
        }

        public int CountPairs(int[] array)
        {
            int _counter = 0;

            for (int index = 0; index < array.Length; index++)
            {
                for (int index2 = 0; index2 < array.Length; index2++)
                {
                    if (index == index2)
                    {
                        continue;
                    }

                    if (array[index] == array[index2])
                    {
                        _counter++;
                        // array[index]++; This increments the value of the element stored in that particular index
                        // array[index++]; This increments the index
                    }
                }
            }

            return _counter;
        }

        public int CountPairsMath(int[] array)
        {
            var _counter = 0;
            var _groups = array.GroupBy(x => x);

            foreach (var item in _groups)
            {
                if (item.Count() <= 1)
                {
                    continue;
                }

                var _size = item.Count();
                var _multiplyBy = _size - 1;
                _counter = _counter + _size * _multiplyBy;
            }

            return _counter;
        }
    }
}
