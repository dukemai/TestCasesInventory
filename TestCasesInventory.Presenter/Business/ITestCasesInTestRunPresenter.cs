using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCasesInTestRunPresenter : IPresenter<TestCasesInTestRunViewModel>
    {
        TestCasesInTestRunViewModel GetTestCaseInTestRunById(int? id);
        void DeleteTestCaseInTestRun(int id);
        void AddTestCasesToTestRun(List<TestCaseInTestSuitePopUpViewModel> testCases, int testRunID);
        void CheckExceptionTestCaseInTestRun(TestCasesInTestRunDataModel testCaseInTestRun);
        void AssignToMe(int? testCaseInTestRunID, string userId);
        IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testSuiteId, FilterOptions filterOptions);

    }
}
