using TestCasesInventory.Common;

namespace TestCasesInventory.Data.Common
{
    public class TestRunNotFoundException : NotFoundException
    {
        public TestRunNotFoundException() { }
        public TestRunNotFoundException(string message) { }
    }
}
