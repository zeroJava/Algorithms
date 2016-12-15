using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class EmaSuper
    {
        public EmaSuper()
        {
            //
        }

        public void Execute3()
        {
            int row = int.Parse(System.Console.ReadLine());
            int column = int.Parse(System.Console.ReadLine());

            string[,] matrix = new string[row, column];
            IntialiseMatrix(matrix);
            IList<int> recordOfResults = new List<int>();
            BigSize(matrix, recordOfResults);
            System.Console.WriteLine(recordOfResults.Max());
        }

        private void BigSize(string[,] matrix, IList<int> list)
        {
            for (int size = 0; size < 8; size++)
            {
                for (int row = 1; row < matrix.GetLength(0) - 1; row++)
                {
                    for (int column = 1; column < matrix.GetLength(1) - 1; column++)
                    {
                        string[,] matrixCloned = (string[,])matrix.Clone();

                        if (!WithinLimit(matrixCloned, row, column, size))
                            continue;

                        int north = CountNorth(matrixCloned, row, column, size);
                        int east = CountEast(matrixCloned, row, column, size);
                        int south = CountSouth(matrixCloned, row, column, size);
                        int west = countWest(matrixCloned, row, column, size);
                        int central = countCentral(matrixCloned, row, column);

                        int total = (north + east + south + west + central);
                        LittleSize(matrixCloned, list, total);                     
                    }
                }
            }
        }

        private void LittleSize(string[,] matrix, IList<int> list, int number1)
        {
            for (int size = 0; size < 8; size++)
            {
                for (int row = 1; row < matrix.GetLength(0); row++)
                {
                    for (int column = 1; column < matrix.GetLength(1); column++)
                    {
                        string[,] matrixCloned = (string[,])matrix.Clone();
                        IList<int> templist = new List<int>();

                        if (!WithinLimit(matrixCloned, row, column, size))
                            continue;

                        int north = CountNorth(matrixCloned, row, column, size);
                        int east = CountEast(matrixCloned, row, column, size);
                        int south = CountSouth(matrixCloned, row, column, size);
                        int west = countWest(matrixCloned, row, column, size);
                        int central = countCentral(matrixCloned, row, column);

                        templist.Add(number1);
                        templist.Add(north + east + south + west + central);

                        list.Add(MultiplyAll(templist));
                    }
                }
            }
        }

        private void IntialiseMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                IntialiseColumn(matrix, row);
            }
        }

        private void IntialiseColumn(string[,] matrix, int row)
        {
            char[] inputValue = System.Console.ReadLine().ToCharArray();
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[row, column] = inputValue[column].ToString(); ;
            }
        }

        private bool WithinLimit(string[,] matrix, int row, int column, int size)
        {
            if (CheckingBlocksPlusSize(matrix, row, column, row - size, size, "n") && CheckingBlocksPlusSize(matrix, row, column, column + size, size, "e") 
                && CheckingBlocksPlusSize(matrix, row, column, row + size, size, "s") && CheckingBlocksPlusSize(matrix, row, column, column - size, size, "w") )
                return true;

            return false;
        }

        private bool CheckingBlocksPlusSize(string[,] matrix, int row, int column, int endpoint, int size, string direction)
        {
            int count = 0;
            int starpoint = 0;

            if (direction.Equals("n"))
            {
                starpoint = row - 1;

                if (endpoint <= -1 || starpoint <= -1)
                    return false;

                while (starpoint >= endpoint)
                {
                    if (!matrix[starpoint, column].Equals("G"))
                    {
                        count = 0;
                        break;
                    }

                    count++;
                    starpoint--;
                }

                if (count == size)
                    return true;
            }
            else if (direction.Equals("e"))
            {
                starpoint = column + 1;

                if (endpoint >= matrix.GetLength(1) || starpoint >= matrix.GetLength(1))
                    return false;

                while (starpoint <= endpoint)
                {
                    if (!matrix[row, starpoint].Equals("G"))
                    {
                        count = 0;
                        break;
                    }

                    count++;
                    starpoint++;
                }

                if (count == size)
                    return true;
            }
            else if (direction.Equals("s"))
            {
                starpoint = row + 1;

                if (endpoint >= matrix.GetLength(0) || starpoint >= matrix.GetLength(0))
                    return false;

                while (starpoint <= endpoint)
                {
                    if (!matrix[starpoint, column].Equals("G"))
                    {
                        count = 0;
                        break;
                    }

                    count++;
                    starpoint++;
                }

                if (count == size)
                    return true;
            }
            else if (direction.Equals("w"))
            {
                starpoint = column - 1;

                if (endpoint <= -1 || starpoint <= -1)
                    return false;

                while (starpoint >= endpoint)
                {
                    if (!matrix[row, starpoint].Equals("G"))
                    {
                        count = 0;
                        break;
                    }

                    count++;
                    starpoint--;
                }

                if (count == size)
                    return true;
            }

            return false;
        }

        private int CountNorth(string[,] matrix, int row, int column, int size)
        {
            int limit = row - size;
            int count = 0;

            for (int currentrow = row - 1; currentrow >= limit; currentrow--)
            {
                matrix[currentrow, column] = "*";
                count++;
            }

            return count;
        }

        private int CountEast(string[,] matrix, int row, int column, int size)
        {
            int limit = column + size;
            int count = 0;

            for (int currentcolumn = column + 1; currentcolumn <= limit; currentcolumn++)
            {
                matrix[row, currentcolumn] = "*";
                count++;
            }

            return count;
        }

        private int CountSouth(string[,] matrix, int row, int column, int size)
        {
            int limit = row + size;
            int count = 0;

            for (int currentrow = row + 1; currentrow <= limit; currentrow++)
            {
                matrix[currentrow, column] = "*";
                count++;
            }

            return count;
        }

        private int countWest(string[,] matrix, int row, int column, int size)
        {
            int limit = column - size;
            int count = 0;

            for (int currentcolumn = column - 1; currentcolumn >= limit; currentcolumn--)
            {
                matrix[row, currentcolumn] = "*";
                count++;
            }

            return count;
        }

        private int countCentral(string[,] matrix, int row, int column)
        {
            matrix[row, column] = "*";
            return 1;
        }

        private int MultiplyAll(IList<int> list)
        {
            if (list.Count == 0)
                return 0;

            int count = 1;

            for (int index = 0; index < 2; index++)
            {
                count = count * list[index];
            }

            return count;
        }
    }
}
