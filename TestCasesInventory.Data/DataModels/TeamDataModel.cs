using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class TeamDataModel : DataModelBase
    {
        [Required]
        public string Name { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<TestSuiteDataModel> TestSuites  { get; set; }
        public virtual ICollection<TestRunDataModel> TestRuns { get; set; }

    }
}
