using Microsoft.AspNet.Identity;
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
using TestCasesInventory.Presenter.Validations;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(PrivilegedUsersConfig.AdminRole, PrivilegedUsersConfig.TesterRole)]
    public class TestCasesInTestRunController : Controller
    {
        #region Properties
        private ITestCasesInTestRunPresenter testCasesInTestRunPresenterObject;
        private IUserPresenter userPresenter;
        private ITeamPresenter teamPresenterObject;
        private ITestRunPresenter testRunPresenterObject;

        protected ITestRunPresenter TestRunPresenterObject
        {
            get
            {
                if (testRunPresenterObject == null)
                {
                    testRunPresenterObject = new TestRunPresenter(HttpContext);
                }
                return testRunPresenterObject;
            }
        }
        protected ITeamPresenter TeamPresenterObject
        {
            get
            {
                if (teamPresenterObject == null)
                {
                    teamPresenterObject = new TeamPresenter(HttpContext);
                }
                return teamPresenterObject;
            }
        }
        protected ITestCasesInTestRunPresenter TestCasesInTestRunPresenterObject
        {
            get
            {
                if (testCasesInTestRunPresenterObject == null)
                {
                    testCasesInTestRunPresenterObject = new TestCasesInTestRunPresenter(HttpContext);
                }
                return testCasesInTestRunPresenterObject;
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

        // GET: Admin/TestCasesInTestRun
        public ActionResult Index(int? testRunID, [ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            try
            {
                if (testRunID.HasValue)
                {
                    var testCasesInTestRun = TestCasesInTestRunPresenterObject.GetTestCasesByTestRunID(testRunID.Value, filterOptions);
                    return View("Index", testCasesInTestRun);
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpGet]
        public ActionResult GetTestSuitesPopUp(int id)
        {
            try
            {
                var testSuitesPopUp = TestCasesInTestRunPresenterObject.GetTestSuitesPopUp(id);
                return Json(testSuitesPopUp, JsonRequestBehavior.AllowGet);
            }
            catch (TestSuiteNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpGet]
        public ActionResult GetTestCasesInTestSuitePopUp(int testSuiteID, int testRunID)
        {
            try
            {
                var testCasesInTestSuitePopUp = TestCasesInTestRunPresenterObject.GetTestCasesInTestSuitePopUp(testSuiteID, testRunID);
                return Json(testCasesInTestSuitePopUp, JsonRequestBehavior.AllowGet);
            }
            catch (TestSuiteNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpPost]
        public ActionResult AddTestCasesToTestRun(List<int> testCases, int testRunID)
        {
            TestCasesInTestRunPresenterObject.AddTestCasesToTestRun(testCases, testRunID);
            return Json("Done");
        }

        [HttpPost]
        public ActionResult RemoveTestCasesFromTestRun(List<int> testCases, int testRunID)
        {
            TestCasesInTestRunPresenterObject.RemoveTestCasesFromTestRun(testCases, testRunID);
            return Json("Done");
        }
        public ActionResult RemoveASingleTestCaseFromTestRun(int? testCasesInTestRunID)
        {
            try
            {
                if (!testCasesInTestRunID.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var testCasesInTestRun = TestCasesInTestRunPresenterObject.GetTestCasesInTestRunById(testCasesInTestRunID.Value);
                var listTestCasesInTestRun = new List<int> { testCasesInTestRun.TestCaseID };
                TestCasesInTestRunPresenterObject.RemoveTestCasesFromTestRun(listTestCasesInTestRun, testCasesInTestRun.TestRunID);
                return RedirectToAction("Details", "TestRun", new { id = testCasesInTestRun.TestRunID });
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpGet]
        public ActionResult GetUsersPopUp(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var usersPopUp = TestCasesInTestRunPresenterObject.GetUsersPopUp(id.Value);
            return Json(usersPopUp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AssignToMe(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            TestCasesInTestRunPresenterObject.AssignTestCaseToMe(id.Value);
            return Json("Done.");
        }

        [HttpPost]
        public ActionResult AssignTestCaseToUser(int testCaseInTestRunID, string userID)
        {
            var userBeAssigned = UserPresenter.FindUserByID(userID);
            TestCasesInTestRunPresenterObject.AssignTestCaseToUser(testCaseInTestRunID, userBeAssigned.Email);
            return Json("Done");
        }




    }
}