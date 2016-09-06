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

        #endregion

        #region Test Cases

        List<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToMe(int testRunId);
        List<TestCasesInTestRunResultViewModel> GetTestCasesAssignedToUser(int testRunId, string userName);
        List<TestCasesInTestRunResultViewModel> GetAllTestCases(int testRunId);
        List<TestCasesInTestRunResultViewModel> GetSelectedTestCases(int testRunId, List<int> selectedTestCases);        

        #endregion
    }
}
