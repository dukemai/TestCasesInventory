using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

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

        public static MvcHtmlString FilterOptions(this HtmlHelper htmlHelper, KeyValuePair<string, string> filterOption)
        {
            var elementsTemplate = new StringBuilder(@"<input type=""checkbox"" name=""filterField"" checked=""checked"" id = ""{0}"" value = ""{1}"" />");
            elementsTemplate.Append(@"<label for= ""{0}"" >{2}</label>");
            var element = string.Format(elementsTemplate.ToString(), "ckb" + filterOption.Key, filterOption.Key, filterOption.Value);
            return new MvcHtmlString(element);
        }
    }
}
