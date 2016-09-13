using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    public class TestCaseResultController : Controller
    {

        #region Properties
        private ITestCaseResultPresenter testCaseResultPresenterObject;
        private ITestCasesInTestRunPresenter testCaseInTestRunPresenterObject;
        protected ITestCaseResultPresenter TestCaseResultPresenterObject
        {
            get
            {
                if (testCaseResultPresenterObject == null)
                {
                    testCaseResultPresenterObject = new TestCaseResultPresenter(HttpContext);
                }
                return testCaseResultPresenterObject;
            }
        }

        protected ITestCasesInTestRunPresenter TestCaseInTestRunPresenterObject
        {
            get
            {
                if (testCaseInTestRunPresenterObject == null)
                {
                    testCaseInTestRunPresenterObject = new TestCasesInTestRunPresenter(HttpContext);
                }
                return testCaseInTestRunPresenterObject;
            }
        }
        #endregion


        #region Funciton

        [HttpPost]
        public ActionResult CreateTestCaseResult(CreateTestCaseResultViewModel testCaseResult)
        {           
            var totalTested = TestCaseResultPresenterObject.InsertOrUpdateTestCaseResult(testCaseResult);
            return Json(new { success = true, totalTested = totalTested });
        }


        [HttpGet]
       public ActionResult GetTestCaseResult(int testCasesInTestRun, int testRunResult)
        {
            var testCaseResult = TestCaseResultPresenterObject.GetTestCaseResult(testCasesInTestRun, testRunResult);
            return Json(testCaseResult, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}