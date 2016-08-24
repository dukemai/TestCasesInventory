using PagedList;
using System;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestRunPresenter : IPresenter<TestRunPresenter>, IObservable<TestRunDataModel>
    {
        TestRunViewModel GetTestRunById(int? testRunID);
        void InsertTestRun(CreateTestRunViewModel testRun);
        void UpdateTestRun(int testRunID, EditTestRunViewModel testRun);
        void DeleteTestRun(int testRunID);
        IPagedList<TestRunViewModel> GetTestRuns(FilterOptions options, string userId);
        CreateTestRunViewModel GetTestRunForCreate();
        CreateTestRunViewModel GetTestRunForAdminCreate(int? teamID);
        EditTestRunViewModel GetTestRunForEdit(int testRunID);
        EditTestRunViewModel GetTestRunForAdminEdit(int testRunID);
    }
}
