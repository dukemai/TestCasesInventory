using System.Collections.Generic;
using System.Web.Mvc;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Controllers
{
    public class FilterController : TestCasesInventory.Web.Common.Base.ControllerBase
    {
        public ActionResult FilterForTestSuite([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var filterFields = new List<KeyValuePair<string, string>>();
            filterFields.Add(new KeyValuePair<string, string>("Title", "Title"));
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