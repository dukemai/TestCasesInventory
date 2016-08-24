using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Common
{
    public static class UrlExtensions
    {
        public static string GetFileNameFromRelativeUrl(this string relativeUrl)
        {
            var start = relativeUrl.LastIndexOf("/");
            var output = string.Empty;
            if (start > -1)
            {
                output = relativeUrl.Substring(start + 1);
            }
            else
            {
                output = relativeUrl;
            }
            return output;
        }
    }
}
