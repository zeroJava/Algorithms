using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class LarrysArray
    {
        private List<int[]> _arrayList = new List<int[]>();
        private const string _input = "52 59 20";

        public void Execute()
        {
            System.Console.WriteLine("Enter testcase");
            int testcase = int.Parse(Console.ReadLine());
            for (int cycle = 0; cycle <  testcase; cycle++)
            {
                System.Console.WriteLine("Enter size\n. And enter array value");
                int size = int.Parse(Console.ReadLine());
                //_arrayList.Add(Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse));
                _arrayList.Add(Array.ConvertAll(_input.Split(' '), int.Parse));
            }
            this.ExecuteAlgo();
        }

        private void ExecuteAlgo()
        {
            foreach (int[] array in _arrayList)
                this.ScanAndRotateIndicesArray(array);
        }

        private void ScanAndRotateIndicesArray(int[] array)
        {
            bool finalChecked = false;
            int cycle = 0;
            while (cycle < array.Length - 2)
            {
                if (cycle < 0)
                    cycle++;

                this.RotateIndices(cycle, array);

                if (cycle > 0)
                {
                    if (array[cycle - 1] > array[cycle])
                    {
                        finalChecked = false;
                        cycle--;
                        continue;
                    }
                }

                if (cycle == array.Length - 3 && !finalChecked)
                {
                    if (!CheckThreeIndicesAligned(cycle, array))
                    {
                        finalChecked = true;
                        continue;
                    }
                }

                cycle++;
            }

            System.Console.WriteLine(CheckAligned(array) ? "YES" : "NO");
            DisplayArray(array);
        }

        private bool CheckThreeIndicesAligned(int startIndex, int[] array)
        {
            int endIndex = startIndex + 2;
            int limit = endIndex < array.Length ? endIndex : array.Length - 1;

            for (int scanIndex = startIndex; scanIndex < limit; scanIndex++)
            {
                if (array[scanIndex] > array[scanIndex + 1])
                {
                    return false;
                }
            }

            return true;
        }

        private bool RotateIndices(int startIndex, int[] array)
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

                    if (array[startIndex] < array[startIndex + 1])
                        return true;
                }
            }

            return false;
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
            System.Console.Write("\n");
        }
    }
}
