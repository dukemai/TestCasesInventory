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
    
    public class TestRunController : TestCasesInventory.Web.Common.Base.ControllerBase
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

        public ActionResult CreateTestRunPartial()
        {
            CreateTestRunViewModel model = null;
            var user = UserPresenter.FindUserByID(User.Identity.GetUserId());

            if (IsCurrentUserAdmin())
            {
                model = TestRunPresenterObject.GetTestRunForAdminCreate(user.TeamID);
                return PartialView("TestRun/_CreateTestRunByAdminPartial", model);
            }

            if (user.TeamID.HasValue)
            {
                model = TestRunPresenterObject.GetTestRunForCreate();
                return PartialView("TestRun/_CreateTestRunByTesterPartial", model);
            }
            else
            {
                throw new Exception("Current user is missing TeamID");
            }
        }

        // POST: Admin/TestRun/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Description, TeamID")] CreateTestRunViewModel testRun)
        {
            if (ModelState.IsValid)
            {
                var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
                if (!IsCurrentUserAdmin())
                {
                    testRun.TeamID = user.TeamID;
                }
                testRun.CreatedDate = testRun.LastModifiedDate = DateTime.Now;
                testRun.Created = testRun.LastModified = user.Email;
                TestRunPresenterObject.InsertTestRun(testRun);
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
            catch (TestRunNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        public ActionResult EditTestRunPartial(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("ID must not be null");
            }
            EditTestRunViewModel updatedTestRun = null;

            var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
            if (IsCurrentUserAdmin())
            {
                updatedTestRun = TestRunPresenterObject.GetTestRunForAdminEdit(id.Value);
                return PartialView("TestRun/_EditTestRunByAdminPartial", updatedTestRun);
            }

            if (user.TeamID.HasValue)
            {
                updatedTestRun = TestRunPresenterObject.GetTestRunForEdit(id.Value);
                return PartialView("TestRun/_EditTestRunByTesterPartial", updatedTestRun);
            }
            else
            {
                throw new Exception("Current user is missing TeamID");
            }
        }

        // POST: Admin/TestRun/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID, Title, Description, TeamID")] EditTestRunViewModel testRun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
                    if (user == null)
                    {
                        throw new Exception("Could not identify current user");
                    }
                    testRun.LastModifiedDate = DateTime.Now;
                    testRun.LastModified = user.Email;
                    TestRunPresenterObject.UpdateTestRun(id, testRun);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (TestRunNotFoundException e)
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
            catch (TestRunNotFoundException e)
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
        public ActionResult Run(int id)
        {
            var model = new CreateTestRunResultViewModel();
            model.TestRunID = id;
            return PartialView("ChooseTestRunOptionPartial", model);
        }
              
    }
}