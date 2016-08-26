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
    public interface ITestRunResultRepository
    {
        IEnumerable<TestRunResultDataModel> ListAll();
        TestRunResultDataModel GetTestRunResultByID(int testRunResultID);
        void InsertTestRunResult(TestRunResultDataModel testRunResult);
        void UpdateTestRunResult(TestRunResultDataModel testRunResult);
        void DeleteTestRunResult(int testRunResultID);
        void Save();
        IEnumerable<TestCaseResultDataModel> ListTestCaseResultsForTestRunResult(int testRunResultID);
        IPagedList<TestRunResultDataModel> GetTestRunResults(FilterOptions options);
    }
}