using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCasesInTestRunPresenter : IPresenter<TestCasesInTestRunViewModel>
    {
        #region TestCasesInTestRun
        IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions);
        void AddTestCasesToTestRun(List<int> testCasesIDs, int testRunId);
        void RemoveTestCasesFromTestRun(List<int> testCasesIDs, int testRunId);
        List<TestCasesInTestRunViewModel> GetTestCasesInTestRun(int testRunId);
        TestCasesInTestRunViewModel GetTestCasesInTestRunById(int? testCasesInTestRunID);

        #endregion

        #region AssignTo
        IList<UserPopUpViewModel> GetUsersPopUp(int testCaseInTestRunID);
        void AssignTestCaseToUser(int testCaseInTestRunId, string username);
        void AssignTestCaseToMe(int testCaseInTestRunId);

        #endregion


    }
}
