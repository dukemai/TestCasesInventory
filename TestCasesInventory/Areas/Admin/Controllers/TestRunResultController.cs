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
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var testRunResults = TestRunResultPresenterObject.GetTestRunResults(filterOptions);
            return View("Index", testRunResults);
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "TestRunID, TestRunOption")] CreateTestRunResultViewModel testRunResult)
        {
            if (ModelState.IsValid)
            {
                var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
                testRunResult.CreatedDate = testRunResult.LastModifiedDate = DateTime.Now;
                testRunResult.Created = testRunResult.LastModified = user.Email;
                testRunResult.Status = TestRunResultStatus.InProgress;
                TestRunResultPresenterObject.InsertTestRunResult(testRunResult);
            }
            return RedirectToAction("Index"); 
        }
        [HttpGet]
        public ActionResult GetTestCasesAssignedToMe(int testRunId)
        {
            var listTestCasesAssignedToMe = TestRunResultPresenterObject.GetTestCasesAssignedToMe(testRunId);
            return Json(listTestCasesAssignedToMe, JsonRequestBehavior.AllowGet);
        }
       

        [HttpGet]
        public ActionResult GetAllTestCases(int testRunId)
        {
            var listAllTestCases = TestRunResultPresenterObject.GetAllTestCases(testRunId);
            return Json(listAllTestCases, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSelectedTestCases(int testRunId, List<int> selectedTestCases)
        {
            var listSelectedTestCases = TestRunResultPresenterObject.GetSelectedTestCases(testRunId, selectedTestCases);
            return Json(listSelectedTestCases, JsonRequestBehavior.AllowGet);
        }


    }
}