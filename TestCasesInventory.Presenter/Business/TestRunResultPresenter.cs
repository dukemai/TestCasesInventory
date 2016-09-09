using System.Web.Mvc;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCasesInventory.Presenter.Business
{
    public class TestRunResultPresenter : ObservablePresenterBase<TestRunResultDataModel>, ITestRunResultPresenter
    {
        #region Properties
        protected HttpContextBase HttpContext;
        protected ITestRunResultRepository testRunResultRepository;
        protected ITestCaseResultRepository testCaseResultRepository;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected ITestRunRepository testRunRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ApplicationUserManager UserManager;
        protected RoleManager<IdentityRole> RoleManager;


        public TestRunResultPresenter(HttpContextBase context) : base()
        {
            HttpContext = context;
            testRunResultRepository = new TestRunResultRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            testRunRepository = new TestRunRepository();
            testSuiteRepository = new TestSuiteRepository();
            testCaseRepository = new TestCaseRepository();
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            testCaseResultRepository = new TestCaseResultRepository();
        }
        #endregion

        #region TestRunResult
        public TestRunResultViewModel GetTestRunResultById(int testRunResultID)
        {
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            IsValidTestRunResult(testRunResult);
            var testRunResultViewModel = testRunResult.MapTo<TestRunResultDataModel, TestRunResultViewModel>();
            return testRunResultViewModel;
        }

        public int CreateTestRunResult(int testRunID)
        {
            var user = UserManager.FindById(HttpContext.User.Identity.GetUserId());
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new TestRunNotFoundException("User was not found.");
            }
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunResultNotFoundException("Test Run was not found");
            }
            var testRunResultViewModel = new CreateTestRunResultViewModel
            {
                TestRunID = testRunID,
                Status = TestRunResultStatus.InProgress,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Created = user.Email,
                LastModified = user.Email,
            };
            var testRunResultDataModel = testRunResultViewModel.MapTo<CreateTestRunResultViewModel, TestRunResultDataModel>();
            testRunResultRepository.InsertTestRunResult(testRunResultDataModel);
            testRunResultRepository.Save();

            var testRunResultID = testRunResultDataModel.ID;
            return testRunResultID;
        }

        public void DeleteTestRunResult(int testRunResultID)
        {
            var testRunResultDataModel = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            IsValidTestRunResult(testRunResultDataModel);
            testRunResultRepository.DeleteTestRunResult(testRunResultID);
            testRunResultRepository.Save();
        }

        public IPagedList<TestRunResultViewModel> GetTestRunResults(FilterOptions options, int testRunID)
        {
            var list = testRunResultRepository.GetTestRunResults(options, testRunID);
            var mappedList = list.MapTo<IPagedList<TestRunResultDataModel>, IPagedList<TestRunResultViewModel>>();
            return mappedList;
        }

        public void FinishTestRunResult(int testRunResultId)
        {
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultId);
            if (IsValidTestRunResult(testRunResult))
            {
                testRunResult.Status = TestRunResultStatus.Finished;
                testRunResultRepository.UpdateTestRunResult(testRunResult);
                testRunResultRepository.Save();
            }
        }

        private bool IsValidTestRunResult(TestRunResultDataModel testRunResult)
        {
            if (testRunResult == null)
            {
                logger.Error("Test Run Result was not found.");
                return false;
            }
            var testRun = testRunRepository.GetTestRunByID(testRunResult.TestRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                return false;
            }
            return true;
        }
        #endregion

        #region Test Cases
        public IList<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToUser(int testRunID, string userName)
        {
            var user = UserManager.FindByEmail(userName);
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("User was not found.");
            }

            var listTestCasesInTestRunResult = new List<TestCasesInTestRunResultViewModel>();
            var listTestCasesInTestRunResultDataModel = testCasesInTestRunRepository.GetTestCasesInTestRunAssignedToUser(testRunID, user.Id);
            foreach (var testCaseInTestRun in listTestCasesInTestRunResultDataModel)
            {
                if (IsValidTestCaseInTestRun(testCaseInTestRun))
                {
                    var testCaseInTestRunView = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>();
                    listTestCasesInTestRunResult.Add(testCaseInTestRunView);
                }
            }
            return listTestCasesInTestRunResult;
        }
        public IList<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToMe(int testRunId)
        {
            var userName = UserManager.FindById(HttpContext.User.Identity.GetUserId()).Email;
            return GetTestCasesAssignedToUser(testRunId, userName);
        }

        public IList<TestCasesInTestRunResultViewModel> GetAllTestCases(int testRunResultID)
        {
            var listTestCases = new List<TestCasesInTestRunResultViewModel>();
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRun Result with id" + testRunResultID + "was not found.");
            }
            if (testCaseResultRepository.TotalTestCaseResultsForTestRunResult(testRunResultID) > 0)
            {
                listTestCases = GetAllTestCaseResults(testRunResultID);
            }
            else
            {
                listTestCases = GetAllTestCasesInTestRun(testRunResultID);
            }
            return listTestCases;
        }

        private List<TestCasesInTestRunResultViewModel> GetAllTestCaseResults(int testRunResultID)
        {
            var listTestCases = new List<TestCasesInTestRunResultViewModel>();
            var testCaseResultsModel = testCaseResultRepository.ListAll(testRunResultID);
            foreach (var testCaseResult in testCaseResultsModel)
            {
                var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(testCaseResult.TestCasesInTestRunID);
                var testCaseResultView = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>();
                testCaseResultView = testCaseResult.MapTo<TestCaseResultDataModel, TestCasesInTestRunResultViewModel>(testCaseResultView);
                listTestCases.Add(testCaseResultView);
            }
            return listTestCases;
        }

        private List<TestCasesInTestRunResultViewModel> GetAllTestCasesInTestRun(int testRunResultID)
        {
            var listTestCases = new List<TestCasesInTestRunResultViewModel>();
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            var testCasesInTestRunModel = testCasesInTestRunRepository.GetTestCasesInTestRun(testRunResult.TestRunID);
            foreach (var testCaseInTestRun in testCasesInTestRunModel)
            {
                if (IsValidTestCaseInTestRun(testCaseInTestRun))
                {
                    var testCaseInTestRunView = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>();
                    listTestCases.Add(testCaseInTestRunView);
                }
            }
            return listTestCases;
        }

        public IList<TestCasesInTestRunResultViewModel> GetSelectedTestCases(int testRunId, List<int> selectedTestCases)
        {
            var listTestCasesInTestRunResult = new List<TestCasesInTestRunResultViewModel>();
            foreach (var id in selectedTestCases)
            {
                var testCase = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id);
                listTestCasesInTestRunResult.Add(testCase.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>());
            }
            return listTestCasesInTestRunResult;
        }


        private bool IsValidTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            if (testCaseInTestRun == null)
            {
                logger.Error("The test case in the current test run was not found.");
                return false;
            }
            var testCase = testCaseRepository.GetTestCaseByID(testCaseInTestRun.TestCaseID);
            if (testCase == null)
            {
                logger.Error("Test case was not found.");
                return false;
            }
            var testRun = testRunRepository.GetTestRunByID(testCaseInTestRun.TestRunID);
            if (testRun == null)
            {
                logger.Error("Test run was not found.");
                return false;
            }
            return true;
        }

        public TestRunResultViewModel GetTestRunResultInProgress(int testRunID)
        {
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test run was not found.");
                throw new TestRunNotFoundException("Test run was not found.");
            }
            var testRunResultInProgressDataMode = testRunResultRepository.GetTestRunResultsInProgress(testRunID);
            if (testRunResultInProgressDataMode == null)
            {
                var testRunResultID = CreateTestRunResult(testRunID);
                testRunResultInProgressDataMode = testRunResultRepository.GetTestRunResultsInProgress(testRunID);
            }

            var testRunResultInProgressViewModel = testRunResultInProgressDataMode.MapTo<TestRunResultDataModel, TestRunResultViewModel>();
            return testRunResultInProgressViewModel;

        }
        #endregion
    }
}
