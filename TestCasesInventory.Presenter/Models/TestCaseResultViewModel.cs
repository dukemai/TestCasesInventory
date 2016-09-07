using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;

namespace TestCasesInventory.Presenter.Models
{
    public class TestCaseResultViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Display(Name = "TestRunResult")]
        public string TestRunTitle { get; set; }
        public int TestRunResultID { get; set; }
        [Display(Name = "TestCase In TestRun")]
        public string TestCaseTitle { get; set; }
        public int TestCasesInTestRunID { get; set; }
        public string Status { get; set; }
        public string RunBy { get; set; }
        public string Comment { get; set; }
        public int TestCaseResultNumber { get; set; }
    }

    public class CreateTestCaseResultViewModel : ViewModelBase
    {
        [Display(Name = "TestRunResult")]
        public int TestRunResultID { get; set; }
        [Display(Name = "TestCase In TestRun")]
        public int TestCasesInTestRunID { get; set; }
        public string Status { get; set; }
        public string RunBy { get; set; }
        public string Comment { get; set; }
    }

    public class EditTestCaseResultViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Display(Name = "TestRunResult")]
        public int TestRunResultID { get; set; }
        [Display(Name = "TestCase In TestRun")]
        public int TestCasesInTestRunID { get; set; }
        public string Status { get; set; }
        public string RunBy { get; set; }
        public string Comment { get; set; }
    }
}
