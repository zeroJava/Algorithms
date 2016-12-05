using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class EmaSupercomputer
    {
        public EmaSupercomputer()
        {
            //
        }

        public void Execute()
        {
            int row = int.Parse(System.Console.ReadLine());
            int column = int.Parse(System.Console.ReadLine());

            int[,] matrix = new int[1, 2];
        }

        private void IntialiseMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                IntialiseColumn(matrix, row);
            }
        }

        private void IntialiseColumn(int[,] matrix, int row)
        {
            string inputValue = System.Console.ReadLine();
            for (int column = 0; column < matr)
            {

            }
        }
    }
}
