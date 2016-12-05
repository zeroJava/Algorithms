using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class MatrixLayerHRimple
    {
        private int[,] _matrix { set; get; }
        private int _rotation { set; get; }

        // TODO work on this algorithm after finishing 

        public MatrixLayerHRimple(int row, int column, int rotation)
        {
            _matrix = new int[row, column];
            _rotation = rotation;
        }

        private int SizeOfInnerMatrix(int[,] array)
        {
            int row = array.GetLength(0) - 2;
            int column = array.GetLength(1) - 2;

            return row + column;
        }

        private int[] ExtractInnerMatrix(int[,] array)
        {
            int[] tempArray = new int[SizeOfInnerMatrix(array)];
            int mainIndex = 0;

            for (int rowIndex = 1; rowIndex < array.GetLength(0) - 1; rowIndex++)
            {
                for (int columnIndex = 1; columnIndex < array.GetLength(1) -1; columnIndex++)
                {
                    tempArray[mainIndex] = array[rowIndex, columnIndex];
                    mainIndex++;
                }
            }

            return tempArray;
        }

        public void Execute()
        {

        }
    }
}
