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
using TestCasesInventory.Presenter.Common;
using System.Collections.Generic;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    public class TestRunResultController : Web.Common.Base.ControllerBase
    {
        #region Properties
        private IUserPresenter userPresenter;
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
        protected IUserPresenter UserPresenter
        {
            get
            {
                if (userPresenter == null)
                {
                    userPresenter = new UserPresenter(HttpContext);
                }
                return userPresenter;
            }
        }
        #endregion
        // GET: Admin/TestRunResult
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions, int testRunID)
        {
            var testRunResults = TestRunResultPresenterObject.GetTestRunResults(filterOptions, testRunID);
            return View("Index", testRunResults);
        }


        [HttpPost]
        public ActionResult Create(int? testRunID)
        {
            if (!testRunID.HasValue)
            {
                throw new Exception("Id was not valid");
            }
            var testRunResultID = TestRunResultPresenterObject.CreateTestRunResult(testRunID.Value); ;
            return Json(new { success = true, testRunResultID = testRunResultID });
        }

        [HttpGet]
        public ActionResult GetTestCasesAssignedToMe(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var listTestCasesAssignedToMe = TestRunResultPresenterObject.GetTestCasesAssignedToMe(id.Value);
                return Json(listTestCasesAssignedToMe, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }


        [HttpGet]
        public ActionResult GetAllTestCases(int? id)
        {
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var listAllTestCases = TestRunResultPresenterObject.GetAllTestCases(id.Value);
                return Json(listAllTestCases, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSelectedTestCases(int? id, List<int> selectedTestCases)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var listSelectedTestCases = TestRunResultPresenterObject.GetSelectedTestCases(id.Value, selectedTestCases);
                return Json(listSelectedTestCases, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }

        }

        [HttpGet]
        public ActionResult GetTestRunResultInProgress(int? testRunResultId)
        {
            try
            {
                if (!testRunResultId.HasValue)
                {
                    throw new Exception("TestRunResultId was not valid");
                }
                var listTestRunResultInProgressViewModel = TestRunResultPresenterObject.GetTestRunResultInProgress(testRunResultId.Value);
                return Json(listTestRunResultInProgressViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                 return View("ResultNotFoundError");
            }
            
        }

        public ActionResult FinishTestRunResult(int testRunResultID)
        {
            return Json("Done");
        }
    }
}
