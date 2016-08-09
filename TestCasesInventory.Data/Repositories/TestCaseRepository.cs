using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCaseRepository : RepositoryBase, ITestCaseRepository
    {
        public TestCaseRepository() : base() { }

        public IEnumerable<TestCaseDataModel> ListAll(int testSuiteID)
        {
            return dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID).ToList();
        }

        public TestCaseDataModel GetTestCaseByID(int testCaseID)
        {
            return dataContext.TestCases.Find(testCaseID);
        }

        public void InsertTestCase(TestCaseDataModel testCase)
        {
            dataContext.TestCases.Add(testCase);
        }


        public void DeleteTestCase(int testCaseID)
        {
            var TestCaseDataModel = dataContext.TestCases.Find(testCaseID);
            dataContext.TestCases.Remove(TestCaseDataModel);
        }

        public void UpdateTestCase(TestCaseDataModel testCase)
        {
            dataContext.Entry(testCase).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCaseDataModel> GetTestCasesBeSearchedByName(int testSuiteID, string title)
        {
            var testCasesForTestSuite = dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID);
            return testCasesForTestSuite.Where(t => t.Title.Contains(title)).ToList();
        }

        public int TotalTestCasesForTestSuite(int testSuiteID)
        {
            return dataContext.TestCases.Where(t => t.TestSuiteID == testSuiteID).Count();
        }
    }
}
