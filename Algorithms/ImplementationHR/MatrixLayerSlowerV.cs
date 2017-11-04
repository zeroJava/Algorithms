using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class MatrixLayerSlowerV
    {
        private int[,] _matrix { set; get; }
        private int _rotation { set; get; }
        private int _leastPossibleLayer;

        public MatrixLayerSlowerV(int row, int column, int rotation)
        {
            _matrix = new int[row, column];
            _rotation = rotation;
            _leastPossibleLayer = Convert.ToInt32(
                    _matrix.GetLength(0) <= _matrix.GetLength(1)
                    ? Math.Ceiling((_matrix.GetLength(0) / 2.00))
                    : Math.Ceiling(_matrix.GetLength(1) / 2.00));
        }

        public void Execute()
        {
            InitialiseMatrix();
            for (int cycle = 0; cycle < _rotation; cycle++)
            {
                _matrix = RotateTheMatrix();
            }
            DisplayMatrix();
        }

        public void InitialiseMatrix()
        {
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                int[] number = System.Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                for (int column = 0; column < _matrix.GetLength(1); column++)
                {
                    _matrix[row, column] = number[column];
                }
            }
        }

        private int[,] RotatedMatrix()
        {
            int[,] newMatrix = new int[_matrix.GetLength(0), _matrix.GetLength(1)];
            for (int row = 0; row < _matrix.GetLength(0); ++row)
            {
                for (int column = 0; column < _matrix.GetLength(1); ++column)
                {
                    newMatrix[row, column] = _matrix[(_matrix.GetLength(0) - column) - 1, row];
                }
            }
            return newMatrix;
        }

        private int[,] RotateTheMatrix()
        {
            int[,] tempMatrix = new int[_matrix.GetLength(0), _matrix.GetLength(1)];
            for (int layer = 0; layer < _leastPossibleLayer; layer++)
            {
                int firstValue = _matrix[layer, layer];
                int nCol = layer;
                int nRow = layer;
                // north side
                for (int northColumn = layer; northColumn < (_matrix.GetLength(1) - layer) - 1; northColumn++)
                {
                    tempMatrix[layer, northColumn] = _matrix[layer, northColumn + 1];
                    nRow = layer;
                    nCol = northColumn + 1;
                }

                if (layer >= (_matrix.GetLength(0) - layer))
                {
                    tempMatrix[nRow, nCol] = firstValue; ;
                    continue;
                }

                bool lastNColumnAssigned = false;
                int eCol = nCol;
                int eRow = layer;
                // west side
                for (int eastRow = layer; eastRow < (_matrix.GetLength(0) - layer) - 1; eastRow++)
                {
                    if (!lastNColumnAssigned)
                    {
                        tempMatrix[eastRow, nCol] = _matrix[eastRow, nCol];
                        lastNColumnAssigned = true;
                    }
                    tempMatrix[eastRow, nCol] = _matrix[eastRow + 1, nCol];
                    eRow = eastRow;
                    eCol = nCol;
                }

                //bool lastERowAssigned = false;
                int sCol = eCol;
                int sRow = eRow;
                // south side
                for (int southCol = eCol; southCol > _matrix.GetLength(1) - (_matrix.GetLength(1) - layer); southCol--)
                {
                    /*if (!lastERowAssigned)
                    {
                        tempMatrix[sRow, southCol] = _matrix[sRow, southCol];
                        lastERowAssigned = true;
                    }*/
                    tempMatrix[sRow, southCol] = _matrix[sRow, southCol -1];
                    sCol = southCol;
                    sRow = eRow;
                }

                //bool lastSColumnAssogned = false;
                int wCol = sCol;
                int wRow = sRow;
                // west side
                for (int westRow = wRow; westRow > _matrix.GetLength(0) - (_matrix.GetLength(0) - layer); westRow--)
                {
                    /*if (!lastSColumnAssogned)
                    {
                        tempMatrix[westRow + 1, wCol] = _matrix[westRow, wCol];
                        lastSColumnAssogned = true;
                    }*/

                    tempMatrix[westRow, wCol] = _matrix[westRow - 1, wCol];
                    wRow = westRow;
                    wCol = sCol;
                }

                tempMatrix[1, 0] = firstValue;
            }

            return tempMatrix;
        }

        private void DisplayMatrix()
        {
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for (int column = 0; column < _matrix.GetLength(1); column++)
                {
                    System.Console.Write(_matrix[row, column] + " ");
                }
                System.Console.WriteLine("");
            }
        }
    }
}
