using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    class Encrytion
    {
        public Encrytion()
        {
            string input = System.Console.ReadLine();
            string strSpaceRemved = input.Replace(" ", "");
            Console.WriteLine(strSpaceRemved);

            double sqrootString = Math.Sqrt(strSpaceRemved.Length);
            double column = Math.Ceiling(sqrootString);
            double row = Math.Floor(sqrootString);

            string[,] grid = GenerateGrid(strSpaceRemved, (int)row, (int)column);
            PrintGrid(grid);
            Console.ReadKey();
        }

        private string[,] GenerateGrid(string str, int row, int column)
        {
            string[,] grid = new string[row, column];
            string temp = str;

            for(int index1 = 0; index1 < grid.GetLength(0); index1++)
            {
                for(int index2 = 0; index2 < grid.GetLength(1); index2++)
                {
                    grid[index1, index2] = temp[0]+"";

                    if( !(temp.Equals("")) && temp.Length > 0)
                    {
                        temp = temp.Remove(0, 1);
                        continue;
                    }

                    goto outter;
                }
            }
            outter :

            return grid;
        }

        private void PrintGrid(string[,] str)
        {
            for(int i = 0; i < str.GetLength(0); i++)
            {
                for(int j = 0; j < str.GetLength(1); j++)
                {
                    System.Console.Write(str[i,j] + " ");
                }
                System.Console.WriteLine();
            }
        }
    }
}
