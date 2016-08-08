﻿using System.Collections.Generic;
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
        IEnumerable<TestCaseDataModel> ListTestCasesForTestSuite(int testSuiteID);
    }
}
