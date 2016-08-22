using System.Collections.Generic;

namespace TestCasesInventory.Presenter.Models
{
    public class TestSuiteStorageViewModel : ViewModelBase
    {
        public TestSuiteViewModel TestSuite { get; set; }
        public List<TestCaseInStorageViewModel> TestCasesInStorage { get; set; }

    }

    public class TestCaseInStorageViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TestSuiteID { get; set; }
        public string TestSuiteTitle { get; set; }
        public bool isInTestRun { get; set; }
        public int? TestRunID { get; set; }
    }

}
