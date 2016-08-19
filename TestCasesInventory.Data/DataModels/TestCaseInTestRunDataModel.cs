using System;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCaseInTestRunDataModel : DataModelBase
    {
        public string AssignedTo { get; set; } // UserID
        public string AssignedBy { get; set; } // UserID
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int TestRunID { get; set; }
        public virtual TestRunDataModel TestRun { get; set; }
        public int TestCaseID { get; set; }
        public virtual TestCaseDataModel TestCase { get; set; }
    }
}
