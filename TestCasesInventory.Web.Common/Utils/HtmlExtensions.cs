using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

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

        public static MvcForm BeginFormGet(this HtmlHelper htmlHelper)
        {
            string rawUrl = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormHelper(htmlHelper, rawUrl, FormMethod.Get, new RouteValueDictionary());
        }

        private static MvcForm FormHelper(HtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder.MergeAttribute("action", formAction);
            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);           
            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            MvcForm result = new MvcForm(htmlHelper.ViewContext);
           
            return result;
        }
    }
}
