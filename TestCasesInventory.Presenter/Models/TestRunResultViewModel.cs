using System.ComponentModel.DataAnnotations;
using TestCasesInventory.Data.Common;

namespace TestCasesInventory.Presenter.Models
{
    public class TestRunResultViewModel : ViewModelBase
    {
        public int Index { get; set; }
        public int ID { get; set; }
        [Required]
        public int TestRunID { get; set; }
        public string Status { get; set; }
        public string TestRunTitle { get; set; }
        public int NumberOfPassedTestCases { get; set; }
        public int NumberOfFailedTestCases { get; set; }
        public int NumberOfTestCases { get; set; }
        public int NumberOfSkippedTestCases
        {
            get
            {
                return (NumberOfTestCases - NumberOfFailedTestCases - NumberOfPassedTestCases);
            }
        }
        public string StatusStyleClass
        {
            get
            {
                if (Status == TestRunResultStatus.InProgress)
                {
                    return "warning";
                }
                else if (Status == TestRunResultStatus.Finished)
                {
                    return "success";
                }
                else
                {
                    return "default";
                }
            }
        }
        public string CreateDisplayOnly { get; set; }
        public string LastModifiedDisplayOnly { get; set; }

    }

    public class TestRunResultDetailViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public int TestRunID { get; set; }
    }

    public class CreateTestRunResultViewModel : ViewModelBase
    {
        [Required]
        public int TestRunID { get; set; }
        public string Status { get; set; }
        public string TestRunOption { get; set; }
    }
    public class EditTestRunResultViewModel : ViewModelBase
    {
        public string Status { get; set; }
    }

}
