using TestCasesInventory.Presenter.Config;

namespace TestCasesInventory.Presenter.Common
{
    public class StringRemove
    {
        public static string getName(string Email)
        {
            string name = Email.Replace(EmailExtension.NitecoMail, "");
            return name;
        }
    }
}
