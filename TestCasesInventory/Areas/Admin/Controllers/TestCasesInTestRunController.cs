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
        
        [HttpPost]
        public ActionResult AddTestCasesToTestRun(List<int> testCases, int testRunID)
        {
            TestCasesInTestRunPresenterObject.AddTestCasesToTestRun(testCases, testRunID);
            return Json("Done");
        }

        [HttpPost]
        public ActionResult RemoeTestCasesFromTestRun(List<int> testCases, int testRunID)
        {
            TestCasesInTestRunPresenterObject.RemoveTestCasesFromTestRun(testCases, testRunID);
            return Json("Done");
        }

        [HttpGet]
        public ActionResult GetUsersPopUp(int id)
        {
            try
            {
                var usersPopUp = TestCasesInTestRunPresenterObject.GetUsersPopUp(id);
                return Json(usersPopUp, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        public ActionResult AssignToMe(int? id, int testRunID)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("Id is not valid");
                }
                TestCasesInTestRunPresenterObject.AssignTestCaseToMe(id.Value);
                return RedirectToAction("Details", "TestRun", new { id = testRunID });
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpPost]
        public ActionResult AssignTestCaseToUser(int testCaseInTestRunID, string userID)
        {
            try
            {
                var userBeAssigned = UserPresenter.FindUserByID(userID);
                TestCasesInTestRunPresenterObject.AssignTestCaseToUser(testCaseInTestRunID, userBeAssigned.Email);
                return Json("Done");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }




    }
}