using PagedList;
using System;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestSuitePresenter : IPresenter<TestSuitePresenter>, IObservable<TestSuiteDataModel>
    {
        TestSuiteViewModel GetTestSuiteById(int testSuiteID);
        void InsertTestSuite(CreateTestSuiteViewModel testSuite);
        void UpdateTestSuite(int testSuiteID, EditTestSuiteViewModel testSuite);
        void DeleteTestSuite(int testSuiteID);
        IPagedList<TestSuiteViewModel> GetTestSuites(FilterOptions options, string userId);
        CreateTestSuiteViewModel GetTestSuiteForCreate();
        CreateTestSuiteViewModel GetTestSuiteForAdminCreate(int teamID);
        EditTestSuiteViewModel GetTestSuiteForEdit(int testSuiteID);
        EditTestSuiteViewModel GetTestSuiteForAdminEdit(int testSuiteID);
    }
}
