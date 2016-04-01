using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ImplementationHR
{
    class SherlockBeast
    {
        public SherlockBeast()
        {
            System.Console.WriteLine("Please enter the of digits yo want to print");
            int _digits = Int32.Parse(System.Console.ReadLine());
            this.Execute(_digits);
        }

        public void Execute(int digits)
        {
            int _threes = 0;
            int _fives = 0;
            int _digits = digits;

            while(_digits > 0)
            {
                if(_digits % 3 == 0)
                {
                    _fives = _digits;
                    break;
                }

                _digits = _digits - 5;
            }

            _threes = digits - digits;

            if(_digits < 0 || _threes % 5 != 0)
            {
                System.Console.WriteLine("-1");
                return;
            }

            StringBuilder _stringBuilder = new StringBuilder(digits);
            while(_fives-- > 0)
            {
                _stringBuilder.Append("5");
            }
            while (_threes-- > 0)
            {
                _stringBuilder.Append("3");
            }

            System.Console.WriteLine(_stringBuilder.ToString());
        }
    }
}
