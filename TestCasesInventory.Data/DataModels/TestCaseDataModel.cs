using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCaseDataModel : DataModelBase
    {
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string Precondition { get; set; }
        public string Priority { get; set; }
        public bool Attachment { get; set; }
        [AllowHtml]
        public string Expect { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public int TestSuiteID { get; set; }
        public virtual TestSuiteDataModel TestSuite { get; set; }
    }
}
