using PagedList;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCasesInTestRunRepository
    {
        TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID);
        void InsertTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun);
        void DeleteTestCaseInTestRun(int testCaseInTestRunID);
        void UpdateTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun);
        void Save();
        IPagedList<TestCasesInTestRunDataModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions);

    }
}
