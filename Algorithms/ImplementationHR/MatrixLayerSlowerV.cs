using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    [Obsolete("This is a slower version")]
    public class MatrixLayerSlowerV
    {
        private int[,] _matrix { set; get; }
        private int _rotation { set; get; }
        private int _leasetPossibleLayer { set; get; }
        private IDictionary<string, IList<int>> _collectionExtraction { get; set; }
        private IDictionary<string, IList<int>> _sizeOfEachExtraction { get; set; }

        public MatrixLayerSlowerV(int row, int column, int rotation)
        {
            _matrix = new int[row, column];
            _rotation = rotation;
            _collectionExtraction = new Dictionary<string, IList<int>>();
            _sizeOfEachExtraction = new Dictionary<string, IList<int>>();
        }

        public void Execute()
        {
            InitialiseMatrix();
            _leasetPossibleLayer = Convert.ToInt32(_matrix.GetLength(0) <= _matrix.GetLength(1) ? Math.Ceiling((_matrix.GetLength(0) / 2.00)) : Math.Ceiling(_matrix.GetLength(1) / 2.00));

            FragmentationOfMatrix();
            Rotate();
            ReconstructMatrix();
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

        public void FragmentationOfMatrix()
        {
            for (int layer = 0; layer < _leasetPossibleLayer; layer++)
            {
                IDictionary<int, IList<int>> fragmentsOfMatrix = new Dictionary<int, IList<int>>();

                fragmentsOfMatrix.Add(0, RetrieveNorthSectionMatrix(layer, layer, (_matrix.GetLength(1)) - layer));
                fragmentsOfMatrix.Add(1, RetrieveEasternSectionMatrix(layer + 1, (_matrix.GetLength(1) - 1) - layer, _matrix.GetLength(0) - layer));

                if (fragmentsOfMatrix[0].Count <= 1 || fragmentsOfMatrix[1].Count == 0)
                {
                    if (fragmentsOfMatrix[0].Count >= 1)
                    {
                        RetrieveSingleSection(layer, "north", fragmentsOfMatrix);
                    }
                    else
                    {
                        RetrieveSingleSection(layer, "east", null); ;
                    }
                }
                else
                {
                    fragmentsOfMatrix.Add(2, RetrieveSouthernSectionMatrix((_matrix.GetLength(0) - 1) - layer, (_matrix.GetLength(1) - 2) - layer, -1 + layer));
                    fragmentsOfMatrix.Add(3, RetrieveWesternSectionMatrix((_matrix.GetLength(0) - 2) - layer, layer, layer));

                    RetrieveMultipleSection(fragmentsOfMatrix, layer);
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

        private void RetrieveSingleSection(int layer, string sectionType, IDictionary<int, IList<int>> extractionSection)
        {
            if (extractionSection == null)
                extractionSection = new Dictionary<int, IList<int>>();

            int extractedlayerIndex = 0;

            if (sectionType.Equals("north"))
            {
                IList<int> extractedLayer = extractionSection[0];

                if (extractedLayer.Count == 1)
                    return;

                _collectionExtraction.Add(layer + " north", extractedLayer);
                _sizeOfEachExtraction.Add(layer + " north", new List<int>());
            }
            else if (sectionType.Equals("east"))
            {
                extractionSection.Add(1, RetrieveEasternSectionMatrix(layer, layer, _matrix.GetLength(0) - layer));
                IList<int> extractedLayer = extractionSection[1];

                if (extractedLayer.Count == 1)
                    return;

                _collectionExtraction.Add(layer + " east", extractedLayer);
                _sizeOfEachExtraction.Add(layer + " east", new List<int>());
            }
        }

        private void RetrieveMultipleSection(IDictionary<int, IList<int>> extractedSections, int layer)
        {
            IList<int> sizeOfSectors = new List<int>();
            IList<int> layerExtracted = RetrieveExtractedLayer(extractedSections, sizeOfSectors);

            _collectionExtraction.Add(layer + "", layerExtracted);
            _sizeOfEachExtraction.Add(layer + "", sizeOfSectors);
        }

        private IList<int> RetrieveExtractedLayer(IDictionary<int, IList<int>> extractedSectors, IList<int> sizeOfSector)
        {
            IList<int> extractedDataMerged = new List<int>();

            for (int sectorsKeys = 0; sectorsKeys < extractedSectors.Count; sectorsKeys++)
            {
                if (extractedSectors[sectorsKeys].Count > 0)
                {
                    var tempdList = extractedDataMerged.Concat(extractedSectors[sectorsKeys]);
                    extractedDataMerged = tempdList.ToList();
                    sizeOfSector.Add(extractedSectors[sectorsKeys].Count);
                }
            }

            return extractedDataMerged;
        }

        private void Rotate()
        {
            for (int cycle = 0; cycle < _rotation; cycle++)
            {
                RotateLayers();
            }
        }

        private void RotateLayers()
        {
            foreach (string collec in _collectionExtraction.Keys)
            {
                int holdFirst = _collectionExtraction[collec][0];

                _collectionExtraction[collec].RemoveAt(0);
                _collectionExtraction[collec].Add(holdFirst);
            }
        }

        private void ReconstructMatrix()
        {
            foreach (string item in _collectionExtraction.Keys)
            {
                if (item.Contains("north") || item.Contains("east"))
                {
                    ReconstructSingleLine(item);
                    continue;
                }
                else
                {
                    int layer = int.Parse(item);
                    ReconstructWholeMatrix(_collectionExtraction[item], _sizeOfEachExtraction[item], layer);
                }
            }
        }

        private void ReconstructSingleLine(string item)
        {
            string[] keySplit = item.Split(' ');
            int layer = int.Parse(keySplit[0]);
            int extractedlayerIndex = 0;

            if (keySplit[1].Equals("north"))
            {
                ReconstructNorthenSectionMatrix(layer, layer, (_matrix.GetLength(1)) - layer, _collectionExtraction[item], ref extractedlayerIndex);
            }
            else if (keySplit.Equals("east"))
            {
                ReconstructEasternSectionMatrix(layer, layer, _matrix.GetLength(0) - layer, _collectionExtraction[item], ref extractedlayerIndex);
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
