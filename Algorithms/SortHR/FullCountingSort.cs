using System;
using System.Text;

namespace Algorithms.SortHR
{
	class FullCountingSort
	{
		public FullCountingSort()
		{
			this.Execute();
		}

		private void Execute()
		{
			int _size = int.Parse(Console.ReadLine());

			Item[] _items = new Item[_size];
			Item[] _sorted = new Item[_size];
			int[] _count = new int[100];

			int _half = _items.Length / 2;
			Item _temp1;

			for (int index = 0; index < _half; ++index)
			{
				_temp1 = _items[index] = new Item();
				_sorted[index] = new Item();
				string[] _input = Console.ReadLine().Split(' ');
				++_count[_temp1.Number = int.Parse(_input[0])];
			}

			//Console.WriteLine("Hle");
			for (int index = _half; index < _size; ++index)
			{
				_temp1 = _items[index] = new Item();
				_sorted[index] = new Item();
				string[] _input = Console.ReadLine().Split(' ');
				++_count[_temp1.Number = int.Parse(_input[0])];
				_temp1.Word = new StringBuilder(_input[1]);
				//_temp1.Word = _temp1.Word.trim
			}

			for (int index = 1; index < 100; ++index)
			{
				_count[index] = _count[index] + _count[index - 1];
			}

			for (int index = _size - 1; index >= _half; --index)
			{
				_temp1 = _items[index];

				_sorted[--_count[_temp1.Number]] = _temp1;
			}

			StringBuilder sout = new StringBuilder("");

			foreach (Item item in _sorted)
			{
				sout.Append(item.Word + " ");
			}
			Console.WriteLine(sout);
		}

		public class Item // Cannot be made into a parameter if inner class if private
		{
			public int Number { get; set; }
			public StringBuilder Word { get; set; }

			public Item()
			{
				//
				this.Word = new StringBuilder("-");
			}

			public Item(int number, StringBuilder word)
			{
				this.Number = number;
				this.Word = word;
			}
		}
	}
}
