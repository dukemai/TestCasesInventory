using PagedList;
using System;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCaseResultPresenter : IPresenter<TestCaseResultPresenter>, IObservable<TestCaseResultDataModel>
    {
        TestCaseResultViewModel GetTestCaseResultById(int? testCaseResultID);
        void InsertTestCaseResult(CreateTestCaseResultViewModel testCaseResult);
        void UpdateTestCaseResult(int testCaseResultID, EditTestCaseResultViewModel testCaseResult);
        void DeleteTestCaseResult(int testCaseResultID);
        IPagedList<TestCaseResultViewModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions filterOptions);

        
    }
}
