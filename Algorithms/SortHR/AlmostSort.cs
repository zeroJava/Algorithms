using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SortHR
{
    class AlmostSort
    {
        public void Execute(int[] array)
        {
            int _beginningAnomoly = 0;
            int _endingAnomoly = 0;
            int _lengthOfAnomoly = 0;

            string _state = null;

            int _tempEndAnomoly = 0;

            bool _descion = Decision(array, ref _beginningAnomoly, ref _endingAnomoly, ref _lengthOfAnomoly);

            if (_descion) // == true
            {
                _state = "reverse";
                Reverse(array, _beginningAnomoly, _endingAnomoly);
                _tempEndAnomoly = _endingAnomoly + 1;
            }
            else //if(!_descion) // == false
            {
                _state = "swap";
                Swap(array, _beginningAnomoly, _endingAnomoly);
                _tempEndAnomoly = _endingAnomoly + 1;
            }

            bool _sorted = Sorted(array, _beginningAnomoly);

            if (_sorted) // true
            {
                System.Console.WriteLine("yes\n" + _state + " " + (_beginningAnomoly + 1) + " " + (_tempEndAnomoly));
            }
            else
            {
                System.Console.WriteLine("no");
            }
        }

        /// <summary>
        /// Using this method we could gather data, and then make a calucalated decision on the type of sort the algorithm should use.
        /// The decision is either a swap or reverse
        /// </summary>
        /// <param name="array">The array which are deciding to sort</param>
        /// <param name="beginningAnomoly">This parameter will return the location of the beginning of the anomoly. </param>
        /// <param name="endingAnology">This paramater will return the location of the end of the anomoly</param>
        /// <param name="lengthOfAnomoly">This will return the size of the anomolgy</param>
        /// <returns>The method will bool which will determine that path the algorithm should take. 
        /// True = reverse.
        /// false = swap.
        /// </returns>
        private bool Decision(int[] array, ref int beginningAnomoly, ref int endingAnology, ref int lengthOfAnomoly)
        {
            for (int index = 0; index < array.Length - 1; index++) // Here we scan through the array for potential anomolies.
            {
                if (array[index] > array[index + 1]) // When a find something in the array that does not seem in place, we mark it as an anomoly
                {
                    beginningAnomoly = index; // We decide that the current position is the beginning of the anomolgy.

                    FindAnomoly(array, ref beginningAnomoly, ref endingAnology, ref lengthOfAnomoly); 
                    // Using the FindAnomoly and the data we have we look for the end and size of the anomoly 
                    break;
                }
            }

            // Here the algorithm make the decision on which path to take.

            if (lengthOfAnomoly > 2 && SectionSortedDescending(array, beginningAnomoly, endingAnology)) // We check here if we should reverse the anomoly to correct the pattern
            {
                // Using the SectionSortedDescending() method, we see if the anomoly has an order in a descending fashion.
                // By checking this, we know that it is not fragmented, and block that needs to placed in the right order, by reversing 
                return true;
            }

            return false;
        }

        /// <summary>
        /// FindAnomoly looks to for where the anomoloy ends, and the size of the anomoly, by getting the location beginning and searching for the end.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="beginning"></param>
        /// <param name="ending"></param>
        /// <param name="length"></param>
        private void FindAnomoly(int[] array, ref int beginning, ref int ending, ref int length)
        {
            int _temp = beginning; // Here we establish the beginning of the anomoly
            int _value = array[beginning]; // We value of the beginning-anomoly, which we will use to find our end

            for (int index = _temp + 1; index < array.Length; index++) // Here in this loop, we are trying to find were the anomoly ends.
            {
                if (array.Length <= 2) 
                {
                    // this has been placed here to deal array that array very small, know that, at this size, by default the end in will be the last element.
                    ending = index;
                    break;
                }
                else if (array.Length > 2 && _value < array[index])  
                {
                    // We move through the array looking for correct location where the benginning of the anomoly should actually be placed.
                    // By doing this, we figure out where the anomoly is located. 
                    ending = index - 1; // we assign the location where the end of the anomoly is reached
                    break;
                }
                else if (index == array.Length - 1)
                {
                    ending = index;
                    break;
                }
            }

            length = (ending - beginning) + 1; // After aquiring both begiining and end, we calculate the number of elements we are dealing with, thus getting the size.s
        }

        private void Swap(int[] array, int beginning, int ending)
        {
            int _valueOfFirst = array[beginning];
            int _valueOfLast = array[ending];

            array[beginning] = _valueOfLast;
            array[ending] = _valueOfFirst;
            int[] temp = array;
        }

        private void Reverse(int[] array, int beginning, int ending)
        {
            IList<int> _list = new List<int>();

            for (int index = beginning; index <= ending; index++)
            {
                _list.Add(array[index]);
            }

            int _listIndex = 0;

            for (int index = ending; index >= beginning; index--)
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
            for (int index = 0; index < array.Length - 1; index++)
            {
                if (array[index] > array[index + 1])
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
