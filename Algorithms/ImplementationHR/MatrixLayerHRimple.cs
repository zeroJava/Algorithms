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
        private int _leasetPossibleLayer { set; get; }

        public MatrixLayerHRimple(int row, int column, int rotation)
        {
            _matrix = new int[row, column];
            _rotation = rotation;
        }

        public void Execute()
        {
            InitialiseMatrix();
            _leasetPossibleLayer = Convert.ToInt32(_matrix.GetLength(0) <= _matrix.GetLength(1) ? Math.Ceiling((_matrix.GetLength(0) / 2.00)) : Math.Ceiling(_matrix.GetLength(1) / 2.00));
            System.Console.WriteLine(_leasetPossibleLayer);

            /*for (int cycle = 0; cycle < _rotation; cycle++) { RotateMatrix(); }*/
            RotateMatrix();
            DisplayMatrix();
        }

        private void InitialiseMatrix()
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

        private void RotateMatrix()
        {
            for (int layer = 0; layer < _leasetPossibleLayer; layer++)
            {
                IDictionary<int, IList<int>> sectionsOfMatrix = new Dictionary<int, IList<int>>();
                
                sectionsOfMatrix.Add(0, RetrieveNorthSectionMatrix(layer, layer,  (_matrix.GetLength(1)) - layer));
                sectionsOfMatrix.Add(1, RetrieveEasternSectionMatrix(layer + 1, (_matrix.GetLength(1) - 1) - layer,  _matrix.GetLength(0) - layer));

                if (sectionsOfMatrix[0].Count <= 1 || sectionsOfMatrix[1].Count == 0)
                {
                    if (sectionsOfMatrix[0].Count >= 1)
                    {
                        RotateSingleSection(layer, "north", sectionsOfMatrix);
                    }
                    else
                    {
                        RotateSingleSection(layer, "east", null); ;
                    }
                }
                else
                {
                    sectionsOfMatrix.Add(2, RetrieveSouthernSectionMatrix((_matrix.GetLength(0) - 1) - layer, (_matrix.GetLength(1) - 2) - layer, -1 + layer));
                    sectionsOfMatrix.Add(3, RetrieveWesternSectionMatrix((_matrix.GetLength(0) - 2) - layer, layer, layer));

                    RotateMultipleSection(sectionsOfMatrix, layer);
                }
            }
        }

        private IList<int> RetrieveNorthSectionMatrix(int rowPosition, int columnPosition, int limit)
        {
            IList<int> tempArray = new List<int>();

            if (columnPosition >= limit)
                return tempArray;

            for (int column = columnPosition; column < limit; column++)
            {
                tempArray.Add(_matrix[rowPosition, column]);
            }

            return tempArray;
        }

        private IList<int> RetrieveEasternSectionMatrix(int rowPosition, int columnPosition, int limit)
        {
            IList<int> tempArray = new List<int>();

            if (rowPosition >= limit)
                return tempArray;

            for (int row = rowPosition; row < limit; row++)
            {
                tempArray.Add(_matrix[row, columnPosition]);
            }

            return tempArray;
        }

        private IList<int> RetrieveSouthernSectionMatrix(int rowPosition, int columnPosition, int limit)
        {
            IList<int> tempArray = new List<int>();

            if (columnPosition <= limit)
                return tempArray;

            for (int column = columnPosition; column > limit; column--)
            {
                tempArray.Add(_matrix[rowPosition, column]);
            }

            return tempArray;
        }

        private IList<int> RetrieveWesternSectionMatrix(int rowPosition, int columnPosition, int limit)
        {
            IList<int> tempArray = new List<int>();

            if (rowPosition <= limit)
                return tempArray;

            for (int row = rowPosition; row > limit; row--)
            {
                tempArray.Add(_matrix[row, columnPosition]);
            }

            return tempArray;
        }

        private void RotateSingleSection(int layer, string sectionType, IDictionary<int, IList<int>> extractionSection)
        {
            if (extractionSection == null)
                extractionSection = new Dictionary<int, IList<int>>();

            int extractedlayerIndex = 0;

            if (sectionType.Equals("north"))
            {
                IList<int> extractedLayer = extractionSection[0];

                if (extractedLayer.Count == 1)
                    return;

                Rotate(extractedLayer);
                ReconstructNorthenSectionMatrix(layer, layer, (_matrix.GetLength(1)) - layer, extractedLayer, ref extractedlayerIndex);
            }
            else if (sectionType.Equals("east"))
            {
                extractionSection.Add(1, RetrieveEasternSectionMatrix(layer, layer, _matrix.GetLength(0) - layer));
                IList<int> extractedLayer = extractionSection[1];

                if (extractedLayer.Count == 1)
                    return;

                Rotate(extractedLayer);
                ReconstructEasternSectionMatrix(layer, layer, _matrix.GetLength(0) - layer, extractedLayer, ref extractedlayerIndex);
            }
        }

        private void RotateMultipleSection(IDictionary<int, IList<int>> extractedSections, int layer)
        {
            IList<int> sizeOfSectors = new List<int>();
            IList<int> layerExtracted = RetrieveExtractedLayer(extractedSections, sizeOfSectors);

            Rotate(layerExtracted);
            ReconstructWholeMatrix(layerExtracted, sizeOfSectors, layer);
        }

        private IList<int> RetrieveExtractedLayer(IDictionary<int, IList<int>> extractedSectors, IList<int> sizeOfSector)
        {
            IList<int> extractedDataMerged = new List<int>();

            for (int sectorsKeys = 0; sectorsKeys < extractedSectors.Count; sectorsKeys++)
            {
                if (extractedSectors[sectorsKeys].Count > 0)
                {
                    /*for (int sectorsPosition = 0; sectorsPosition < extractedSectors[sectorsKeys].Count; sectorsPosition++)
                    {
                        extractedDataMerged.Add(extractedSectors[sectorsKeys][sectorsPosition]);
                    }*/
                    var tempdList = extractedDataMerged.Concat(extractedSectors[sectorsKeys]);
                    extractedDataMerged = tempdList.ToList();
                    sizeOfSector.Add(extractedSectors[sectorsKeys].Count);
                }
            }

            return extractedDataMerged;
        }       

        private void Rotate(IList<int> list)
        {
            for (int cycle = 0; cycle < _rotation; cycle++)
            {
                int holdFirst = list[0];

                list.RemoveAt(0);
                list.Add(holdFirst);
            }
        }

        private void ReconstructWholeMatrix(IList<int> extractedLayer, IList<int> sizeOfSectors, int layer)
        {
            int extractedlayerIndex = 0;

            for (int reconstructionCycle = 0; reconstructionCycle < sizeOfSectors.Count; reconstructionCycle++)
            {
                if (extractedlayerIndex >= extractedLayer.Count)
                    break;

                switch (reconstructionCycle)
                {
                    case 0:
                        ReconstructNorthenSectionMatrix(layer, layer, (_matrix.GetLength(1)) - layer, extractedLayer, ref extractedlayerIndex);
                        break;
                    case 1:
                        ReconstructEasternSectionMatrix(layer + 1, (_matrix.GetLength(1) - 1) - layer, _matrix.GetLength(0) - layer, extractedLayer, ref extractedlayerIndex);
                        break;
                    case 2:
                        ReconstructSourthenSectionMatrix((_matrix.GetLength(0) - 1) - layer, (_matrix.GetLength(1) - 2) - layer, -1 + layer, extractedLayer, ref extractedlayerIndex);
                        break;
                    case 3:
                        ReconstructWeternSectionMatrix((_matrix.GetLength(0) - 2) - layer, layer, layer, extractedLayer, ref extractedlayerIndex);
                        break;
                }
            }
        }

        private void ReconstructNorthenSectionMatrix(int rowPosition, int columnPosition, int limit, IList<int> extractedLayer, ref int extractedIndex)
        {
            for (int column = columnPosition; column < limit; column++)
            {
                _matrix[rowPosition, column] = extractedLayer[extractedIndex];
                extractedIndex++;
            }
        }

        private void ReconstructEasternSectionMatrix(int rowPosition, int columnPosition, int limit, IList<int> extractedLayer, ref int extractedIndex)
        {
            for (int row = rowPosition; row < limit; row++)
            {
                _matrix[row, columnPosition] = extractedLayer[extractedIndex];
                extractedIndex++;
            }
        }

        private void ReconstructSourthenSectionMatrix(int rowPosition, int columnPosition, int limit, IList<int> extractedLayer, ref int extractedIndex)
        {
            for (int column = columnPosition; column > limit; column--)
            {
                _matrix[rowPosition, column] = extractedLayer[extractedIndex];
                extractedIndex++;
            }
        }

        private void ReconstructWeternSectionMatrix(int rowPosition, int columnPosition, int limit, IList<int> extractedLayer, ref int extractedIndex)
        {
            for (int row = rowPosition; row > limit; row--)
            {
                _matrix[row, columnPosition] = extractedLayer[extractedIndex];
                extractedIndex++;
            }
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
