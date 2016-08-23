using PagedList;
using System;
using System.Web.Mvc;
using System.Linq;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Web.Common;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using TestCasesInventory.Presenter.Synchroniser;
using TestCasesInventory.Data.Repositories;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(PrivilegedUsersConfig.TesterRole, PrivilegedUsersConfig.AdminRole)]

    public class TestSuiteController : TestCasesInventory.Web.Common.Base.ControllerBase
    {
        #region Properties
        private IUserPresenter userPresenter;
        private ITestSuitePresenter testSuitePresenterObject;
        private IRolePresenter rolePresenter;
        private ITeamPresenter teamPresenter;

        protected ITeamPresenter TeamPresenter
        {
            get
            {
                if (teamPresenter == null)
                {
                    teamPresenter = new TeamPresenter(HttpContext);
                }
                return teamPresenter;
            }
        }
        protected ITestSuitePresenter TestSuitePresenterObject
        {
            get
            {
                if (testSuitePresenterObject == null)
                {
                    testSuitePresenterObject = new TestSuitePresenter(HttpContext);
                    testSuitePresenterObject.Subscribe(new TestSuiteObserver());
                }
                return testSuitePresenterObject;
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

        #region Constructors

        public TestSuiteController()
        {
        }

        #endregion

        #region Actions

        // GET: Admin/TestSuite
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            //var searchOptions = BuildFilterOptionsFromRequest(keyword, filterBy, page, sortBy, sortDirection);
            var userId = User.Identity.GetUserId();
            var testSuites = TestSuitePresenterObject.GetTestSuites(filterOptions, userId);
            return View("Index", testSuites);
        }

        // GET: Admin/TestSuite/Details/5
        public ActionResult Details(int? id, [ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            try
            {
                var testSuite = TestSuitePresenterObject.GetTestSuiteById(id);
                return View("Details", testSuite);
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

        // GET: Admin/TestSuite/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateTestSuitePartial()
        {
            CreateTestSuiteViewModel model = null;
            if (User.IsInRole(PrivilegedUsersConfig.AdminRole))
            {
                model = TestSuitePresenterObject.GetTestSuiteForAdminCreate();
                return PartialView("_CreateTestSuiteByAdminPartial", model);
            }

            var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
            if (user.TeamID.HasValue)
            {
                model = TestSuitePresenterObject.GetTestSuiteForCreate();
                return PartialView("_CreateTestSuiteByTesterPartial", model);
            }
            else
            {
                throw new Exception("Current user is missing TeamID");
            }
        }

        // POST: Admin/TestSuite/CreateByTester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Description, TeamID")] CreateTestSuiteViewModel testSuite)
        {
            if (ModelState.IsValid)
            {
                var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
                if (User.IsInRole(PrivilegedUsersConfig.TesterRole))
                {
                    testSuite.TeamID = user.TeamID;
                }
                testSuite.CreatedDate = testSuite.LastModifiedDate = DateTime.Now;
                testSuite.Created = testSuite.LastModified = user.Email;
                TestSuitePresenterObject.InsertTestSuite(testSuite);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/TestSuite/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                var updatedTestSuite = TestSuitePresenterObject.GetTestSuiteById(id);
                return View("Edit", updatedTestSuite);
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

        public ActionResult EditTestSuitePartial(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("ID must not be null");
            }
            EditTestSuiteViewModel updatedTestSuite = null;
            if (User.IsInRole(PrivilegedUsersConfig.AdminRole))
            {
                updatedTestSuite = TestSuitePresenterObject.GetTestSuiteForAdminEdit(id.Value);
                return PartialView("_EditTestSuiteByAdminPartial", updatedTestSuite);
            }
            var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
            if (user.TeamID.HasValue)
            {
                updatedTestSuite = TestSuitePresenterObject.GetTestSuiteForEdit(id.Value);
                return PartialView("_EditTestSuiteByTesterPartial", updatedTestSuite);
            }
            else
            {
                throw new Exception("Current user is missing TeamID");
            }
        }

        // POST: Admin/TestSuite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID, Title, Description, TeamID")] EditTestSuiteViewModel testSuite)
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
                    testSuite.LastModifiedDate = DateTime.Now;
                    testSuite.LastModified = user.Email;
                    TestSuitePresenterObject.UpdateTestSuite(id, testSuite);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (TestSuiteNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        // GET: Admin/TestSuite/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var deletedTestSuite = TestSuitePresenterObject.GetTestSuiteById(id);
                return View("Delete", deletedTestSuite);
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

        // POST: Admin/TestSuite/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                TestSuitePresenterObject.DeleteTestSuite(id);
                return RedirectToAction("Index");
            }
            catch (TestSuiteNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        [HttpGet]
        public ActionResult AddTestCase(int id)
        {
            try
            {
                var testSuite = TestSuitePresenterObject.GetTestSuiteById(id);
                return RedirectToAction("Create", "TestCase", new { testSuiteID = id });
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

        #endregion

    }
}
