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
    [CustomAuthorize(PrivilegedUsersConfig.TesterRole, PrivilegedUsersConfig.AdminRole)]
    
    public class TestRunController : Controller
    {
        #region Properties
        private IUserPresenter userPresenter;
        private ITestRunPresenter testRunPresenterObject;
        private IRolePresenter rolePresenter;
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
        protected IRolePresenter RolePresenter
        {
            get
            {
                if (rolePresenter == null)
                {
                    rolePresenter = new RolePresenter(HttpContext);
                }
                return rolePresenter;
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
        // GET: Admin/TestRun
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            //var searchOptions = BuildFilterOptionsFromRequest(keyword, filterBy, page, sortBy, sortDirection);
            var userId = User.Identity.GetUserId();
            var testRuns = TestRunPresenterObject.GetTestRuns(filterOptions, userId);
            return View("Index", testRuns);
        }

        // GET: Admin/TestRun/Details/5
        public ActionResult Details(int? id, [ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            try
            {
                var testRun = TestRunPresenterObject.GetTestRunById(id);
                return View("Details", testRun);
            }
            catch (TestRunNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // GET: Admin/TestRun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TestRun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Description")] TestRunViewModel testRun)
        {
            if (ModelState.IsValid)
            {
                var createdTestRun = new CreateTestRunViewModel
                {
                    Title = testRun.Title,
                    Description = testRun.Description,
                    Created = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    LastModified = User.Identity.Name,
                    LastModifiedDate = DateTime.Now
                };
                TestRunPresenterObject.InsertTestRun(createdTestRun);
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Admin/TestRun/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                var updatedTestRun = TestRunPresenterObject.GetTestRunById(id);
                return View("Edit", updatedTestRun);
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

        // POST: Admin/TestRun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID, Title, Description")] TestRunViewModel testRun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedTestRun = new EditTestRunViewModel
                    {
                        Title = testRun.Title,
                        Description = testRun.Description,
                        LastModified = User.Identity.Name,
                        LastModifiedDate = DateTime.Now
                    };
                    TestRunPresenterObject.UpdateTestRun(id, updatedTestRun);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (TestSuiteNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }
        // GET: Admin/TestRun/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var deletedTestRun = TestRunPresenterObject.GetTestRunById(id);
                return View("Delete", deletedTestRun);
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

        // POST: Admin/TestRun/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                TestRunPresenterObject.DeleteTestRun(id);
                return RedirectToAction("Index");
            }
            catch (TestRunNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }
        //[HttpGet]
        //public ActionResult AddTestCase(int id)
        //{
        //    try
        //    {
        //        var testRun = TestRunPresenterObject.GetTestRunById(id);
        //        return RedirectToAction("Create", "TestCase", new { testRunID = id });
        //    }
        //    catch (TestSuiteNotFoundException e)
        //    {
        //        return View("ResultNotFoundError");
        //    }
        //    catch (Exception e)
        //    {
        //        return View("ResultNotFoundError");
        //    }
        //}
    }
}