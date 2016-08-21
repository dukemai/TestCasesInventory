using PagedList;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestRunPresenter:IPresenter<TestRunPresenter>
    {
        TestRunViewModel GetTestRunById(int? testRunID);
        void InsertTestRun(CreateTestRunViewModel testRun);
        void UpdateTestRun(int testRunID, EditTestRunViewModel testRun);
        void DeleteTestRun(int testRunID);
        IPagedList<TestRunViewModel> GetTestRuns(FilterOptions options, string userId);
    }
}
