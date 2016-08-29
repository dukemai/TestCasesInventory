using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCasePresenter : IPresenter<TestCaseViewModel>
    {
        TestCaseViewModel GetTestCaseById(int? id);
        void InsertTestCase(CreateTestCaseViewModel testCase);
        void UpdateTestCase(int id, EditTestCaseViewModel testCase);
        void DeleteTestCase(int id);
        IPagedList<TestCaseViewModel> GetTestCasesForTestSuite(int testSuiteId, FilterOptions filterOptions);
        CreateTestCaseViewModel GetTestCaseForCreate(int testSuiteId);
        EditTestCaseViewModel GetTestCaseForEdit(int id);
        string BuildTestCaseAttachmentUrl(string fileName, int testCaseId);
        List<string> GetTestCaseAttachments(int? testCaseId);
    }
}
