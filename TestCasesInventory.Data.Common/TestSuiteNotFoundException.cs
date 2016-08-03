using TestCasesInventory.Common;

namespace TestCasesInventory.Data.Common
{
    public class TestSuiteNotFoundException : NotFoundException
    {
        public TestSuiteNotFoundException() { }
        public TestSuiteNotFoundException(string message) { }
    }
}
