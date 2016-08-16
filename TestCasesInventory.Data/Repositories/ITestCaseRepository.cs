using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;


namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCaseRepository
    {
        IEnumerable<TestCaseDataModel> ListAll(int testSuiteID);
        TestCaseDataModel GetTestCaseByID(int testCaseID);
        void InsertTestCase(TestCaseDataModel testCase);
        void DeleteTestCase(int testCaseID);
        void UpdateTestCase(TestCaseDataModel testCase);
        void Save();
        int TotalTestCasesForTestSuite(int testSuiteID);
        IPagedList<TestCaseDataModel> GetTestCasesForTestSuite(int testSuiteId, FilterOptions filterOptions);
    }
}
