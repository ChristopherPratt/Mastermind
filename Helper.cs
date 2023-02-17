using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    internal class Helper
    {
        // checks a string to see if it is an integer.
        // returns a tuple with the first value being true if the string was a number
        // the second value being the converted integer
        public static Tuple<bool,int> convertInt(string _string)
        {
            int value;
            if (int.TryParse(_string, out value))
            {
                return Tuple.Create(true,value);
            }
            else
            {
                return Tuple.Create(false, 0);
            }

        }
    }
}
