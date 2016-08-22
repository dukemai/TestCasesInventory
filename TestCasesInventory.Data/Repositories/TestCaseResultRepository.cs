using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCaseResultRepository : RepositoryBase, ITestCaseResultRepository
    {
        public TestCaseResultRepository() : base() { }

        public IEnumerable<TestCaseResultDataModel> ListAll(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID).ToList();
        }

        public TestCaseResultDataModel GetTestCaseResultByID(int testCaseResultID)
        {
            return dataContext.TestCaseResults.Find(testCaseResultID);
        }

        public void InsertTestCaseResult(TestCaseResultDataModel testCaseResult)
        {
            dataContext.TestCaseResults.Add(testCaseResult);
        }

        public void DeleteTestCaseResult(int testCaseResultID)
        {
            var testCaseResultDataModel = dataContext.TestCaseResults.Find(testCaseResultID);
            dataContext.TestCaseResults.Remove(testCaseResultDataModel);
        }

        public void UpdateTestCaseResult(TestCaseResultDataModel testCaseResult)
        {
            dataContext.Entry(testCaseResult).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public int TotalTestCaseResultsForTestRunResult(int testRunResultID)
        {
            return dataContext.TestCaseResults.Where(t => t.TestRunResultID == testRunResultID).Count();
        }
    }
}
