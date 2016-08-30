using System.Collections.Generic;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCasesInTestRunPresenter : IPresenter<TestCasesInTestRunViewModel>
    {
        #region TestCasesInTestRun

        void AddTestCasesToTestRun(List<int> testCasesIDs, int testRunId);
        void RemoveTestCasesFromTestRun(List<int> testCasesIDs, int testRunId);
        List<TestCasesInTestRunViewModel> GetTestCasesInTestRun(int testRunId);

        #endregion

        #region AssignTo

        void AssignTestCaseToUser(int testCaseInTestRunId, string username);
        void AssignTestCaseToMe(int testCaseInTestRunId);

        #endregion
    }
}
