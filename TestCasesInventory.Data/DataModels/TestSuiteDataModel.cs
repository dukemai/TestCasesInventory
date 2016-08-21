using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("TeamID")]
        public virtual TeamDataModel Team { get; set; }
        public virtual ICollection<TestCaseDataModel> TestCases { get; set; }
    }
}
