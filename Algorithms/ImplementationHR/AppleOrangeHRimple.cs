using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class AppleOrangeHRimple
    {
        public void ExecuteAlgo(int houseStart, int houseEnd, int appleTree, int orangeTree, int[] apples, int[] oranges)
        {
            System.Console.WriteLine(AppleCount(houseStart, houseEnd, appleTree, apples));

            int orangeCount = oranges.Where(x => orangeTree + x >= houseStart && orangeTree + x <= houseEnd).Count();
            System.Console.WriteLine(orangeCount);
        }

        private int AppleCount(int houseStart, int houseEnd, int appleTree, int[] apples)
        {
            int count = 0;

            foreach (int apple in apples)
            {
                int position = appleTree + apple;
                if (position >= houseStart && position <= houseEnd)
                    count++;
            }

            return count;
        }
    }
}
