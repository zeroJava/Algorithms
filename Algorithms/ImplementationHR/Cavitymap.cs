using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    class Cavitymap
    {
        public Cavitymap()
        {
            System.Console.WriteLine("Welcome to the cavity map algorithm");

            System.Console.WriteLine("Please enter the size of the matrix. e.g. 5 == 5 x 5");
            int _matrixSize = Convert.ToInt32(Console.ReadLine());

            string[,] _grid = new string[_matrixSize,_matrixSize];

            for (int row = 0; row < _matrixSize; row++)
            {
                System.Console.WriteLine("Please enter the data for each row. e.g. 1234");
                char[] _inputedData = Console.ReadLine().ToCharArray();

                for(int column = 0; column < _inputedData.Length; column++)
                {
                    _grid[row, column] = _inputedData[column].ToString();
                }
            }

            this.Execute(_grid);
        }

        private void Execute(string[,] grid)
        {
            for (int row = 1; row < grid.GetLength(0) - 1; row++)
            {
                for (int column = 1; column < grid.GetLength(1) - 1; column++)
                {
                    if(CellsContainX(grid, row, column))
                    {
                        continue;
                    }

                    if(this.ConditionMet(grid, row, column, grid[row,column]))
                    {
                        grid[row, column] = "X";
                    }
                }
            }

            this.Display(grid);
        }

        private bool ConditionMet(string[,] grid, int current_row, int current_column, string current)
        {
            int _top = Int32.Parse(grid[current_row - 1, current_column]);
            int _bottom = Int32.Parse(grid[current_row + 1, current_column]);
            int _right = Int32.Parse(grid[current_row, current_column + 1]);
            int _left = Int32.Parse(grid[current_row, current_column - 1]);

            int _currentNUmber = Int32.Parse(current);

            if(_top < _currentNUmber && _bottom < _currentNUmber && _left < _currentNUmber && _right < _currentNUmber)
            {
                return true;
            }

            return false;
        }

        private bool CellsContainX(string[,] grid, int current_row, int current_column)
        {
            string _top = grid[current_row - 1, current_column];
            string _bottom = grid[current_row + 1, current_column];
            string _left = grid[current_row, current_column + 1];
            string _right = grid[current_row, current_column - 1];

            if(_top.Equals("X") || _bottom.Equals("X") || _left.Equals("X") || _right.Equals("X"))
            {
                return true;
            }

            return false;
        }

        public void Display(string[,] grid)
        {
            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int column = 0; column < grid.GetLength(1); column++)
                {
                    System.Console.Write(grid[row,column]);
                }
                System.Console.WriteLine("");
            }
        }
    }
}
