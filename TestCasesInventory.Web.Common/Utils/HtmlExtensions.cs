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
            var url = request.AddQueryString(new KeyValuePair<string, string>("sortField", sortFieldName),
                new KeyValuePair<string, string>("sortDirection", "Asc"));

            var link = string.Format("<a class=\"sort-field\" href=\"{0}\" title=\"{1}\">{1}</a>", url, title);
            return new MvcHtmlString(link);
        }
    }
}
