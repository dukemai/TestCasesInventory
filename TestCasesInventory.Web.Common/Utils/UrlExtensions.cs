using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestCasesInventory.Web.Common.Utils
{
    public static class UrlExtensions
    {
        public static string AddQueryString(this HttpRequestBase requestBase, params KeyValuePair<string, string>[] queries)
        {
            var baseUrl = new UriBuilder(requestBase.Url.AbsoluteUri);
            var queryStrings = HttpUtility.ParseQueryString(baseUrl.Query.ToString());
            foreach (var query in queries)
            {
                queryStrings[query.Key] = query.Value;
            }
            baseUrl.Query = queryStrings.ToString();
            return baseUrl.ToString();
        }
    }
}
