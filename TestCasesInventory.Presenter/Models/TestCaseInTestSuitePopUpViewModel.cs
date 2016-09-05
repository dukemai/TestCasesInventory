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
        public string Priority { get; set; }
        public string PriorityStyleClass
        {
            get
            {
                return string.IsNullOrEmpty(Priority) ? "default" : Priority.ToLowerInvariant();
            }
        }
        public string CreatedDate { get; set; }
        public string CreatedDisplayOnly { get; set; }
    }

    public class TestSuiteInTestRunPopUpViewModel : TestSuiteViewModel
    {
        public int TestRunID { get; set; }
    }

    public class UserPopUpViewModel : UsersBelongTeamViewModel
    {
        public int TestCaseInTestRunID { get; set; }
    }
}
