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
using TestCasesInventory.Presenter.Business;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCaseResultPresenter : PresenterBase, ITestCaseResultPresenter
    {
        #region Properties

        protected HttpContextBase HttpContext;
        protected ApplicationUserManager UserManager;
        protected ApplicationSignInManager SignInManager;
        protected IPrincipal User;
        protected ITestCaseResultRepository testCaseResultRepository;
        protected ITestRunResultRepository testRunResultRepository;
        protected ITestCasesInTestRunRepository testCaseInTestRunRepository;
        protected ITestRunRepository testRunRepository;

        #endregion


        #region Method


        public TestCaseResultPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            User = HttpContext.User;
            testCaseResultRepository = new TestCaseResultRepository();
            testRunResultRepository = new TestRunResultRepository();
            testCaseInTestRunRepository = new TestCasesInTestRunRepository();
            testRunRepository = new TestRunRepository();
        }


        public TestCaseResultViewModel GetTestCaseResultById(int? testCaseResultID)
        {
            if (!testCaseResultID.HasValue)
            {
                logger.Error("TestCaseResultID was not valid.");
                throw new Exception("TestCaseResultID was not valid.");
            }
            var testCaseResult = testCaseResultRepository.GetTestCaseResultByID(testCaseResultID.Value);
            if (testCaseResult == null)
            {
                logger.Error("Test Case was not found.");
                throw new TestCaseNotFoundException("Test Case was not found.");
            }
            var testCaseResultViewModel = testCaseResult.MapTo<TestCaseResultDataModel, TestCaseResultViewModel>();
            return testCaseResultViewModel;
        }

        public TestCaseResultViewModel GetTestCaseResult(int testCasesInTestRunID, int testRunResultID)
        {
            var testCasesInTestRun = testCaseInTestRunRepository.GetTestCaseInTestRunByID(testCasesInTestRunID);
            if (testCasesInTestRun == null)
            {
                logger.Error("TestCasesInTestRunID was not valid");
                throw new Exception("TestCasesInTestRunID was not valid");
            }
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRunResultID was not valid");
                throw new Exception("TestRunResultID was not valid");
            }
            var testCaseResult = testCaseResultRepository.GetTestCaseResult(testCasesInTestRunID, testRunResultID);

            if (testCaseResult == null)
            {
                logger.Error("Test Case was not found.");
                throw new TestCaseNotFoundException("Test Case was not found.");
            }

            var testCaseResultViewModel = testCaseResult.MapTo<TestCaseResultDataModel, TestCaseResultViewModel>();
            return testCaseResultViewModel;
        }


        public IPagedList<TestCaseResultViewModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions filterOptions)
        {
            var list = testCaseResultRepository.GetTestCasesForTestSuite(testRunResultId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCaseResultDataModel>, IPagedList<TestCaseResultViewModel>>();
            return mappedList;
        }

        public bool InsertOrUpdateTestCaseResult(CreateTestCaseResultViewModel testCaseResult)
        {
            testCaseResult.RunBy = User.Identity.GetUserId();
            testCaseResult.Created = testCaseResult.LastModified = User.Identity.GetUserName();
            testCaseResult.CreatedDate = testCaseResult.LastModifiedDate = DateTime.Now;
            var testCasesInTestRun = testCaseInTestRunRepository.GetTestCaseInTestRunByID(testCaseResult.TestCasesInTestRunID);
            if (testCasesInTestRun == null)
            {
                logger.Error("TestCasesInTestRunID was not valid");
                throw new Exception("TestCasesInTestRunID was not valid");
            }
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testCaseResult.TestRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRunResultID was not valid");
                throw new Exception("TestRunResultID was not valid");
            }

            var testCaseResultDataModel = testCaseResultRepository.GetTestCaseResult(testCaseResult.TestCasesInTestRunID, testCaseResult.TestRunResultID);
            if (testCaseResultDataModel == null)
            {
                testCaseResultDataModel = testCaseResult.MapTo<CreateTestCaseResultViewModel, TestCaseResultDataModel>();
                testCaseResultRepository.InsertTestCaseResult(testCaseResultDataModel);
                testCaseResultRepository.Save();
            }
            else
            {
                var editTestCaseResult = testCaseResult.MapTo<CreateTestCaseResultViewModel, EditTestCaseResultViewModel>();
                testCaseResultDataModel = editTestCaseResult.MapTo<EditTestCaseResultViewModel, TestCaseResultDataModel>(testCaseResultDataModel);
                testCaseResultRepository.UpdateTestCaseResult(testCaseResultDataModel);
                testCaseResultRepository.Save();
            }

            var testedAll = CheckTestedAll(testRunResult);
            return testedAll;
        }

        private bool CheckTestedAll(TestRunResultDataModel testRunResult)
        {
            var totalFailed = testCaseResultRepository.TotalFailedTestCaseResults(testRunResult.ID);
            var totalPassed = testCaseResultRepository.TotalPassedTestCaseResults(testRunResult.ID);
            var totalTested = testCaseInTestRunRepository.TotalTestCasesInTestRun(testRunResult.TestRunID);
            return totalTested == (totalFailed + totalPassed);
        }

        #endregion
    }

}