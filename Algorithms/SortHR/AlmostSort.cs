using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SortHR
{
    class AlmostSort
    {
        public AlmostSort()
        {

        }

        public void Execute(int[] array)
        {
            int _beginningAnomoly= 0;
            int _endingAnology = 0;
            int _lengthOfAnomoly = 0;
            string _state = null; 

            bool _descion = Decision(array, ref _beginningAnomoly, ref _endingAnology, ref _lengthOfAnomoly);

            if(_descion) // == true
            {
                _state = "reverse";
                Reverse(array, _beginningAnomoly, _endingAnology);
                //Display(array);
            }
            else //if(!_descion) // == false
            {
                _state = "swap";
                Swap(array, _beginningAnomoly, _endingAnology);
                //Display(array);
            }

            bool _st = Sorted(array, _beginningAnomoly);

            if (_st) // true
            {
                System.Console.WriteLine("yes\n" + _state + " " + (_beginningAnomoly + 1) + " " + (_endingAnology));
            }
            else
            {
                System.Console.WriteLine("no");
            }
        }

        /// <summary>
        /// True means that approach used will be reverse one segmentation.
        /// False means that approach used will be swap
        /// </summary>
        /// <returns></returns>
        private bool Decision(int[] array, ref int beginningAnomoly, ref int endingAnology, ref int lengthOfAnomoly)
        {
            for(int index = 0; index < array.Length - 1; index++)
            {
                if(array[index] > array[index + 1])
                {
                    beginningAnomoly = index;

                    FindAnomoly(array, ref beginningAnomoly, ref endingAnology, ref lengthOfAnomoly); // We use this function to find our anomoly
                    break;
                }
            }

            //System.Console.WriteLine("The beginning, ending and size" + beginningAnomoly + " " + endingAnology + " " + lengthOfAnomoly);

            if (lengthOfAnomoly > 2 && SectionSortedDescending(array, beginningAnomoly, endingAnology))
            {
                //System.Console.WriteLine("The beginning, ending and size" + beginningAnomoly + " " + endingAnology + " " + lengthOfAnomoly);
                return true;
            }

            return false;
        }

        private void FindAnomoly(int[] array, ref int beginning, ref int ending, ref int length)
        {
            int _temp = beginning;
            int _value = array[beginning];

            for (int index = _temp + 1; index < array.Length; index++) // Here in this loop, we are trying to find were the anomoly ends.
            {
                if (array.Length <= 2)
                {
                    ending = index;
                    break;
                }
                else if (array.Length > 2 && _value < array[index]) 
                {
                    ending = index - 1; // we assign the location where the end of the anomoly is reached
                    break;
                }
                else if(index == array.Length - 1)
                {
                    ending = index;
                    break;
                }
            }

            length = (ending - beginning) + 1;
        }

        private void Swap(int[] array, int beginning, int ending)
        {
            int _valueOfFirst = array[beginning];
            int _valueOfLast = array[ending];

            array[beginning] = _valueOfLast;
            array[ending] = _valueOfFirst;
        }

        private void Reverse(int[] array, int beginning, int ending)
        {
            IList<int> _list = new List<int>();
            
            for(int index = beginning; index <= ending; index++)
            {
                _list.Add(array[index]);
            }

            int _listIndex = 0;

            for(int index = ending; index >= beginning; index--)
            {
                array[index] = _list[_listIndex];
                _listIndex++;
            }
        }

        private bool SectionSortedDescending(int[] array, int beginning, int ending)
        {
            for (int index = ending; index > beginning; index--)
            {
                if (array[index - 1] < array[index])
                {
                    return false;
                }
            }

            return true;
        }

        private bool Sorted(int[] array, int starting_point)
        {
            for(int index = 0; index < array.Length - 1; index++)
            {
                if(array[index] > array[index + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private void Display(int[] array)
        {
            foreach(int number in array)
            {
                System.Console.Write(number + " ");
            }
        }
    }
}
