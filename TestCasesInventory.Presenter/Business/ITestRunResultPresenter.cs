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
        TestRunResultViewModel GetTestRunResultById(int? testRunResultID);
        void InsertTestRunResult(CreateTestRunResultViewModel testRunResult);
        void UpdateTestRunResult(int testRunResultID, EditTestRunResultViewModel testRunResult);
        void DeleteTestRunResult(int testRunResultID);
        IPagedList<TestRunResultViewModel> GetTestRunResults(FilterOptions options);
        CreateTestRunResultViewModel GetTestRunResultForCreate(int testRunId);
        EditTestRunResultViewModel GetTestRunResultForEdit(int id);
    }
}
