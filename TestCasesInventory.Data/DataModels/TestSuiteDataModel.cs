using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestCasesInventory.Data.DataModels
{
    public class TestSuiteDataModel : DataModelBase
    {
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public int TeamID { get; set; }
        public virtual TeamDataModel Team { get; set; }
        public virtual ICollection<TestCaseDataModel> TestCases { get; set; }
    }
}
