using System;
using System.Web.Mvc;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Web.Common;
using System.IO;
using System.Web;
using TestCasesInventory.Web.Common.Utils;
using System.Linq;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(PrivilegedUsersConfig.AdminRole, PrivilegedUsersConfig.TesterRole)]
    public class TestCaseController : Controller
    {
        #region Properties
        private ITestCasePresenter testCasePresenterObject;
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
        private IFileControlPresenter fileControlPresenterObject;
        protected IFileControlPresenter FileControlPresenterObject
        {
            get
            {
                if (fileControlPresenterObject == null)
                {
                    fileControlPresenterObject = new FileControlPresenter();
                }
                return fileControlPresenterObject;
            }
        }


        #endregion


        // GET: Admin/TestCase
        public ActionResult Index(int? testSuiteID)
        {
            try
            {
                var testCases = TestCasePresenterObject.ListAll(testSuiteID);
                ViewBag.TestSuiteID = testSuiteID;
                return View("Index", testCases);
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // GET: Admin/TestCase/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var testCase = TestCasePresenterObject.GetTestCaseById(id);
                testCase.AttachmentUrl = FileControlPresenterObject.GetFileUrl(id);
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
        public ActionResult Create(int? testSuiteID, string testSuiteTitle)
        {
            ViewBag.TestSuiteID = testSuiteID;
            ViewBag.TestSuiteTitle = testSuiteTitle;
            return View();
        }

        // POST: Admin/TestCase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int testSuiteID, [Bind(Include = "Title, Description, Precondition, Expect")] TestCaseViewModel testCase, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var createdTestCase = new CreateTestCaseViewModel
                {
                    Title = testCase.Title,
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
                if (!(file == null || file.ContentLength == 0))
                {
                    var testCaseId = createdTestCase.ID.ToString();
                    FileControlPresenterObject.UploadFile(file, testCaseId);
                }
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
        public ActionResult Edit(int id, int testSuiteID, [Bind(Include = "Title, Description, Precondition, Expect, TestSuitID")] TestCaseViewModel testCase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedTestCase = new EditTestCaseViewModel
                    {
                        Title = testCase.Title,
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

    }
}
