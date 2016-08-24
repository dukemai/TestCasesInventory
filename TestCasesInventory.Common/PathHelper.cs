using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestCasesInventory.Common
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
                return files.Select(f => UrlHelper.Combine(relativeUrl, Path.GetFileName(f))).ToList();
            }
            return new List<string>();
        }
    }
}
