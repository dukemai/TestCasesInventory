using System.Collections.Generic;
using System.Web.Mvc;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;
using System.Linq;


namespace TestCasesInventory.Controllers
{
    public class FilterController : TestCasesInventory.Web.Common.Base.ControllerBase
    {
        public ActionResult FilterForTestSuite([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<FilterOptionViewModel>();
            filterFields.Add(new FilterOptionViewModel
            {
                Name = "Title",
                DisplayName = "Title",
                IsChecked = filterOptions.FilterFields.Length > 0 ? filterOptions.FilterFields.FirstOrDefault(f => f == "Title") != null : true
            });
            //filterFields.Add(new KeyValuePair<string, string>("Team", "Team"));
            var viewModel = new FilterViewModel
            {
                Controller = "TestSuite",
                Action = "Index",
                Area = "Admin",
                FilterFields = filterFields,
                FilterOptions = filterOptions
            };
            return PartialView("~/Views/Shared/Filter/_FilterPartialView.cshtml", viewModel);
        }
    }
}