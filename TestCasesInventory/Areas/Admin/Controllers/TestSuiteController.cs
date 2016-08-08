using PagedList;
using System;
using System.Web.Mvc;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Web.Common;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(PrivilegedUsersConfig.TesterRole, PrivilegedUsersConfig.AdminRole)]
    
    public class TestSuiteController : Controller
    {
        #region Properties
        private ITestSuitePresenter testSuitePresenterObject;
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
        #endregion


        // GET: Admin/TestSuite
        public ActionResult Index(string valueToSearch, string searchBy, int? page, string sortBy)
        {
            var testSuites = TestSuitePresenterObject.ListAll();
            if (!String.IsNullOrEmpty(valueToSearch))
            {
                testSuites = TestSuitePresenterObject.GetTestSuitesBeSearched(valueToSearch.Trim(), searchBy.Trim());
            }
            SetViewBagToSort(sortBy);
            testSuites = TestSuitePresenterObject.GetTestSuitesBeSorted(testSuites, sortBy);
            return View("Index", testSuites.ToPagedList(page ?? PagingConfig.PageNumber, PagingConfig.PageSize));            
        }

        // GET: Admin/TestSuite/Details/5
        public ActionResult Details(int? id)
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
                return RedirectToAction("Create", "TestCase", new { testSuiteID = id, testSuiteTitle = testSuite.Title });
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

        private void SetViewBagToSort(string sortBy)
        {
            ViewBag.SortByTitle = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortByTeamName = sortBy == "TeanName asc" ? "TeanName desc" : "TeanName asc";
        }
    }
}
