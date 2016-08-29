using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Common;
using System.Linq;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasePresenter : PresenterBase, ITestCasePresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestCaseRepository testCaseRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ApplicationUserManager UserManager;


        public TestCasePresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCaseRepository = new TestCaseRepository();
            testSuiteRepository = new TestSuiteRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public TestCaseViewModel GetTestCaseById(int? id)
        {
            if (!id.HasValue)
            {
                logger.Error("TestCase Id was not valid.");
                throw new Exception("TestCase Id was not valid.");
            }
            var testCase = testCaseRepository.GetTestCaseByID(id.Value);
            if (testCase == null)
            {
                logger.Error("TestCase was not found.");
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testCase.TestSuiteID);
            if (testSuite == null)
            {
                logger.Error("Test Suite was not found.");
                throw new TestSuiteNotFoundException("Test Suite was not found.");
            }
            var testCaseViewModel = testCase.MapTo<TestCaseDataModel, TestCaseViewModel>();
            return testCaseViewModel;
        }

        public void InsertTestCase(CreateTestCaseViewModel testCase)
        {

            var testCaseDataModel = testCase.MapTo<CreateTestCaseViewModel, TestCaseDataModel>();
            testCaseRepository.InsertTestCase(testCaseDataModel);
            testCaseRepository.Save();
            testCase.ID = testCaseDataModel.ID;
        }

        public void UpdateTestCase(int id, EditTestCaseViewModel testCase)
        {

            var testCaseDataModel = testCaseRepository.GetTestCaseByID(id);
            if (testCaseDataModel == null)
            {
                logger.Error("TestCase was not found.");
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseDataModel = testCase.MapTo<EditTestCaseViewModel, TestCaseDataModel>(testCaseDataModel);
            };
            testCaseRepository.UpdateTestCase(testCaseDataModel);
            testCaseRepository.Save();
        }

        public void DeleteTestCase(int id)
        {
            var testCaseDataModel = testCaseRepository.GetTestCaseByID(id);
            if (testCaseDataModel == null)
            {
                logger.Error("TestCase was not found.");
                throw new TestCaseNotFoundException("TestCase was not found.");
            }
            else
            {
                testCaseRepository.DeleteTestCase(id);
                testCaseRepository.Save();
            }
        }

        public IPagedList<TestCaseViewModel> GetTestCasesForTestSuite(int testSuiteId, FilterOptions filterOptions)
        {
            var list = testCaseRepository.GetTestCasesForTestSuite(testSuiteId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCaseDataModel>, IPagedList<TestCaseViewModel>>();
            return mappedList;
        }

        public CreateTestCaseViewModel GetTestCaseForCreate(int testSuiteId)
        {
            var testSuite = testSuiteRepository.GetTestSuiteByID(testSuiteId);
            if (testSuite == null)
            {
                throw new TestSuiteNotFoundException();
            }
            var model = new CreateTestCaseViewModel();
            model.TestSuiteID = testSuiteId;
            model.TestSuiteTitle = testSuite.Title;
            model.Priorities = GetPriorities();
            return model;
        }

        public EditTestCaseViewModel GetTestCaseForEdit(int id)
        {
            var dataModel = testCaseRepository.GetTestCaseByID(id);
            var model = dataModel.MapTo<TestCaseDataModel, EditTestCaseViewModel>();
            model.Priorities = GetPriorities();
            return model;
        }

        private List<SelectListItem> GetPriorities()
        {
            var items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Highest", Value = "Highest" });
            items.Add(new SelectListItem { Text = "High", Value = "High" });
            items.Add(new SelectListItem { Text = "Medium", Value = "Medium", Selected = true });
            items.Add(new SelectListItem { Text = "Low", Value = "Low" });
            items.Add(new SelectListItem { Text = "Lowest", Value = "Lowest" });
            return items;
        }

        public string BuildTestCaseAttachmentUrl(string fileName, int testCaseId)
        {
            return TestCasesInventory.Common.UrlHelper.Combine(TestCaseConfigurations.TestCasesFolderPath, testCaseId.ToString(), fileName);
        }


        public List<string> GetTestCaseAttachments(int? testCaseId)
        {
            if (!testCaseId.HasValue)
            {
                logger.Error("TestCase Id was not valid.");
                throw new Exception("TestCase Id was not valid.");
            }
            var testCaseDirectory = TestCasesInventory.Common.UrlHelper.Combine(TestCaseConfigurations.TestCasesFolderPath, testCaseId.Value.ToString());
            return PathHelper.GetFileRelativeUrlsFromRelativeUrlDirectory(testCaseDirectory, HttpContext.Server);
        }
    }
}
