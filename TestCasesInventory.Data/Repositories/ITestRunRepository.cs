using System.Collections.Generic;
using PagedList;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestRunRepository
    {
        IEnumerable<TestRunDataModel> ListAll();
        TestRunDataModel GetTestRunByID(int testRunID);
        void InsertTestRun(TestRunDataModel testRun);
        void UpdateTestRun(TestRunDataModel testRun);
        void DeleteTestRun(int testRunID);
        void Save();
        IPagedList<TestRunDataModel> GetTestRuns(FilterOptions options, int? teamID, bool getAll);
        IEnumerable<TestCasesInTestRunDataModel> ListTestCasesForTestRun(int testRunID);
    }
}
