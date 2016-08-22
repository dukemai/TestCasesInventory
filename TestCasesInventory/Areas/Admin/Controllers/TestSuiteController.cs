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
            if (User.IsInRole(PrivilegedUsersConfig.AdminRole))
            {
                var teams = TeamPresenter.ListAllTeam().Select(t => new SelectListItem { Text = t.Name, Value = t.ID.ToString() }).ToList();
                return PartialView("_CreateTestSuiteByAdminPartial", new TestSuiteViewModel { Teams = teams });
            }
            var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
            if (user.TeamID.HasValue)
            {
                return PartialView("_CreateTestSuiteByTesterPartial");
            }
            else
            {
                throw new Exception("Current user is missing TeamID");
            }
        }

        // POST: Admin/TestSuite/CreateByTester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Description, TeamID")] TestSuiteViewModel testSuite)
        {
            if (ModelState.IsValid)
            {
                var user = UserPresenter.FindUserByID(User.Identity.GetUserId());

                var createdTestSuite = new CreateTestSuiteViewModel
                {
                    Title = testSuite.Title,
                    Description = testSuite.Description,
                    Created = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    LastModified = User.Identity.Name,
                    LastModifiedDate = DateTime.Now,
                    TeamID = testSuite.TeamID
                };
                if (User.IsInRole(PrivilegedUsersConfig.TesterRole))
                {
                    createdTestSuite.TeamID = user.TeamID;
                }
                TestSuitePresenterObject.InsertTestSuite(createdTestSuite);
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
            var updatedTestSuite = TestSuitePresenterObject.GetTestSuiteById(id);
            if (User.IsInRole(PrivilegedUsersConfig.AdminRole))
            {
                var teams = TeamPresenter.ListAllTeam().Select(t => new SelectListItem { Text = t.Name, Value = t.ID.ToString() }).ToList();
                updatedTestSuite.Teams = teams;
                return PartialView("_CreateTestSuiteByAdminPartial", updatedTestSuite);
            }
            var user = UserPresenter.FindUserByID(User.Identity.GetUserId());
            if (user.TeamID.HasValue)
            {
                return PartialView("_CreateTestSuiteByTesterPartial", updatedTestSuite);
            }
            else
            {
                throw new Exception("Current user is missing TeamID");
            }
        }

        // POST: Admin/TestSuite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID, Title, Description, TeamID")] TestSuiteViewModel testSuite)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedTestSuite = new EditTestSuiteViewModel
                    {
                        Title = testSuite.Title,
                        Description = testSuite.Description,
                        LastModified = User.Identity.Name,
                        LastModifiedDate = DateTime.Now,                        
                    };
                    TestSuitePresenterObject.UpdateTestSuite(id, updatedTestSuite);
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
