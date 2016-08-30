﻿namespace TestCasesInventory.Presenter.Models
{
    public class TestCasesInTestRunResultViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public int TestCaseID { get; set; }
        public int TestSuiteID { get; set; }
        public int TestRunID { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        //public string TestCaseTitle { get; set; }
        //public string TestCaseDescription { get; set; }
        //public string TestCasePriority { get; set; }
        //public string PriorityStyleClass
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(TestCasePriority) ? "default" : TestCasePriority.ToLowerInvariant();
        //    }
        //}
        //public string TestRunTitle { get; set; }
        //public string TestSuiteTitle { get; set; }
    }
}
