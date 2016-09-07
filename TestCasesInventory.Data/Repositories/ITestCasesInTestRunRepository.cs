using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCasesInTestRunRepository
    {
        TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID);
        IEnumerable<TestCasesInTestRunDataModel> GetTestCasesInTestRun(int testRunID);
        void AddTestCasesToTestRun(List<TestCasesInTestRunDataModel> testCasesInTestRunData);
        void RemoveTestCasesFromTestRun(List<int> testCasesIDs, int testRunID);
        void AssignTestCaseToUser(TestCasesInTestRunDataModel testCaseInTestRun);
        TestCasesInTestRunDataModel GetTestCaseInTestRun(int testCaseID, int testRunID);
        void Save();
        int TotalTestCasesInTestRun(int testRunID);        
        IPagedList<TestCasesInTestRunDataModel> GetPagedListTestCasesByTestRun(int testRunId, FilterOptions filterOptions);
    }
}
