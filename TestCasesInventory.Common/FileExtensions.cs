using System.IO;
using System.Web;

namespace TestCasesInventory.Common
{
    public static class FileExtensions
    {
        public static bool IsRelativePathExisted(this HttpServerUtilityBase server, string relativeUrl)
        {
            var serverUrl = server.MapPath(relativeUrl);
            return File.Exists(serverUrl);
        }
    }
}
