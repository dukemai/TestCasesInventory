using System;
using System.Web.Mvc;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Web.Common;
using PagedList;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(PrivilegedUsersConfig.AdminRole, PrivilegedUsersConfig.TesterRole)]
    public class TestCaseController : Controller
    {
        #region Properties
        private ITestCasePresenter testCasePresenterObject;
        private ITestSuitePresenter testSuitePresenterObject;
        protected ITestCasePresenter TestCasePresenterObject
        {
            get
            {
                if (testCasePresenterObject == null)
                {
                    testCasePresenterObject = new TestCasePresenter();
                }
                return testCasePresenterObject;
            }
        }
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


        // GET: Admin/TestCase
        public ActionResult Index(int? testSuiteID, string searchByTitle, int? page, string sortBy)
        {
            try
            {
                var testCases = TestCasePresenterObject.ListAll(testSuiteID);
                ViewBag.TestSuiteID = testSuiteID;
                if (!String.IsNullOrEmpty(searchByTitle))
                {
                    testCases = TestCasePresenterObject.GetTestCasesBeSearchedByName(testSuiteID, searchByTitle.Trim());
                }
                SetViewBagToSort(sortBy);
                testCases = TestCasePresenterObject.GetTestCasesBeSorted(testCases, sortBy);
                return View("Index", testCases.ToPagedList(page ?? PagingConfig.PageNumber, PagingConfig.PageSize));
            }
            catch(Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // GET: Admin/TestCase/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                var testCase = TestCasePresenterObject.GetTestCaseById(id);
                return View("Details", testCase);
            }
            catch (TestCaseNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }


        // GET: Admin/TestCase/Create?
        public ActionResult Create(int? testSuiteID)
        {
            try
            {
                ViewBag.TestSuiteID = testSuiteID;
                ViewBag.TestSuiteTitle = TestSuitePresenterObject.GetTestSuiteById(testSuiteID).Title;
                return View();
            }
            catch(TestSuiteNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/TestCase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int testSuiteID, [Bind(Include = "Title, Priority, Description, Precondition, Expect")] TestCaseViewModel testCase)
        {
            if (ModelState.IsValid)
            {
                var createdTestCase = new CreateTestCaseViewModel
                {
                    Title = testCase.Title,
                    Priority = testCase.Priority,
                    TestSuiteID = testSuiteID,
                    Description = testCase.Description,
                    Precondition = testCase.Precondition,
                    Expect = testCase.Expect,
                    Created = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    LastModified = User.Identity.Name,
                    LastModifiedDate = DateTime.Now
                };
                TestCasePresenterObject.InsertTestCase(createdTestCase);
                return RedirectToAction("Details", "TestSuite", new { id = testSuiteID });
            }
            return View();
        }

        // GET: Admin/TestCase/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                var updatedTestCase = TestCasePresenterObject.GetTestCaseById(id);
                return View("Edit", updatedTestCase);
            }
            catch (TestCaseNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/TestCase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int testSuiteID, [Bind(Include = "Title, Priority, Description, Precondition, Expect, TestSuitID")] TestCaseViewModel testCase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedTestCase = new EditTestCaseViewModel
                    {
                        Title = testCase.Title,
                        Priority = testCase.Priority,
                        Description = testCase.Description,
                        Precondition = testCase.Precondition,
                        Expect = testCase.Expect,
                        LastModified = User.Identity.Name,
                        LastModifiedDate = DateTime.Now
                    };
                    TestCasePresenterObject.UpdateTestCase(id, updatedTestCase);
                    return RedirectToAction("Details", "TestSuite", new { id = testSuiteID });
                }
                return View();
            }
            catch (TestCaseNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        // GET: Admin/TestCase/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var deletedTestCase = TestCasePresenterObject.GetTestCaseById(id);
                return View("Delete", deletedTestCase);
            }
            catch (TestCaseNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/TestCase/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int testSuiteID)
        {
            try
            {
                TestCasePresenterObject.DeleteTestCase(id);
                return RedirectToAction("Details", "TestSuite", new { id = testSuiteID });
            }
            catch (TestCaseNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        private void SetViewBagToSort(string sortBy)
        {
            ViewBag.SortByTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : "";
        }
    }
}
