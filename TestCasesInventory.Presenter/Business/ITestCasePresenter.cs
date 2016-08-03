using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCasePresenter : IPresenter<TestCaseViewModel>
    {
        List<TestCaseViewModel> ListAll();

        TestCaseViewModel GetTestCaseById(int? id);

        void InsertTestCase( CreateTestCaseViewModel testCase);

        void UpdateTestCase(int id, EditTestCaseViewModel testCase);

        void DeleteTestCase(int id);
    }
}
