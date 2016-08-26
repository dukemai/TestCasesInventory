using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Data.DataModels
{
    public class TestRunResultDataModel : DataModelBase
    {
        public int TestRunID { get; set; }
        public string Status { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreateDisplayOnly { get; set; }
        public string LastModifiedDisplayOnly { get; set; }
        [ForeignKey("TestRunID")]
        public virtual TestRunDataModel TestRun { get; set; }
        public virtual ICollection<TestCaseResultDataModel> TestCaseResults { get; set; }
}
}
