using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class PairsHRsearchcs
    {
        public PairsHRsearchcs()
        {
            int[] array = { 1, 5, 3, 4, 2, 8, 10, 6 };
            int dif = 2;
            System.Console.WriteLine(GetNotpait(array, dif));
            System.Console.ReadKey();
        }

        private int GetNotpait(int[] array, int differenceOf)
        {
            int number = 0;

            for(int index1 = 0; index1 < array.Length - 1; index1++)
            {
                for(int index2 = index1 + 1; index2 < array.Length; index2++)
                {
                    int checkCa = array[index1] - array[index2];
                    int ch2 = array[index2] - array[index1];

                    if (checkCa == differenceOf || ch2 == differenceOf)
                    {
                        number++;
                    }
                }
            }
            return number;
        }
    }
}
