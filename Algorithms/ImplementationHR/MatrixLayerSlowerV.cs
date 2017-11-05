using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            //List<Task> tasks = new List<Task>();

            /*for (int layer = 0; layer < _leastPossibleLayer; layer++)
            {
                int layerNumber = layer;
                tasks.Add(Task.Factory.StartNew(() => RotateMatrixAlgo(layerNumber)));
            }*/

            Parallel.For(0, _leastPossibleLayer, l => RotateMatrixAlgo(l));

            Console.WriteLine("");
            //Task.WaitAll(tasks.ToArray());
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

        private void RotateMatrixAlgo(int layer)
        {
            //int[,] tempMatrix = _matrix;
            for (int cycle = 0; cycle < _rotation; cycle++)
            {
                //tempMatrix = this.RotationMatrixElements(layer, tempMatrix);
                _matrix = this.RotationMatrixElements(layer, _matrix);
            }

            //return tempMatrix;
        }

        private int[,] RotationMatrixElements(int layer, int[,] matrix)
        {
            int[,] tempMatrix = matrix;

            if (layer == (matrix.GetLength(0) - 1) - layer
                    && layer == (matrix.GetLength(1) - 1) - layer)
            {
                int loc = (matrix.GetLength(0) - layer) - 1;
                tempMatrix[loc, loc] = matrix[loc, loc];
                return tempMatrix;
            }

            int firstValue = matrix[layer, layer];
            int nCol = layer;
            int nRow = layer;
            // north side
            for (int northColumn = layer; northColumn < (matrix.GetLength(1) - layer) - 1; northColumn++)
            {
                tempMatrix[layer, northColumn] = matrix[layer, northColumn + 1];
                nRow = layer;
                nCol = northColumn + 1;
            }

            if (layer >= (matrix.GetLength(0) - layer))
            {
                tempMatrix[nRow, nCol] = firstValue; ;
                return tempMatrix;
            }

            bool lastNColumnAssigned = false;
            int eCol = nCol;
            int eRow = layer;
            // west side
            for (int eastRow = layer; eastRow < (matrix.GetLength(0) - layer) - 1; eastRow++)
            {
                if (!lastNColumnAssigned)
                {
                    tempMatrix[eastRow, nCol] = matrix[eastRow, nCol];
                    lastNColumnAssigned = true;
                }
                tempMatrix[eastRow, nCol] = matrix[eastRow + 1, nCol];
                eRow = eastRow + 1;
                eCol = nCol;
            }

            int sCol = eCol;
            int sRow = eRow;
            // south side
            for (int southCol = (matrix.GetLength(1) - layer) - 1; southCol > layer; southCol--)
            {
                tempMatrix[sRow, southCol] = matrix[sRow, southCol - 1];
                sCol = southCol - 1;
                sRow = eRow;
            }

            int wCol = sCol;
            int wRow = sRow;
            // west side
            for (int westRow = (matrix.GetLength(0) - layer) - 1; westRow > layer; westRow--)
            {
                tempMatrix[westRow, wCol] = matrix[westRow - 1, wCol];
                wRow = westRow;
                wCol = sCol;
            }

            tempMatrix[layer + 1, layer] = firstValue;

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
