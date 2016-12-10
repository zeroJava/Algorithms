using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class EmaSupercomputer
    {
        private IList<int> _number;

        public EmaSupercomputer()
        {
            _number = new List<int>();
        }

        public void Execute()
        {
            int row = int.Parse(System.Console.ReadLine());
            int column = int.Parse(System.Console.ReadLine());

            string[,] matrix = new string[5, 5];
            IntialiseMatrix(matrix);
            AlgorithmEma(matrix);
            System.Console.WriteLine("Vertical: " + ReachedEdgeVertical(matrix, 5, 0) + " Horizontal: " + ReachedEdgeHorizontal(matrix, 5, 1));
            DisplayMatrix(matrix);
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
            System.Console.WriteLine("Enter string of G and B");
            char[] inputValue = System.Console.ReadLine().ToCharArray();
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[row, column] = inputValue[column].ToString(); ;
            }
        }

        private void DisplayMatrix(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    System.Console.Write(matrix[row, column]);
                }
                System.Console.WriteLine();
            }
        }

        private void AlgorithmEma(string[,] matrix)
        {
            if (matrix.GetLength(0) < 3 || matrix.GetLength(1) < 3)
            {
                System.Console.WriteLine(0);
                return;
            }
                
            for (int rows = 1; rows < matrix.GetLength(0) - 1; rows++)
            {
                for (int columns = 1; columns < matrix.GetLength(1) - 1; columns++)
                {
                    int size = 0;
                    int badBlock = 0;

                    if (matrix[rows, columns].Equals("B") || matrix[rows, columns].Equals("S") || matrix[rows, columns].Equals("*"))
                        continue;

                    ScanSurroundingBlocks(matrix, rows, columns, out size, out badBlock);

                    if (badBlock > 2)
                        continue;

                    System.Console.WriteLine("Path: " + CreatePlus(matrix, rows, columns, size));
                }
            }
        }

        private void ScanSurroundingBlocks(string[,] matrix, int row, int column, out int size, out int badBlocks)
        {
            size = 0;
            badBlocks = 0;
            int[] array = new int[4];

            array[0] = CountBlockVertical(matrix, row, column, "north"); // north
            array[1] = countBlockHorizontal(matrix, row, column, "east"); // east
            array[2] = CountBlockVertical(matrix, row, column, "south"); // south
            array[3] = countBlockHorizontal(matrix, row, column, "west"); // west

            size = array.Min();
            badBlocks = array.Where(x => x == 0).Count();
            System.Console.WriteLine("ro: " + row + " col: " + column + " max: " + badBlocks);
        }

        private int CreatePlus(string[,] matrix, int row, int column, int size)
        {
            int north = CountingNorthenPath(matrix, row, column, size);
            int east = CountingEasternPath(matrix, row, column, size);
            int south = CountSouthernPath(matrix, row, column, size);
            int west = CountWesternPath(matrix, row, column, size);
            int central = 1;

            return north + east + south + west + central;
        }

        private int CountingNorthenPath(string[,] matrix, int row, int column, int size)
        {
            int limit = row - size;
            int count = 0;

            for(int currentrow = row - 1; currentrow >= limit; currentrow--)
            {
                matrix[currentrow, column] = "S";
                OverlapParameterFlagNorth(matrix, currentrow, column, limit);
                count++;
            }

            return count;
        }

        private int CountingEasternPath(string[,] matrix, int row, int column, int size)
        {
            int limit = column + size;
            int count = 0;

            for (int currentcolumn = column + 1; currentcolumn <= limit; currentcolumn++)
            {
                matrix[row, currentcolumn] = "S";
                OverlapParameterFlagEast(matrix, row, currentcolumn, limit);
                count++;
            }

            return count;
        }

        private int CountSouthernPath(string[,] matrix, int row, int column, int size)
        {
            int limit = row + size;
            int count = 0;

            for (int currentrow = row + 1; currentrow <= limit; currentrow++)
            {
                matrix[currentrow, column] = "S";
                OverlapParameterFlagSouth(matrix, currentrow, column, limit);
                count++;
            }

            return count;
        }

        private int CountWesternPath(string[,] matrix, int row, int column, int size)
        {
            int limit = column - size;
            int count = 0;

            for (int currentcolumn = column - 1; currentcolumn >= limit; currentcolumn--)
            {
                matrix[row, currentcolumn] = "S";
                OverlapParameterFlagWest(matrix, row, currentcolumn, limit);
                count++;
            }

            return count;
        }

        private void OverlapParameterFlagNorth(string[,] matrix, int row, int column, int limit)
        {
            if (!ReachedEdgeHorizontal(matrix, column, 1)) // eastern part
                matrix[row, column + 1] = "*";

            if (!ReachedEdgeHorizontal(matrix, column, 0)) // western part
                matrix[row, column - 1] = "*";

            if (row == limit)
            {
                if (!ReachedEdgeVertical(matrix, row, 0)) // north
                    matrix[row - 1, column] = "*";

                if (!ReachedEdgeDiaganol(matrix, row, column, "ne")) // north east
                    matrix[row - 1, column + 1] = "*";

                if (!ReachedEdgeDiaganol(matrix, row, column, "nw")) // north west
                    matrix[row - 1, column - 1] = "*";
            }
        }

        private void OverlapParameterFlagEast(string[,] matrix, int row, int column, int limit)
        {
            if (!ReachedEdgeVertical(matrix, row, 0)) // north
                matrix[row - 1, column] = "*";

            if (!ReachedEdgeVertical(matrix, row, 1)) // south
                matrix[row + 1, column] = "*";

            if (column == limit)
            {
                if (!ReachedEdgeHorizontal(matrix, column, 1)) //east
                    matrix[row, column + 1].Equals("*");

                if (!ReachedEdgeDiaganol(matrix, row, column, "ne")) // north east
                    matrix[row - 1, column + 1] = "*";

                if (!ReachedEdgeDiaganol(matrix, row, column, "se")) // south east
                    matrix[row + 1, column + 1] = "*";
            }
        }

        private void OverlapParameterFlagSouth(string[,] matrix, int row, int column, int limit)
        {
            if (!ReachedEdgeHorizontal(matrix, column, 1)) // eastern part
                matrix[row, column + 1] = "*";

            if (!ReachedEdgeHorizontal(matrix, column, 0)) // western part
                matrix[row, column - 1] = "*";

            if (row == limit)
            {
                if (!ReachedEdgeVertical(matrix, row, 1)) // south
                    matrix[row + 1, column] = "*";

                if (!ReachedEdgeDiaganol(matrix, row, column, "se")) // north east
                    matrix[row + 1, column + 1] = "*";

                if (!ReachedEdgeDiaganol(matrix, row, column, "sw")) // north west
                    matrix[row + 1, column - 1] = "*";
            }
        }

        private void OverlapParameterFlagWest(string[,] matrix, int row, int column, int limit)
        {
            if (!ReachedEdgeVertical(matrix, row, 0)) // north
                matrix[row - 1, column] = "*";

            if (!ReachedEdgeVertical(matrix, row, 1)) // south
                matrix[row + 1, column] = "*";

            if (column == limit)
            {
                if (!ReachedEdgeHorizontal(matrix, column, 0)) //west
                    matrix[row, column - 1].Equals("*");

                if (!ReachedEdgeDiaganol(matrix, row, column, "nw")) // north west
                    matrix[row - 1, column - 1] = "*";

                if (!ReachedEdgeDiaganol(matrix, row, column, "sw")) // south west
                    matrix[row + 1, column - 1] = "*";
            }
        }

        private int CountBlockVertical(string[,] matrix, int row, int column, string direction)
        {
            int count = 0;
            int currentRowPosition = row;

            if (direction.Equals("north"))
            {
                while (matrix[currentRowPosition, column].Equals("G"))
                {
                    //matrix[currentRowPosition, column] = "*";
                    count++;

                    if (ReachedEdgeVertical(matrix, currentRowPosition, 0))
                        break;

                    currentRowPosition--;
                }
            }
            else if (direction.Equals("south"))
            {
                while (matrix[currentRowPosition, column].Equals("G"))
                {
                    //matrix[currentRowPosition, column] = "*";
                    count++;

                    if (ReachedEdgeVertical(matrix, currentRowPosition, 1))
                        break;

                    currentRowPosition++;
                }
            }

            if (count <= 0)
                return 0;

            return count - 1;
        }

        private int countBlockHorizontal(string[,] matrix, int row, int column, string direction)
        {
            int count = 0;
            int currentColumnPosition = column;

            if (direction.Equals("west"))
            {
                while (matrix[row, currentColumnPosition].Equals("G"))
                {
                    //matrix[row, currentColumnPosition] = "*";
                    count++;

                    if (ReachedEdgeHorizontal(matrix, currentColumnPosition, 0))
                        break;

                    currentColumnPosition--;
                }
            }
            else if (direction.Equals("east"))
            {
                while (matrix[row, currentColumnPosition].Equals("G"))
                {
                    //matrix[row, currentColumnPosition] = "*";
                    count++;

                    if (ReachedEdgeHorizontal(matrix, currentColumnPosition, 1))
                        break;

                    currentColumnPosition++;
                }
            }

            if (count <= 0)
                return 0;

            return count - 1;
        }

        private bool ReachedEdgeVertical(string[,] matrix, int position, int direction)
        {
            if (direction <= 0)
            {
                return position - 1 <= -1 ? true : false;
            }
            else
            {
                return position + 1 >= matrix.GetLength(0) ? true : false;
            }
        }

        private bool ReachedEdgeHorizontal(string[,] matrix, int position, int direction)
        {
            if (direction <= 0)
            {
                return position - 1 <= -1 ? true : false;
            }
            else
            {
                return position + 1 >= matrix.GetLength(1) ? true : false;
            }
        }

        private bool ReachedEdgeDiaganol(string[,] matrix, int rowPosition, int columnPoistion, string direction)
        {
            if (direction.Equals("ne"))
            {
                return rowPosition - 1 <= -1 && columnPoistion + 1 >= matrix.GetLength(1) ? true : false;
            }
            else if (direction.Equals("se"))
            {
                return rowPosition + 1 >= matrix.GetLength(0) && columnPoistion + 1 >= matrix.GetLength(1) ? true : false;
            }
            else if (direction.Equals("sw"))
            {
                return rowPosition + 1 >= matrix.GetLength(0) && columnPoistion - 1 <= matrix.GetLength(1) ? true : false;
            }
            else //if (direction.Equals("nw"))
            {
                return rowPosition - 1 <= -1 && columnPoistion - 1 <= -1 ? true : false;
            }
        }
    }
}
