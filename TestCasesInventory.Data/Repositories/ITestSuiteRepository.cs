using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestSuiteRepository
    {
        IEnumerable<TestSuiteDataModel> ListAll();
        TestSuiteDataModel GetTestSuiteByID(int testSuiteID);
        void InsertTestSuite(TestSuiteDataModel testSuite);
        void UpdateTestSuite(TestSuiteDataModel testSuite);
        void DeleteTestSuite(int testSuiteID);
        void Save();
        IList<TestSuiteDataModel> GetTestSuitesBeSearchedByTitle(string valueToSearch);
        IList<TestSuiteDataModel> GetTestSuitesBeSearchedByTeam(int teamID);


        //IEnumerable<TestSuiteDataModel> GetExistedTestSuiteByName(string testSuiteName);
        //IEnumerable<TestCaseDataModel> ListTestCasesNotBelongTestSuite(int testSuiteID);
        //IEnumerable<TestCaseDataModel> ListTestCasesBelongTestSuite(int testSuiteID);
        //void AssignUsersToTeam(IList<TestCaseDataModel> testCases, int testSuiteID);
        //void RemoveUsersFromTeam(IList<TestCaseDataModel> testCases);
        //TestCaseDataModel FindTestCaseByID(string testCaseID);
    }
}
