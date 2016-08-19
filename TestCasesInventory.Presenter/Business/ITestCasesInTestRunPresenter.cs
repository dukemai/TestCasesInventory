using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITestCasesInTestRunPresenter : IPresenter<TestCasesInTestRunViewModel>
    {
        IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testSuiteId, FilterOptions filterOptions);

    }
}
