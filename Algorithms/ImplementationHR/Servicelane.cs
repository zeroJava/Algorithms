using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    class Servicelane
    {
        private Dictionary<int, int> _dictionary = new Dictionary<int, int>();

        public Servicelane()
        {
            // getting and allocating the length of the lane, and the number of testcases.
            System.Console.WriteLine("Enter number of lanes and tescases. E.g. 8 5");
            string[] _firstInput = System.Console.ReadLine().Split(' ');
            int _nlength = Int32.Parse(_firstInput[0]);
            int _testcase = Int32.Parse(_firstInput[1]);

            // Getting the width of of all segments in the lane
            System.Console.WriteLine("Enter widths for n number of segments. E.g. 3 2 1 3 4 5 4 ");
            int[] _width = Array.ConvertAll(System.Console.ReadLine().Split(' '), Int32.Parse);

            // this method will execute our algorithm
            this.Execute(_testcase, _width);
        }

        private void Execute(int _testcase, int[] _width)
        {
            // running our tescases
            for(int index = 0; index < _testcase; index++)
            {
                System.Console.WriteLine("Enter entry and exit");

                // Getting the entry and exit point.
                string[] line = System.Console.ReadLine().Split(' ');
                int _entry = Int32.Parse(line[0]);
                int _end = Int32.Parse(line[1]);

                // TypeOfvehicle return the type of vehicle that is allowed by calculatig the entry and exit point
                System.Console.WriteLine(TypeOfVehicle(_entry, _end, _width));
            }
        }

        private int TypeOfVehicle(int _entry, int _exit, int[] _width)
        {
            int _type = 3; // We put the largest vehicle as the default
            int _cycle = _entry;

            while(_cycle <= _exit)
            {
                if(_width[_cycle] < _type) // if a the current width is smaller than the current type of vehicle, then it is illegal to use that vehicle in that route 
                {
                    _type = _width[_cycle];
                }
                _cycle++;
            }

            return _type;
        }
    }
}
