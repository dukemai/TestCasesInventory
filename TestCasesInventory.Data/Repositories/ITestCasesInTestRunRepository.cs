using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCasesInTestRunRepository
    {
        IEnumerable<TestCasesInTestRunDataModel> ListAll(int testRunID);
        TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID);
        void InsertTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun);
        void DeleteTestCaseInTestRun(int testCaseInTestRunID);
        void UpdateTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun);
        void Save();
        int TotalTestCasesInTestRun(int testRunID);
        bool CheckTestCaseInTestRunByTestCaseID(int testRunID, int testCaseID);
        IPagedList<TestCasesInTestRunDataModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions);

    }
}
