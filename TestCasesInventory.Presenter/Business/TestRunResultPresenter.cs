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

        public List<TestRunResultViewModel> GetTestRunResults(FilterOptions options, int testRunID)
        {
            var listTestRunResutlDataModel = testRunResultRepository.ListAll(options, testRunID);
            var listTestRunResutlViewModel = new List<TestRunResultViewModel>();
            var index = 0;
            if (options.SortOptions.Direction == SortDirections.Asc)
            {
                foreach (var testRunResultDataModel in listTestRunResutlDataModel)
                {
                    index++;
                    var testRunResultViewModel = testRunResultDataModel.MapTo<TestRunResultDataModel, TestRunResultViewModel>();
                    testRunResultViewModel.Index = index;
                    listTestRunResutlViewModel.Add(testRunResultViewModel);
                }
            }
            else
            {
                index = listTestRunResutlDataModel.Count();
                foreach (var testRunResultDataModel in listTestRunResutlDataModel)
                {
                    var testRunResultViewModel = testRunResultDataModel.MapTo<TestRunResultDataModel, TestRunResultViewModel>();
                    testRunResultViewModel.Index = index--;
                    listTestRunResutlViewModel.Add(testRunResultViewModel);
                }
            }

            return listTestRunResutlViewModel;
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
        public IList<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToUser(int testRunResultID, string userName)
        {
            var user = UserManager.FindByEmail(userName);
            if (user == null)
            {
                logger.Error("User was not found.");
                throw new UserNotFoundException("User was not found.");
            }

            var listTestCases = new List<TestCasesInTestRunResultViewModel>();
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRun Result with id" + testRunResultID + "was not found.");
            }

            var testCasesInTestRunModel = testCasesInTestRunRepository.GetTestCasesInTestRunAssignedToUser(testRunResult.TestRunID, user.Id);
            foreach (var testCaseInTestRun in testCasesInTestRunModel)
            {
                var testCaseResultModel = testCaseResultRepository.GetTestCaseResult(testCaseInTestRun.ID, testRunResultID);
                if (IsValidTestCaseInTestRun(testCaseInTestRun))
                {
                    var testCaseInTestRunView = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>();
                    if (testCaseResultModel != null)
                    {
                        testCaseInTestRunView = testCaseResultModel.MapTo<TestCaseResultDataModel, TestCasesInTestRunResultViewModel>(testCaseInTestRunView);
                    }
                    listTestCases.Add(testCaseInTestRunView);
                }
            }
            return listTestCases;
        }
        public IList<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToMe(int testRunResultID)
        {
            var userName = UserManager.FindById(HttpContext.User.Identity.GetUserId()).Email;
            var listTestCasesAssignedToMe = GetTestCasesAssignedToUser(testRunResultID, userName);
            return listTestCasesAssignedToMe;
        }

        public IList<TestCasesInTestRunResultViewModel> GetAllTestCases(int testRunResultID)
        {
            var listTestCases = new List<TestCasesInTestRunResultViewModel>();
            var testRunResult = testRunResultRepository.GetTestRunResultByID(testRunResultID);
            if (testRunResult == null)
            {
                logger.Error("TestRun Result with id" + testRunResultID + "was not found.");
            }
            var testCasesInTestRunModel = testCasesInTestRunRepository.GetTestCasesInTestRun(testRunResult.TestRunID);
            foreach (var testCaseInTestRun in testCasesInTestRunModel)
            {
                var testCaseResultModel = testCaseResultRepository.GetTestCaseResult(testCaseInTestRun.ID, testRunResultID);
                if (IsValidTestCaseInTestRun(testCaseInTestRun))
                {
                    var testCaseInTestRunView = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>();
                    if(testCaseResultModel != null)
                    {
                        testCaseInTestRunView = testCaseResultModel.MapTo<TestCaseResultDataModel, TestCasesInTestRunResultViewModel>(testCaseInTestRunView);
                    }
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
