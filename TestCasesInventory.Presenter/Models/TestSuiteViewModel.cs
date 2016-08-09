using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;

namespace TestCasesInventory.Presenter.Models
{
    public class TestSuiteViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfTestSuiteTitle, ErrorMessage = ValidationMessages.ErrorMessageForTestSuiteTitleProperty)]
        public string Title { get; set; }
        public string TeamName { get; set; }
        public int TestCasesNumber { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateTestSuiteViewModel : ViewModelBase
    {
        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfTestSuiteTitle, ErrorMessage = ValidationMessages.ErrorMessageForTestSuiteTitleProperty)]
        public string Title { get; set; }
        public int TeamID { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class EditTestSuiteViewModel : ViewModelBase
    {
        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfTestSuiteTitle, ErrorMessage = ValidationMessages.ErrorMessageForTestSuiteTitleProperty)]
        public string Title { get; set; }
        public int TeamID { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

}
