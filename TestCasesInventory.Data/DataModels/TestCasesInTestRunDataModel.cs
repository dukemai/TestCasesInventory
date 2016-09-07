using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCasesInTestRunDataModel : DataModelBase
    {
        public int TestCaseID { get; set; }
        public int TestRunID { get; set; }
        public int TestSuiteID { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime LastRunDate { get; set; }
        [ForeignKey("TestRunID")]
        public virtual TestRunDataModel TestRun { get; set; }
        public virtual ICollection<TestCaseResultDataModel> TestCaseResults { get; set; }
        [ForeignKey("TestCaseID")]
        public virtual TestCaseDataModel TestCase { get; set; }
        [ForeignKey("AssignedTo")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
