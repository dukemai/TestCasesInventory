using System;
using System.Collections.Generic;
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
        public string Created { get; set; }
        public string LastModified { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime LastRunDate { get; set; }
    }
}
