using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITestCaseResultRepository
    {
        IEnumerable<TestCaseResultDataModel> ListAll(int testRunResultID);
        TestCaseResultDataModel GetTestCaseResultByID(int testCaseResultID);
        void InsertTestCaseResult(TestCaseResultDataModel testCaseResult);
        void DeleteTestCaseResult(int testCaseResultID);
        void UpdateTestCaseResult(TestCaseResultDataModel testCaseResult);
        void Save();
        int TotalTestCaseResultsForTestRunResult(int testRunResultID);
        IPagedList<TestCaseResultDataModel> GetTestCasesForTestSuite(int testRunResultId, FilterOptions filterOptions);
    }
}
