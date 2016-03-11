using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    public class SherlockandSquare
    {
        private int _testcase;

        public SherlockandSquare()
        {
            System.Console.WriteLine("Enter testcase");
            _testcase = Int32.Parse(System.Console.ReadLine());
        }

        public void ExcuteLogic()
        {
            for(int te = 0; te < _testcase; te++)
            {
                System.Console.WriteLine("Enter lowesttest");
                int _lowestRange = Int32.Parse(System.Console.ReadLine());
                System.Console.WriteLine("Enter highestrange");
                int _highestRange = Int32.Parse(System.Console.ReadLine());
                int _counter = 0;

                for (int iteraction = _lowestRange; iteraction <= _highestRange; iteraction++)
                {
                    double value = Math.Sqrt((double)iteraction);

                    if (value % 1 == 0)
                    {
                        _counter++;
                    }
                }

                System.Console.WriteLine(_counter);
            }
        }

        public int Number(int count, int range, int higest)
        {
            int _co = count;
            if(range <= higest)
            {
                double value = Math.Sqrt((double)range);
                if(value % 1 == 0)
                {
                    _co++;
                }
                _co = Number(_co, range + 1, higest);
            }
            return _co;
        }
    }
}
