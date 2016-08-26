using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Common;

namespace TestCasesInventory.Data.Common
{
    public class TestRunResultNotFoundException : NotFoundException
    {
        public TestRunResultNotFoundException() { }
        public TestRunResultNotFoundException (string message) { }
    }
}
