using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Common;
using System.Linq;

namespace TestCasesInventory.Web.Common.Utils
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SortableLink(this HtmlHelper htmlHelper, HttpRequestBase request, string sortFieldName, string title)
        {
            var baseUrl = new UriBuilder(request.Url.AbsoluteUri);
            var queryStrings = HttpUtility.ParseQueryString(baseUrl.Query.ToString());
            //add SortField
            queryStrings["sortField"] = sortFieldName;
            //default value. will fix later
            queryStrings["sortDirection"] = "Asc";
            baseUrl.Query = queryStrings.ToString();
            var link = string.Format("<a class=\"sort-field\" href=\"{0}\" title=\"{1}\">{1}</a>", baseUrl.ToString(), title);
            return new MvcHtmlString(link);
        }
    }
}
