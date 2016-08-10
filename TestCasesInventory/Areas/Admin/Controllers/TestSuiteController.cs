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

    public class TestSuiteController : Controller
    {
        #region Properties
        private IUserPresenter userPresenter;
        private ITestSuitePresenter testSuitePresenterObject;
        private IRolePresenter rolePresenter;
        protected ITestSuitePresenter TestSuitePresenterObject
        {
            get
            {
                if (testSuitePresenterObject == null)
                {
                    testSuitePresenterObject = new TestSuitePresenter(HttpContext);
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


        // GET: Admin/TestSuite
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            //var searchOptions = BuildFilterOptionsFromRequest(keyword, filterBy, page, sortBy, sortDirection);
            var roles = UserPresenter.GetRolesForUser(User.Identity.GetUserId());
            var teamID = UserPresenter.FindUserByID(User.Identity.GetUserId()).TeamID;
            var testSuites = TestSuitePresenterObject.GetTestSuites(filterOptions, roles, teamID);
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

        // POST: Admin/TestSuite/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Description")] TestSuiteViewModel testSuite)
        {
            if (ModelState.IsValid)
            {
                var createdTestSuite = new CreateTestSuiteViewModel
                {
                    Title = testSuite.Title,
                    Description = testSuite.Description,
                    Created = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    LastModified = User.Identity.Name,
                    LastModifiedDate = DateTime.Now
                };
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

        // POST: Admin/TestSuite/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID, Title, Description")] TestSuiteViewModel testSuite)
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
                        LastModifiedDate = DateTime.Now
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

    }
}
