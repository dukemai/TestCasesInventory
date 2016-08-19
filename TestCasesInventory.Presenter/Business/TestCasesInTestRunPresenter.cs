using PagedList;
using System.Web;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TestCasesInTestRunPresenter: PresenterBase, ITestCasesInTestRunPresenter
    {
        protected HttpContextBase HttpContext;
        protected ITestCasesInTestRunRepository testCasesInTestRunRepository;

        public TestCasesInTestRunPresenter(HttpContextBase context)
        {
            HttpContext = context;
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
        }

        public IPagedList<TestCasesInTestRunViewModel> GetTestCasesByTestRunID(int testRunId, FilterOptions filterOptions)
        {
            var list = testCasesInTestRunRepository.GetTestCasesByTestRunID(testRunId, filterOptions);
            var mappedList = list.MapTo<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>();
            return mappedList;
        }

    }
}
