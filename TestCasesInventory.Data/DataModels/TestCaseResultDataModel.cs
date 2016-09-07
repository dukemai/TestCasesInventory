using System.ComponentModel.DataAnnotations.Schema;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCaseResultDataModel : DataModelBase
    {
        public int TestRunResultID { get; set; }
        public int TestCasesInTestRunID { get; set; }
        public string Status { get; set; }
        public string RunBy { get; set; }
        public string Comment { get; set; }
        [ForeignKey("TestRunResultID")]
        public virtual TestRunResultDataModel TestRunResult { get; set; }
        [ForeignKey("TestCasesInTestRunID")]
        public virtual TestCasesInTestRunDataModel TestCasesInTestRun { get; set; }
        [ForeignKey("RunBy")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
