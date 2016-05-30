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
            System.Console.WriteLine("Data enetred");
            Display(_integerArray, _stringArray);

            int _index = 0;
            while(_index < _integerArray.Length - 1)
            {
                int _tempInd = 1;
                for(int numberIndex = _index + 1; numberIndex < _integerArray.Length; numberIndex++)
                {
                    if(_integerArray[_index] == _integerArray[numberIndex])
                    {
                        this.ShiftArrayRight(_integerArray, _stringArray, _index + _tempInd, numberIndex);
                        _tempInd++;
                    }
                    else if(_integerArray[_index] > _integerArray[numberIndex])
                    {
                        this.ShiftArrayRight(_integerArray, _stringArray, _index, numberIndex);
                        _tempInd = 1;
                    }
                }

                if(_tempInd > 1)
                {
                    _index = _index + _tempInd;
                    continue;
                }

                _index++;
            }

            System.Console.WriteLine("Checking shift");
            Display(_integerArray, _stringArray);
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
            int _temp = integerArray[ending];
            string _tempString = stringArray[ending];

            for(int index = ending; index > beginning; index--)
            {
                int _number1 = integerArray[index - 1];
                string _string1 = stringArray[index - 1];

                integerArray[index] = _number1;
                stringArray[index] = _string1;
            }

            integerArray[beginning] = _temp;
            stringArray[beginning] = _tempString;
        }

        public void Display(int[] integerArray, string[] stringArray)
        {
            for(int index = 0; index < integerArray.Length; index++)
            {
                System.Console.Write(integerArray[index] + " ");
            }

            System.Console.WriteLine("");

            for (int index = 0; index < stringArray.Length; index++)
            {
                System.Console.Write(stringArray[index] + " ");
            }
        }
    }
}
