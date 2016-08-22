using System.Collections.Generic;

namespace TestCasesInventory.Presenter.Models
{
    public class TestSuiteInTestRunPopUpViewModel : ViewModelBase
    {
        public TestSuiteViewModel TestSuite { get; set; }
        public List<TestCaseInTestRunPopUpViewModel> ListTestCaseInTestRunPopUp { get; set; }
    }

    public class TestCaseInTestRunPopUpViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TestSuiteID { get; set; }
        public string TestSuiteTitle { get; set; }
        public bool isInTestRun { get; set; }
        public int? TestRunID { get; set; }
    }

}
