using System;
using System.ComponentModel.DataAnnotations;
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
        public string Description { get; set; }
        public string Precondition { get; set; }
        public bool Attachment { get; set; }
        public string Expect { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class TestCaseDetailViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Precondition { get; set; }
        public bool Attachment { get; set; }
        public string Expect { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateTestCaseViewModel : ViewModelBase
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
    }

    public class EditTestCaseViewModel : ViewModelBase
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
    }
}
