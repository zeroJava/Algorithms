using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    class CutTheSticks
    {
        public CutTheSticks()
        {

            System.Console.WriteLine("Enter you array in the for of a long string");
            // Getting our array values in the form of a whole string and then spliting it into seperate elements
            string[] strArray = System.Console.ReadLine().Split(' ');
            // Converting the string elements into integer elements
            int[] array = Array.ConvertAll(strArray, Int32.Parse);
            // executing our logic
            Execute(array);
        }

        private void Execute(int[] array)
        {
            int[] temp = array; // a temporary array to copy the value of the original array
            bool state = true; // to start of the logic

            while(state) // all business logic will happen within thi loop
            {
                int nos = NumberOfSticks(temp); // using the method, we can get the current number of elements, which are postive and big than zeros
                if(nos <= 0 )
                {
                    break; // we'll stop our logic here, when the conditions is met
                }
                System.Console.WriteLine(nos); // display our reults
                int shortestStick = FindShortestStick(temp); // using this method we'll find the smallest element that is more that 0;
                SubtractElements(temp, shortestStick); // using this method, we'll start to subsract all the elements in the array with the smallest element from the array.
            }
            state = false;
        }

        private void SubtractElements(int[] array, int number)
        {
            for(int index = 0; index < array.Length; index++)
            {
                if(array[index] > 0) // we don't need to substract everything, just those more than 0;
                {
                    array[index] = array[index] - number; // doing the calculation here
                }
            }
        }

        private int FindShortestStick(int[] array)
        {
            return array.Where(x => x > 0).Min(); // using the lambda equation, we can get the smallest element from the array that is more that 0;
        }

        private int NumberOfSticks(int[] array)
        {
            return array.Where(x => x > 0).Count(); // return the total ammount of element in the array that are more than 0;
        }
    }
}
