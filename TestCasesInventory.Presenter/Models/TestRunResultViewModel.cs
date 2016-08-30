using System;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Presenter.Models
{
    public class TestRunResultViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Required]
        public int TestRunID { get; set; }
        public string Status { get; set; }
        public string CreateDisplayOnly { get; set; }
        public string LastModifiedDisplayOnly { get; set; }
    
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
