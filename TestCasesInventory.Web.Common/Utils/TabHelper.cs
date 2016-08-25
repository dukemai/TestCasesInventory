namespace TestCasesInventory.Web.Common.Utils
{
    public class TabHelper
    {
        public static string TabStateClass(bool isActive)
        {
            return isActive ? "active" : "tab";
        }
        public static string TabContentStateClass(bool isActive)
        {
            return isActive ? "active" : "tab-content";
        }
    }
}
