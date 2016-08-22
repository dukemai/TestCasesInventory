using System;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TestRunResultRepository : RepositoryBase, ITestRunResultRepository
    {
        public TestRunResultRepository() : base() {}
        public IEnumerable<TestRunResultDataModel> ListAll()
        {
            return dataContext.TestRunResults.ToList();
        }

        public TestRunResultDataModel GetTestRunResultByID(int testRunResultID)
        {
            return dataContext.TestRunResults.Find(testRunResultID);
        }

        public void InsertTestRunResult(TestRunResultDataModel testRunResult)
        {
            dataContext.TestRunResults.Add(testRunResult);
        }

        public void UpdateTestRunResult(TestRunResultDataModel testRunResult)
        {
            dataContext.Entry(testRunResult).State = EntityState.Modified;
        }

        public void DeleteTestRunResult(int testRunResultID)
        {
            TestRunResultDataModel testRunResultDataModel = dataContext.TestRunResults.Find(testRunResultID);
            dataContext.TestRunResults.Remove(testRunResultDataModel);
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCaseResultDataModel> ListTestCaseResultsForTestRunResult(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID).ToList();
        }
    }
}
