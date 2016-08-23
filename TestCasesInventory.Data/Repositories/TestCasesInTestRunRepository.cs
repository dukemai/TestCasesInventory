﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;

namespace TestCasesInventory.Data.Repositories
{
    public class TestCasesInTestRunRepository : RepositoryBase, ITestCasesInTestRunRepository
    {
        public TestCasesInTestRunRepository() : base() { }
        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TestCasesInTestRunDataModel> ListAll(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).ToList();
        }

        public int TotalTestCasesForTestRun(int testRunID)
        {
            return dataContext.TestCasesInTestRuns.Where(t => t.TestRunID == testRunID).Count();
        }
    }
}