using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCaseDataModel : DataModelBase
    {
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string Precondition { get; set; }
        public string Priority { get; set; }
        public int PriorityValue { get; set; }
        public bool Attachment { get; set; }
        [AllowHtml]
        public string Expect { get; set; }
        public int TestSuiteID { get; set; }
        [ForeignKey("TestSuiteID")]
        public virtual TestSuiteDataModel TestSuite { get; set; }
        public virtual ICollection<TestCasesInTestRunDataModel> TestCasesInTestRuns { get; set; }

    }
}
