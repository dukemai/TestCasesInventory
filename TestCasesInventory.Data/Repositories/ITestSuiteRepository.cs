using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestSuiteRepository
    {
        IEnumerable<TestSuiteDataModel> ListAll();
        TestSuiteDataModel GetTestSuiteByID(int testSuiteID);
        void InsertTestSuite(TestSuiteDataModel testSuite);
        void DeleteTestSuite(int testSuiteID);
        void UpdateTestSuite(TestSuiteDataModel testSuite);
        void Save();
        IEnumerable<TestSuiteDataModel> GetExistedTestSuiteByName(string testSuiteName);
        //IEnumerable<TestCaseDataModel> ListTestCasesNotBelongTestSuite(int testSuiteID);
        //IEnumerable<TestCaseDataModel> ListTestCasesBelongTestSuite(int testSuiteID);
        //void AssignUsersToTeam(IList<TestCaseDataModel> testCases, int testSuiteID);
        //void RemoveUsersFromTeam(IList<TestCaseDataModel> testCases);
        //TestCaseDataModel FindTestCaseByID(string testCaseID);
    }
}
