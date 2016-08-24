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
    public class TestCasesInTestRunPresenter: PresenterBase, ITestCasesInTestRunPresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;
        protected ITestSuiteRepository testSuiteRepository;
        protected ITestCaseRepository testCaseRepository;
        protected ITestRunRepository testRunRepository;



        public TestCasesInTestRunPresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            testSuiteRepository = new TestSuiteRepository();
            testCaseRepository = new TestCaseRepository();
            testRunRepository = new TestRunRepository();
        }

        public TestCasesInTestRunViewModel GetTestCaseInTestRunById(int? id)
        {
            if (!id.HasValue)
            {
                logger.Error("Id was not valid.");
                throw new Exception("Id was not valid.");
            }
            var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByID(id.Value);
            if (testCaseInTestRun == null)
            {
                logger.Error("The test case in the current test run was not found.");
                throw new TestCaseInTestRunNotFoundException("The test case in the current test run was not found.");
            }
            var testSuite = testSuiteRepository.GetTestSuiteByID(testCaseInTestRun.TestSuiteID);
            if (testSuite == null)
            {
                logger.Error("Test suite was not found.");
                throw new TestSuiteNotFoundException("Test suite was not found.");
            }
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
            var testCaseInTestRunViewModel = testCaseInTestRun.MapTo<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>();
            return testCaseInTestRunViewModel;
        }





        public IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            var list = testCasesInTestRunRepository.GetTestCasesByTestRunID(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }

    }
}
