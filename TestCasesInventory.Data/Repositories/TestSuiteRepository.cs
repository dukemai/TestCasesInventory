using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TestSuiteRepository : RepositoryBase, ITestSuiteRepository, IDisposable
    {
        public TestSuiteRepository()
            : base()
        {
        }

        public IEnumerable<TestSuiteDataModel> ListAll()
        {
            return dataContext.TestSuites.ToList();
        }

        public TestSuiteDataModel GetTestSuiteByID(int testSuiteID)
        {
            return dataContext.TestSuites.Find(testSuiteID);
        }

        public void InsertTestSuite(TestSuiteDataModel testSuite)
        {
            dataContext.TestSuites.Add(testSuite);
        }

        public void UpdateTestSuite(TestSuiteDataModel testSuite)
        {
            dataContext.Entry(testSuite).State = EntityState.Modified;
        }

        public void DeleteTestSuite(int testSuiteID)
        {
            TestSuiteDataModel testSuiteDataModel = dataContext.TestSuites.Find(testSuiteID);
            dataContext.TestSuites.Remove(testSuiteDataModel);
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        //public IEnumerable<TestSuiteDataModel> GetExistedTeamByName(string teamName)
        //{
        //    return dataContext.TestSuites.Where(t => t.Name == teamName).ToList();
        //}
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {


            if (!this.disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
