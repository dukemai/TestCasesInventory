using System.IO;

namespace TestCasesInventory.Presenter.Common
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
