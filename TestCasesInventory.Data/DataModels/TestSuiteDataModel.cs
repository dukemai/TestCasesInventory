using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class TestSuiteDataModel : DataModelBase
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int TeamID { get; set; }
        public virtual TeamDataModel Team { get; set; }
        public virtual ICollection<TestCaseDataModel> TestCases { get; set; }
    }
}
