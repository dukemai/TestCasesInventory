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

        public ActionResult Details(int? id)
        {
            try
            {
                var testCaseInTestRun = TestCasesInTestRunPresenterObject.GetTestCaseInTestRunById(id);
                return View("Details", testCaseInTestRun);
            }
            catch (TestCaseInTestRunNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        public ActionResult AssignToMe(int? id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                TestCasesInTestRunPresenterObject.AssignToMe(id, userId);
                return Json("Assigned", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

       

        public ActionResult Delete(int? id)
        {
            try
            {
                var deletedTestCaseInTestRun = TestCasesInTestRunPresenterObject.GetTestCaseInTestRunById(id);
                return View("Delete", deletedTestCaseInTestRun);
            }
            catch (TestCaseInTestRunNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int testRunID)
        {
            try
            {
                TestCasesInTestRunPresenterObject.DeleteTestCaseInTestRun(id);
                return RedirectToAction("Details", "TestRun", new { id = testRunID });
            }
            catch (TestCaseInTestRunNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        //public ActionResult DeleteFile(int id)
        //public ActionResult DeleteFile(int id, string item)
        //{
        //    FileControlPresenterObject.DeleteRelativeUrlFile(item);
        //    return RedirectToAction("Edit", "TestCase", new { id = id });
        //}
    }
}