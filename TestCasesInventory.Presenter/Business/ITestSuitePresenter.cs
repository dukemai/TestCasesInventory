using System.Collections.Generic;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestSuitePresenter : IPresenter<TestSuitePresenter>
    {
        List<TestSuiteViewModel> ListAll();
        TestSuiteViewModel GetTestSuiteById(int? testSuiteID);
        void InsertTestSuite(CreateTestSuiteViewModel testSuite);
        void UpdateTestSuite(int testSuiteID, EditTestSuiteViewModel testSuite);
        void DeleteTestSuite(int testSuiteID);
        List<TestSuiteViewModel> GetTestSuitesBeSearched(string valueToSearch, string searchBy);
        List<TestSuiteViewModel> GetTestSuitesBeSorted(List<TestSuiteViewModel> testSuites, string sortBy);

    }
}
