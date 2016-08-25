using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasesInTestRunPresenter : PresenterBase, ITestCasesInTestRunPresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ITestRunRepository testRunRepository;
        protected ApplicationUserManager UserManager;




        public TestCasesInTestRunPresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            testSuiteRepository = new TestSuiteRepository();
            testCaseRepository = new TestCaseRepository();
            testRunRepository = new TestRunRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public TestCasesInTestRunViewModel GetTestCaseInTestRunById(int? id)
        {
            if (!id.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
            }
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id.Value);
            CheckExceptionTestCaseInTestRun(testCaseInTestRun);
            var testCaseInTestRunViewModel = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>();
            return testCaseInTestRunViewModel;
        }

        public void AddTestCasesToTestRun(List<TestCaseInTestSuitePopUpViewModel> testCases, int testRunID)
        {
            var testRun = testRunRepository.GetTestRunByID(testRunID);
            if (testRun == null)
            {
                logger.Error("Test Run was not found.");
                throw new TestRunNotFoundException("Test Run was not found.");
            }
            foreach (var testCase in testCases)
            {
                var user = UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                if(user == null)
                {
                    logger.Error("User was not found.");
                    throw new UserNotFoundException("User was not found.");
                }
                var testCaseDataModel = testCaseRepository.GetTestCaseByID(testCase.ID);
                if (testCaseDataModel == null)
                {
                    logger.Error("Test Case with id = " + testCase.ID + " was not found.");
                    throw new TestCaseNotFoundException("Test Case with id = " + testCase.ID + " was not found.");
                }
                TestCasesInTestRunDataModel testCaseAlreadyInTestRun = testCasesInTestRunRepository.TestCaseAlreadyInTestRun(testRunID, testCase.ID);
                if (!testCase.IsInTestRun && testCaseAlreadyInTestRun != null)
                {
                    testCasesInTestRunRepository.DeleteTestCaseInTestRun(testCaseAlreadyInTestRun.ID);
                    testCasesInTestRunRepository.Save();
                }
                if (testCase.IsInTestRun && testCaseAlreadyInTestRun == null)
                {
                    var testCaseInTestRunViewModel = new CreateTestCasesInTestRunViewModel
                    {
                        TestCaseID = testCase.ID,
                        TestRunID = testRunID,
                        TestSuiteID = testCase.TestSuiteID,
                        CreatedDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        Created = user.Email,
                        LastModified = user.Email,
                        AssignedTo = user.Email,
                    };
                    var testCaseInTestRunDataModel = testCaseInTestRunViewModel.MapTo<CreateTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>();
                    testCasesInTestRunRepository.InsertTestCaseInTestRun(testCaseInTestRunDataModel);
                    testCasesInTestRunRepository.Save();
                }
            }
        }


        public void DeleteTestCaseInTestRun(int id)
        {
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id);
            CheckExceptionTestCaseInTestRun(testCaseInTestRun);
            testCasesInTestRunRepository.DeleteTestCaseInTestRun(id);
            testCasesInTestRunRepository.Save();
        }

        public void CheckExceptionTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun)
        {
            if (testCaseInTestRun == null)
            {
                logger.Error("The test case in the current test run was not found.");
                throw new TestCaseInTestRunNotFoundException("The test case in the current test run was not found.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testCaseInTestRun.TestSuiteID);
            var testCase = testCaseRepository.GetTestCaseByID(testCaseInTestRun.TestCaseID);
            if (testCase == null)
            {
                logger.Error("Test case was not found.");
                throw new TestCaseNotFoundException("Test case was not found.");
            }
            var testRun = testRunRepository.GetTestRunByID(testCaseInTestRun.TestRunID);
            if (testRun == null)
            {
                logger.Error("Test run was not found.");
                throw new TestCaseNotFoundException("Test run was not found.");
            }
        }



        public IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            var list = testCasesInTestRunRepository.GetTestCasesByTestRunID(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }

    }
}
