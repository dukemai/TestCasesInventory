using PagedList;
using System;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestRunResultPresenter : IPresenter<TestRunResultPresenter>, IObservable<TestRunResultDataModel>
    {
        #region TestRunResult


        TestRunResultViewModel GetTestRunResultById(int testRunResultID);
        int CreateTestRunResult(int testRunID);
        void DeleteTestRunResult(int testRunResultID);
        IPagedList<TestRunResultViewModel> GetTestRunResults(FilterOptions options, int testRunID);
        void FinishTestRunResult(int testRunResultID);
        IList<TestRunResultViewModel> GetTestRunResultInProgress(int testRunID);

        #endregion

        #region Test Cases

        IList<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToMe(int testRunId);
        IList<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToUser(int testRunId, string userName);
        IList<TestCasesInTestRunResultViewModel> GetAllTestCases(int testRunId);
        IList<TestCasesInTestRunResultViewModel> GetSelectedTestCases(int testRunId, List<int> selectedTestCases);        

        #endregion
    }
}
