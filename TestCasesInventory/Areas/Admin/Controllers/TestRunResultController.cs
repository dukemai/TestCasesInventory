using PagedList;
using System;
using System.Web.Mvc;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Web.Common;
using Microsoft.AspNet.Identity;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    public class TestRunResultController :  Web.Common.Base.ControllerBase
    {
        #region Properties
        private ITestRunResultPresenter testRunResultPresenterObject;
        protected ITestRunResultPresenter TestRunResultPresenterObject
        {
            get
            {
                if (testRunResultPresenterObject == null)
                {
                    testRunResultPresenterObject = new TestRunResultPresenter(HttpContext);
                }
                return testRunResultPresenterObject;
            }
        }
        #endregion
        // GET: Admin/TestRunResult
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var testRunResults = TestRunResultPresenterObject.GetTestRunResults(filterOptions);
            return View("Index", testRunResults);
        }
    }
}