using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SortHR
{
    class FullCountingSort
    {
        int[] _integerArray;
        string[] _stringArray;

        public FullCountingSort()
        {
            int _size = int.Parse(System.Console.ReadLine());

            _integerArray = new int[_size];
            _stringArray = new string[_size];
            this.PopulateArray(_integerArray, _stringArray);
        }

        public void Execute()
        {
            int _smallestValue = this.LocateTheSmallestNumber(_integerArray);

        }

        private void PopulateArray(int[] integerArray, string[] stringArray)
        {
            for(int index = 0; index < integerArray.Length; index++)
            {
                string[] _inputs = System.Console.ReadLine().Split(' ');
                integerArray[index] = int.Parse(_inputs[0]);
                stringArray[index] = _inputs[1];
            }
        }

        /// <summary>
        /// Using this method (function) we can create a position and start to work from there.
        /// This will return the location of the first position (if there are multiple copies) of the smallest value. 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private int LocateTheSmallestNumber(int[] array)
        {
            int _location = 0;

            for(int index = 0; index < array.Length; index++)
            {
                if(array[index] < array[_location])
                {
                    _location = index;
                }
            }

            return _location;
        }

        private void ShiftArrayRight(int[] integerArray, string[] stringArray, int beginning, int ending)
        {

        }
    }
}
