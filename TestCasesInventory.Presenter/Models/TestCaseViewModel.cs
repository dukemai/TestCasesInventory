using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Validations;

namespace TestCasesInventory.Presenter.Models
{
    public class TestCaseViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public int TestSuiteID { get; set; }
        public string TestSuiteTitle { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string Precondition { get; set; }
        public string Priority { get; set; }

        public string PriorityStyleClass
        {
            get
            {
                return string.IsNullOrEmpty(Priority) ? "default" : Priority.ToLowerInvariant();
            }
        }
        public bool Attachment { get; set; }
        [AllowHtml]
        public string Expect { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }


    public class CreateTestCaseViewModel : ViewModelBase
    {
        [Required]
        public string Title { get; set; }
        public int TestSuiteID { get; set; }
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
    }

    public class EditTestCaseViewModel : ViewModelBase
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
    }
}
