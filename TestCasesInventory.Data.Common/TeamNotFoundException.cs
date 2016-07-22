using TestCasesInventory.Common;

namespace TestCasesInventory.Data.Common
{
    public class TeamNotFoundException : NotFoundException
    {
        public TeamNotFoundException() { }
        public TeamNotFoundException(string message) { }
    }
}
