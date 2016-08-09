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
            var page = request.QueryString[PagingConfig.PageQueryString].ToInt(1);
            var filterKey = request["filterKeyword"] ?? string.Empty;
            var filterField = request["filterField"] ?? string.Empty;
            var sortField = request["sortField"] ?? string.Empty;
            var sortDirection = request["sortDirection"] ?? string.Empty;
            return new FilterOptions
            {
                Keyword = filterKey,
                FilterFields = filterField.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                PagingOptions = new PagingOptions
                {
                    CurrentPage = page,
                    PageSize = PagingConfig.PageSize
                },
                SortOptions = new SortOptions
                {
                    Field = sortField,
                    Direction = string.Equals(sortDirection, "Desc", StringComparison.InvariantCultureIgnoreCase) ?
                    SortDirections.Desc : SortDirections.Asc
                }
            };
        }
    }
}