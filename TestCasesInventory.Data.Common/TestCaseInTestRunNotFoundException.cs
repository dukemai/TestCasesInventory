using TestCasesInventory.Common;

namespace TestCasesInventory.Data.Common
{
    public class TestCaseInTestRunNotFoundException : NotFoundException
    {
        public TestCaseInTestRunNotFoundException() { }
        public TestCaseInTestRunNotFoundException(string message) { }
    }
}
