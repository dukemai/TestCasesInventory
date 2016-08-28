using System.Collections.Generic;

namespace TestCasesInventory.Presenter.Models
{
    public class TestCaseInTestSuitePopUpViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TestSuiteID { get; set; }
        public string TestSuiteTitle { get; set; }
        public bool Checked { get; set; }
        public int? TestRunID { get; set; }
    }

    public class TestSuiteInTestRunPopUpViewModel : TestSuiteViewModel
    {
        public int TestRunID { get; set; }
    }


}
