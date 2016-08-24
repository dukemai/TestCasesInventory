using System;
using System.Text;
using System.Web.Hosting;

namespace TestCasesInventory.Common
{
    public class UrlHelper
    {
        public static string Combine(params string[] slugs)
        {
            if (slugs.Length == 0)
            {
                return string.Empty;
            }
            if (slugs.Length == 1)
            {
                return slugs[0];
            }
            var length = slugs.Length;
            var start = slugs[0].TrimEnd('/');
            var end = slugs[length - 1].TrimStart('/');
            var sb = new StringBuilder();
            sb.Append(start);
            for (int i = 1; i < length - 1; i++)
            {
                sb.AppendFormat("/{0}", slugs[i].Trim());
            }
            sb.AppendFormat("/{0}", end);
            return sb.ToString();
        }

        public static string PhysicalPathToVirtualPath(string physicalPath)
        {
            var output = physicalPath.Replace(HostingEnvironment.ApplicationPhysicalPath, String.Empty);
            output = output.Replace('\\', '/').Insert(0, "~/");
            return output;
        }
    }
}
