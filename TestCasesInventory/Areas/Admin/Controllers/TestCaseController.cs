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
using TestCasesInventory.Common;
using TestCasesInventory.Bindings;
using System.Collections.Generic;

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
                    testCasePresenterObject = new TestCasePresenter(HttpContext);
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
        public ActionResult Index(int? testSuiteID, [ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            try
            {
                if (testSuiteID.HasValue)
                {
                    var testCases = TestCasePresenterObject.GetTestCasesForTestSuite(testSuiteID.Value, filterOptions);
                    return View("Index", testCases);
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

        // GET: Admin/TestCase/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var testCase = TestCasePresenterObject.GetTestCaseById(id);
                if (FileControlPresenterObject.IsAttachmentExisted(id))
                {
                    testCase.IsAttachmentUrlExisted = true;
                    testCase.AttachmentUrl = FileControlPresenterObject.GetFileUrl(id);
                    testCase.AttachmentUrlList = FileControlPresenterObject.GetFileUrlList(id);
                }

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
                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = "Highest", Value = "Highest" });
                items.Add(new SelectListItem { Text = "High", Value = "High" });
                items.Add(new SelectListItem { Text = "Medium", Value = "Medium", Selected = true });
                items.Add(new SelectListItem { Text = "Low", Value = "Low" });
                items.Add(new SelectListItem { Text = "Lowest", Value = "Lowest" });
                ViewBag.PriorityList = items;

                ViewBag.TestSuiteID = testSuiteID;
                ViewBag.TestSuiteTitle = TestSuitePresenterObject.GetTestSuiteById(testSuiteID).Title;
                return View();
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
        public ActionResult Edit(int id)
        {
            try
            {
                var updatedTestCase = TestCasePresenterObject.GetTestCaseById(id);
                if (FileControlPresenterObject.IsAttachmentExisted(id))
                {
                    updatedTestCase.IsAttachmentUrlExisted = true;
                    updatedTestCase.AttachmentUrl = FileControlPresenterObject.GetFileUrl(id);
                    updatedTestCase.AttachmentUrlList = FileControlPresenterObject.GetFileUrlList(id);
                    updatedTestCase.AttachmentNameList = FileControlPresenterObject.GetFileNameList(updatedTestCase.AttachmentUrlList);
                }
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
        public ActionResult Edit(int id, int testSuiteID, [Bind(Include = "Title, Priority, Description, Precondition, Expect, TestSuitID")] TestCaseViewModel testCase, HttpPostedFileBase file)
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
                    if (!(file == null || file.ContentLength == 0))
                    {
                        FileControlPresenterObject.UploadFile(file, id.ToString());
                    }
                    return RedirectToAction("Edit", "TestCase", new { id = id });
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

        //public ActionResult DeleteFile(int id)
        public ActionResult DeleteFile(int id, string item)
        {
            FileControlPresenterObject.DeleteFile(item);
            return RedirectToAction("Edit", "TestCase", new { id = id });
        }

        private void SetViewBagToSort(string sortBy)
        {
            ViewBag.SortByTitle = String.IsNullOrEmpty(sortBy) ? "Title desc" : "";
        }
    }
}
