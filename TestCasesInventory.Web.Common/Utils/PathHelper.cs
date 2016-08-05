using System.IO;

namespace TestCasesInventory.Web.Common.Utils
{
    public class PathHelper
    {
        public static void EnsureDirectories(string filePath)
        {
            var dirs = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(dirs))
            {
                Directory.CreateDirectory(dirs);
            }
        }
    }
}
