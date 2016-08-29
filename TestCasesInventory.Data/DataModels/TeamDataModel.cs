using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class TeamDataModel : DataModelBase
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<TestSuiteDataModel> TestSuites  { get; set; }
        public virtual ICollection<TestRunDataModel> TestRuns { get; set; }

    }
}
