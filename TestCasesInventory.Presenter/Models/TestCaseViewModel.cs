using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Validations;
using System.Linq;

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
        public string Priority { get; set; }
        [AllowHtml]
        public string Precondition { get; set; }
        [Display(Name = "Attachment")]
        public List<string> AttachmentUrlList { get; set; }
        public bool HasAttachment
        {
            get
            {
                return AttachmentUrlList.Any();
            }
        }
        public string PriorityStyleClass
        {
            get
            {
                return string.IsNullOrEmpty(Priority) ? "default" : Priority.ToLowerInvariant();
            }
        }

        [AllowHtml]
        public string Expect { get; set; }
    }


    public class CreateTestCaseViewModel : ViewModelBase
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
        public List<SelectListItem> Priorities { get; set; }
        public bool Attachment { get; set; }
        [AllowHtml]
        public string Expect { get; set; }
        public List<string> AttachmentUrlList { get; set; }
        public bool HasAttachment { get; set; }
        public string PriorityStyleClass
        {
            get
            {
                return string.IsNullOrEmpty(Priority) ? "default" : Priority.ToLowerInvariant();
            }
        }

    }

    public class EditTestCaseViewModel : ViewModelBase
    {
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string Precondition { get; set; }
        public string Priority { get; set; }
        public List<SelectListItem> Priorities { get; set; }
        [AllowHtml]
        public string Expect { get; set; }
        public List<string> AttachmentUrlList { get; set; }
        public bool HasAttachment { get; set; }
        public string PriorityStyleClass
        {
            get
            {
                return string.IsNullOrEmpty(Priority) ? "default" : Priority.ToLowerInvariant();
            }
        }
    }
}
