using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using TestCasesInventory.Common;

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

        public static List<string> GetFileNamesFromRelativeUrlDirectory(string relativeUrl, HttpServerUtilityBase server)
        {
            var physicalUrl = server.MapPath(relativeUrl);
            return Directory.Exists(physicalUrl) ? Directory.GetFiles(physicalUrl).ToList() : new List<string>();
        }

        public static List<string> GetFileRelativeUrlsFromRelativeUrlDirectory(string relativeUrl, HttpServerUtilityBase server)
        {
            var physicalUrl = server.MapPath(relativeUrl);
            if (Directory.Exists(physicalUrl))
            {
                var files = Directory.GetFiles(physicalUrl);
                return files.Select(f => UrlHelper.Combine(relativeUrl, f)).ToList();
            }
            return new List<string>();
        }
    }
}
