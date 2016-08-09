using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Common
{
    public static class StringExtensions
    {
        public static int ToInt(this string input)
        {
            var output = 0;
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            if (int.TryParse(input, out output))
            {
                return output;
            }
            return 0;
        }
    }
}
