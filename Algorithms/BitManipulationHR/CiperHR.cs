using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.BitManipulationHR
{
    public class CiperHR
    {
        public void ExecuteAlgo()
        {
            int size = int.Parse(System.Console.ReadLine());
            int shift = int.Parse(System.Console.ReadLine());
            string originalBits = System.Console.ReadLine();
            this.DecipheredString("100010", 4);
        }

        private string DecipheredString(string orginalBits, int shift)
        {
            string[] shiftedBits = new string[shift];
            this.InitialiseAndShiftBits(shiftedBits, orginalBits);
            this.DisplayShiftBits(shiftedBits);
            return null;
        }

        private void InitialiseAndShiftBits(string[] shiftdBits, string originalBits)
        {
            shiftdBits[0] = originalBits;

            for (int index = 1; index < shiftdBits.Length; index++)
            {
                shiftdBits[index] = "0" + shiftdBits[index - 1];
            }
        }

        private void DisplayShiftBits(string[] shiftBits)
        {
            foreach(string item in shiftBits)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
