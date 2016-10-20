using TestCasesInventory.Presenter.Config;

namespace TestCasesInventory.Presenter.Common
{
    public class StringRemove
    {
        public static string GetNameFromEmail(string Email)
        {
            string name = Email.Replace(EmailConstant.NitecoMail, "");
            return name;
        }
    }
}
