using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestCasesInventory.Data.DataModels
{
    public class TestRunDataModel : DataModelBase
    {
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int TeamID { get; set; }
        public virtual TeamDataModel Team { get; set; }
        public virtual ICollection<TestCasesInTestRunDataModel> TestCasesInTestRun { get; set; }
        public virtual ICollection<TestRunResultDataModel> TestRunResults { get; set; }
		public string TeamNameDisplayOnly { get; set; }
        public string CreateDisplayOnly { get; set; }
        public string LastModifiedDisplayOnly { get; set; }
    }
}
