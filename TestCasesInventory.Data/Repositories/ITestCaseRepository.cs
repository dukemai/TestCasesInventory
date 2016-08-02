using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;


namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCaseRepository
    {
        IEnumerable<TestCaseDataModel> ListAll();
        TestCaseDataModel GetTestCaseByID(int testCaseID);
        void InsertTestCase(TestCaseDataModel testCase);
        void DeleteTestCase(int testCaseID);
        void UpdateTestCase(TestCaseDataModel testCase);
        void Save();
    }
}
