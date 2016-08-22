using PagedList;
using System.Collections.Generic;
using System.Web;
using TestCasesInventory.Common;
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



        public TestCasesInTestRunPresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            testSuiteRepository = new TestSuiteRepository();
            testCaseRepository = new TestCaseRepository();
        }

        public List<TestSuiteStorageViewModel> GetTestSuiteStorages(int testRunID)
        {
            var testSuiteStorages = new List<TestSuiteStorageViewModel>();
            var listTestSuites = testSuiteRepository.ListAll();
            foreach(var testSuite in listTestSuites)
            {
                var storage = new TestSuiteStorageViewModel();
                var listTestCases = testCaseRepository.ListAll(testSuite.ID);
                storage.TestSuite = testSuite.MapTo<TestSuiteDataModel, TestSuiteViewModel>();
                foreach(var testCase in listTestCases)
                {
                    var testCaseInStorage = testCase.MapTo<TestCaseDataModel, TestCaseInStorageViewModel>();
                    var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByTestCaseID(testRunID, testCase.ID);
                    if(testCaseInTestRun != null)
                    {
                        testCaseInStorage.isInTestRun = true;
                        testCaseInStorage.TestRunID = testRunID;
                    }
                    storage.TestCasesInStorage.Add(testCaseInStorage);
                }
                testSuiteStorages.Add(storage);
            }
            return testSuiteStorages;
        }

        public IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            var list = testCasesInTestRunRepository.GetTestCasesByTestRunID(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }

    }
}
