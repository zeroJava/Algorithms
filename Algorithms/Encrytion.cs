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
            string input = System.Console.ReadLine(); // we input the string
            string strSpaceRemved = input.Replace(" ", ""); // we start to trim of the white spaces, leaving us with just characters
            Console.WriteLine(strSpaceRemved);

            double sqrootString = Math.Sqrt(strSpaceRemved.Length); // we get the square root of refined string. the value returned will define the column and row size.
            double column = Math.Ceiling(sqrootString); // getting the value of column for 2d array
            double row = Math.Floor(sqrootString); // getting the value for row, for our 2d array
            // the value of columns has to be bigger than the value of row

            if( ( (int)(column * row) ) < strSpaceRemved.Length)
            {
                /* This safety mechanism checks if the value produced from column * row is bigger than the length of strSpaceRemved.
                * This is important, because we need to make sure that our 2d array has enough space to hold the characters from the string
                */

                //column++;
                row++; // we increement row to provide space for the remaining strings
            }

            string[,] grid = GenerateGrid(strSpaceRemved, (int)row, (int)column);
            PrintGrid(grid);

            string value = Encrpt(grid);
            Console.WriteLine(value);

            Console.ReadKey();
        }

        private string[,] GenerateGrid(string str, int row, int column)
        {
            string[,] grid = new string[row, column];
            string temp = str;

            for(int rows = 0; rows < grid.GetLength(0); rows++)
            {
                for(int columns = 0; columns < grid.GetLength(1); columns++)
                {
                    if( !(temp.Equals("")) && temp.Length > 0)
                    {
                        grid[rows, columns] = temp[0]+"";
                        temp = temp.Remove(0, 1);
                        continue;
                    }

                    goto outter;
                }
            }
            outter :

            return grid;
        }

        private string Encrpt(string[,] grid)
        {
            string value = "";

            for(int column = 0; column < grid.GetLength(1); column++)
            {
                for(int row = 0; row < grid.GetLength(0); row++)
                {
                    value = value + grid[row, column];
                }
                value += " ";
            }

            return value;
        }

        private void PrintGrid(string[,] str)
        {
            Console.WriteLine();

            for(int i = 0; i < str.GetLength(0); i++)
            {
                for(int j = 0; j < str.GetLength(1); j++)
                {
                    System.Console.Write(str[i,j] + " ");
                }
                System.Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
