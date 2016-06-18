using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SortHR
{
    class FullCountingSortWithClasses
    {

        public FullCountingSortWithClasses()
        {
            int _size = int.Parse(System.Console.ReadLine());

            IList<Item> _items = new List<Item>();
            this.Populate(_items, _size);
            this.Execute(_items);
        }

        public void Execute(IList<Item> items)
        {
            int _index = 0;
            while (_index < items.Count - 1)
            {
                int _tempInd = 1;
                for (int numberIndex = _index + 1; numberIndex < items.Count; numberIndex++)
                {
                    if (items[_index].Number == items[numberIndex].Number)
                    {
                        //this.ShiftRight(items, _index + _tempInd, numberIndex);
                        Item _temp = items[numberIndex];
                        items.RemoveAt(numberIndex);
                        items.Insert(_index + _tempInd, _temp);
                        _tempInd++;
                    }
                    else if (items[_index].Number > items[numberIndex].Number)
                    {
                        //this.ShiftRight(items, _index, numberIndex);
                        Item _temp = items[numberIndex];
                        items.RemoveAt(numberIndex);
                        items.Insert(_index, _temp);
                        _tempInd = 1;
                    }
                }

                if (_tempInd > 1)
                {
                    _index = _index + _tempInd;
                    continue;
                }

                _index++;
            }

            Display(items);
        }

        private void Populate(IList<Item> items, int size)
        {
            for(int index = 0; index < size; index++)
            {
                string[] _input = System.Console.ReadLine().Split(' ');

                Item _tempItem = new Item(int.Parse(_input[0]), index < size / 2 ? "-" : _input[1]);
                items.Add(_tempItem);
            }
        }

        public void Display(IList<Item> items)
        {
            StringBuilder _string = new StringBuilder();
            for(int index = 0; index < items.Count; index++)
            {
                //System.Console.Write(items[index].Word + " ");
                _string.Append(items[index].Word + " ");
            }

            System.Console.WriteLine(_string.ToString());
        }

        public class Item // Cannot be made into a parameter if inner class if private
        {
            public int Number { get; set; }
            public string Word { get; set; }

            public Item(int number, string word)
            {
                this.Number = number;
                this.Word = word;
            }
        }
    }
}
