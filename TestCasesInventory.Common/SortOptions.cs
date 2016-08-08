using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Common
{
    public enum SortDirections 
    {
        Asc,
        Desc
    }

    public class SortOptions
    {
        public string Field { get; set; }
        public SortDirections Direction { get; set; }
    }
}
