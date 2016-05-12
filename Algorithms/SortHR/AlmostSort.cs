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
            int[] _array = { 1, 2, 3, 4, 5, 6, 8, 9, 7, 10, 11 };
            Execute(_array);
        }

        public void Execute(int[] array)
        {
            Decision(array);
        }

        /// <summary>
        /// Yes means that approach used will be reverse one segmentation.
        /// No means that approach used will be swap
        /// </summary>
        /// <returns></returns>
        private bool Decision(int[] array)
        {
            int _beginningAnomoly = 0;
            int _endingAnology = 0;
            int _lengthOfAnomoly = 0;

            for(int index = 0; index < array.Length - 1; index++)
            {
                if(array[index] > array[index + 1])
                {
                    _beginningAnomoly = index;

                    FindAnomoly(array, ref _beginningAnomoly, ref _endingAnology, ref _lengthOfAnomoly); // We use this function to find our anomoly
                    //break;
                }
            }

            System.Console.WriteLine("The beginning, ending and size" + _beginningAnomoly + " " + _endingAnology + " " + _lengthOfAnomoly);

            return false;
        }

        private void FindAnomoly(int[] array, ref int beginning, ref int ending, ref int length)
        {
            int _temp = beginning;

            for(int index = _temp; index < array.Length - 1; index++) // Here in this loop, we are trying to find were the anomoly ends.
            {
                if(array[index] < array[index + 1]) 
                {
                    /* We know that anomoly causes problems with the array by preventing it from getting a perfect ascending order.
                     * we know that when we come to a point where it is ascending that it is most likely the place where the anomoly stops */

                    ending = index; // we assign the location where the end of the anomoly is reached
                    break;
                }
            }

            for(int index = _temp; index > 0; index--) // here we try find the true beginning
            {
                /* We know that anomoly causes problems with the array by preventing it from getting a perfect ascending order.
                 * * we know that when we come to a point where it is ascending that it is most likely the place where the anomoly stops */

                if ( !(array[index] > array[ending]) )
                {
                    break;
                }

                beginning = index;
            }

            length = (ending - beginning) + 1;
        }
    }
}
