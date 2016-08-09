using System;
using System.Web.ModelBinding;
using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Web.Common;

namespace TestCasesInventory.Bindings
{
    public class FilterOptionsBinding : System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var page = request.QueryString[PagingConfig.PageQueryString].ToInt();
            var filterKey = request["filterKeyword"] ?? string.Empty;
            var filterField = request["filterField"] ?? string.Empty;
            return new FilterOptions
            {
                Keyword = filterKey,
                FilterField = filterField,
                PagingOptions = new PagingOptions
                {
                    CurrentPage = page,
                    PageSize = PagingConfig.PageSize
                },
                SortOptions = new SortOptions
                {

                }
            };
        }
    }
}