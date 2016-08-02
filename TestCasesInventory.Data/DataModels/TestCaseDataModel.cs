using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class TestCaseDataModel : DataModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Precondition { get; set; }
        public bool Attachment { get; set; }
        public string Expect { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public int TestSuiteID { get; set; }
        public virtual TestSuiteDataModel TestSuite { get; set; }
    }
}
