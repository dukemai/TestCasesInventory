using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;


namespace TestCasesInventory.Presenter.Business
{
    public interface ITestRunPresenter
    {
        TestRunViewModel GetTestRunById(int? testRunID);
        void InsertTestRun(CreateTestRunViewModel testRun);
        void UpdateTestRun(int testRunID, EditTestRunViewModel testRun);
        void DeleteTestRun(int testRunID);
        IPagedList<TestRunViewModel> GetTestRuns(FilterOptions options, string userId);
        List<TestSuiteViewModel> GetTestSuitesPopUp(int testRunID);
        List<TestCaseInTestSuitePopUpViewModel> GetTestCasesInTestSuitePopUp(int testSuiteID, int testRunID);


    }
}
