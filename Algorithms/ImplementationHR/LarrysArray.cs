using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class LarrysArray
    {
        public void Execute()
        {
            System.Console.WriteLine("Enter testcase");
            int testcase = int.Parse(Console.ReadLine());
            for (int cycle = 0; cycle <  testcase; cycle++)
            {
                ExecuteAlgo();
            }
        }

        private void ExecuteAlgo()
        {
            System.Console.WriteLine("Enter size\n. And enter array value");
            int size = int.Parse(Console.ReadLine());
            int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            for (int cycle = 0; cycle < array.Length; cycle++)
            {
                int location;
                bool perfectSet = this.ScanThreeIndicies(cycle, out location, array);

                if (perfectSet)
                    continue;
                this.Reshuffle(location, array);
            }

            System.Console.WriteLine(CheckAligned(array) ? "YES" : "NO");
            DisplayArray(array);
        }

        private bool ScanThreeIndicies(int startIndex, out int location, int[] array)
        {
            location = 0;
            int endIndex = startIndex + 2;
            int limit = endIndex < array.Length ? endIndex : array.Length - 1;

            for (int scanIndex = startIndex; scanIndex < limit; scanIndex++)
            {
                if (array[scanIndex] > array[scanIndex + 1])
                {
                    location = scanIndex;
                    return false;
                }
            }

            return true;
        }

        private void Reshuffle(int startIndex, int[] array)
        {
            int endIndex = startIndex + 2;
            int limit = endIndex < array.Length ? endIndex : array.Length - 1;

            if (endIndex < array.Length)
            {
                for (int cycle = 0; cycle < 3; cycle++)
                {
                    int indexOne = array[startIndex];
                    int indexTwo = array[startIndex + 1];
                    int indexThree = array[startIndex + 2];

                    array[startIndex] = indexThree;
                    array[startIndex + 1] = indexOne;
                    array[startIndex + 2] = indexTwo;

                    int location;
                    if (!ScanThreeIndicies(startIndex, out location, array))
                        break;
                }
            }
        }

        private bool CheckAligned(int[] array)
        {
            bool perfectAligned = true;

            for (int index = 0; index < array.Length - 1; index++)
            {
                if (array[index] > array[index + 1])
                    return false;
            }

            return perfectAligned;
        }

        private void DisplayArray(int[] array)
        {
            foreach (int number in array)
                System.Console.Write(number + " ");
        }
    }
}
