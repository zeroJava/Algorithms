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
                row++; // we increement row to provide space for the remaining characters
            }

            string[,] grid = GenerateGrid(strSpaceRemved, (int)row, (int)column); // we generate the 2d array to the split and hold the characters if the string
            PrintGrid(grid); // this prints out the characters in the grid

            string value = Encrypt(grid); // we do the encryption using the Ecrypt method
            Console.WriteLine(value);

            Console.ReadKey();
        }

        private string[,] GenerateGrid(string str, int row, int column) // this method will generate and return a grid
        {
            string[,] grid = new string[row, column]; // here we create our grid or our 2d array
            string temp = str; // a temporary string to hold our string value

            for(int rows = 0; rows < grid.GetLength(0); rows++) // we start by going through each row and and column
            {
                for(int columns = 0; columns < grid.GetLength(1); columns++)
                {
                    if( !(temp.Equals("")) && temp.Length > 0)
                    {
                        grid[rows, columns] = temp[0]+""; // here we add the character to a specific location in the grid
                        temp = temp.Remove(0, 1); // for every iteration we remove the character that's been added to the grid from the temp string
                        continue; // safety mechanism to stop us going to the goto statement
                    }
                    goto outter;
                }
            }
            outter:

            return grid; // returns the grid we created
        }

        private string Encrypt(string[,] grid)
        {
            string value = ""; // varaiable to hold ecrypted value

            for(int column = 0; column < grid.GetLength(1); column++) 
            {
                for(int row = 0; row < grid.GetLength(0); row++)
                {
                    // here we go through each columns first, and them we collect all the data from the rows within that column, and then add the result to value
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
