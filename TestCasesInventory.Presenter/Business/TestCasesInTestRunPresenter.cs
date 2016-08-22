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

        public List<TestSuiteInTestRunPopUpViewModel> GetTestRunPopUp(int testRunID)
        {
            var testRunPopUp = new List<TestSuiteInTestRunPopUpViewModel>();
            var listTestSuites = testSuiteRepository.ListAll();
            foreach(var testSuite in listTestSuites)
            {
                var testSuiteInTestRunPopUp = new TestSuiteInTestRunPopUpViewModel();
                var listTestCases = testCaseRepository.ListAll(testSuite.ID);
                testSuiteInTestRunPopUp.TestSuite = testSuite.MapTo<TestSuiteDataModel, TestSuiteViewModel>();
                foreach(var testCase in listTestCases)
                {
                    var testCaseInTestRunPopUp = testCase.MapTo<TestCaseDataModel, TestCaseInTestRunPopUpViewModel>();
                    var testCaseInTestRun = testCasesInTestRunRepository.GetTestCaseInTestRunByTestCaseID(testRunID, testCase.ID);
                    if(testCaseInTestRun != null)
                    {
                        testCaseInTestRunPopUp.isInTestRun = true;
                        testCaseInTestRunPopUp.TestRunID = testRunID;
                    }
                    testSuiteInTestRunPopUp.ListTestCaseInTestRunPopUp.Add(testCaseInTestRunPopUp);
                }
                testRunPopUp.Add(testSuiteInTestRunPopUp);
            }
            return testRunPopUp;
        }

        public IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            var list = testCasesInTestRunRepository.GetTestCasesByTestRunID(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }

    }
}
