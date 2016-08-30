using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCasesInTestRunRepository
    {
        void Save();
        IEnumerable<TestCasesInTestRunDataModel> ListAll(int testRunID);
        int TotalTestCasesForTestRun(int testRunID);
        IList<TestCasesInTestRunDataModel> GetTestCasesInTestRunAssignedToMe(string userId, int testRunID);
        TestCasesInTestRunDataModel GetTestCaseInTestRunByID(int testCaseInTestRunID);
    }
}
