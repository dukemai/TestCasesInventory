using TestCasesInventory.Common;

namespace TestCasesInventory.Data.Common
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) { }
    }
}
