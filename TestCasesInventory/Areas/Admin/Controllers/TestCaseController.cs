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
    public class TestCaseController : TestCasesInventory.Web.Common.Base.ControllerBase
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
                    fileControlPresenterObject = new FileControlPresenter(HttpContext.Server);
                }
                return fileControlPresenterObject;
            }
        }


        #endregion

        #region Actions

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
        public ActionResult Details(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("TestCase Id was not valid.");
                }
                var testCase = TestCasePresenterObject.GetTestCaseById(id.Value);
                testCase.AttachmentUrlList = TestCasePresenterObject.GetTestCaseAttachments(id.Value);

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
                if (!testSuiteID.HasValue)
                {
                    throw new Exception("TestSuite ID must be specified");
                }

                var model = TestCasePresenterObject.GetTestCaseForCreate(testSuiteID.Value);
                //ViewBag.PriorityList = items;

                //ViewBag.TestSuiteID = testSuiteID;
                //ViewBag.TestSuiteTitle = TestSuitePresenterObject.GetTestSuiteById(testSuiteID).Title;
                //model.TestSuiteID = testSuiteID.Value;
                return View(model);
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
        public ActionResult Create(int testSuiteID, [Bind(Include = "Title, Description, Precondition, Expect, Priority")] CreateTestCaseViewModel testCase, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                testCase.Created = testCase.LastModified = User.Identity.Name;
                testCase.CreatedDate = testCase.LastModifiedDate = DateTime.Now;
                testCase.TestSuiteID = testSuiteID;
                TestCasePresenterObject.InsertTestCase(testCase);
                if (!(file == null || file.ContentLength == 0))
                {
                    var fileNameToUpload = TestCasePresenterObject.BuildTestCaseAttachmentUrl(file.FileName, testCase.ID);
                    FileControlPresenterObject.UploadRelativeUrlFile(file, fileNameToUpload);
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
                if (!id.HasValue)
                {
                    throw new Exception("TestCase Id was not valid.");
                }
                var updatedTestCase = TestCasePresenterObject.GetTestCaseById(id.Value);
                updatedTestCase.AttachmentUrlList = TestCasePresenterObject.GetTestCaseAttachments(id.Value);
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
        public ActionResult Edit(int id, int testSuiteID, [Bind(Include = "Title, Priority, Description, Precondition, Expect, TestSuitID")] EditTestCaseViewModel testCase, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    testCase.LastModified = User.Identity.Name;
                    testCase.LastModifiedDate = DateTime.Now;
                    TestCasePresenterObject.UpdateTestCase(id, testCase);
                    if (!(file == null || file.ContentLength == 0))
                    {
                        var fileNameToUpload = TestCasePresenterObject.BuildTestCaseAttachmentUrl(file.FileName, id);
                        FileControlPresenterObject.UploadRelativeUrlFile(file, fileNameToUpload);
                    }
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
                if (!id.HasValue)
                {
                    throw new Exception("TestCase Id was not valid.");
                }
                var deletedTestCase = TestCasePresenterObject.GetTestCaseById(id.Value);
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
            FileControlPresenterObject.DeleteRelativeUrlFile(item);
            return RedirectToAction("Edit", "TestCase", new { id = id });
        }

        #endregion
    }
}
