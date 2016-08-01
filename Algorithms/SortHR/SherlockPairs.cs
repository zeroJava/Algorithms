using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SortHR
{
    class SherlockPairs
    {
        [Obsolete("This method is inefficient. Please use CountPairsMath")]
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

        public long CountPairsMath(int[] array)
        {
            long _counter = 0;
            var _groups = array.GroupBy(x => x);

            foreach (var item in _groups)
            {
                if (item.Count() <= 1)
                {
                    continue;
                }

                long _size = item.Count();
                long _multiplyBy = _size - 1;
                _counter = _counter + _size * _multiplyBy;
            }

            return _counter;
        }
    }
}
