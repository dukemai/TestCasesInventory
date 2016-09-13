using PagedList;
using System;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCaseResultPresenter : IPresenter<TestCaseResultPresenter>
    {
        TestCaseResultViewModel GetTestCaseResult(int testCasesInTestRunID, int testRunResultID);
        TestCaseResultViewModel GetTestCaseResultById(int? testCaseResultID);      
        int InsertOrUpdateTestCaseResult(CreateTestCaseResultViewModel testCaseResult);     
        IPagedList<TestCaseResultViewModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions filterOptions);

        
    }
}
