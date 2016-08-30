using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCasesInTestRunRepository
    {
        IEnumerable<TestCasesInTestRunDataModel> GetTestCasesInTestRun(int testRunID);
        void AddTestCasesToTestRun(List<TestCasesInTestRunDataModel> testCasesInTestRunData);
        void RemoveTestCasesFromTestRun(List<TestCasesInTestRunDataModel> testCasesInTestRunData);
        void Save();
        int TotalTestCasesInTestRun(int testRunID);        
        IPagedList<TestCasesInTestRunDataModel> GetPagedListTestCasesByTestRun(int testRunId, FilterOptions filterOptions);
    }
}
