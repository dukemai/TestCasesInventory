using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;

namespace TestCasesInventory.Presenter.Models
{
    public class ExternalLoginConfirmationViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel : ViewModelBase
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel : ViewModelBase
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel : ViewModelBase
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Email")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : ViewModelBase
    {
        //RegisterViewModelConfig Config = new RegisterViewModelConfig();

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfPassword, ErrorMessage = ValidationMessages.ErrorMessageForPasswordProperty)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ValidationMessages.ErrorMessageForConFirmPasswordProperty)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfDisplayName, ErrorMessage = ValidationMessages.ErrorMessageForDisplayNameProperty)]
        [Display(Name = "Name")]
        public string DisplayName { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class ResetPasswordViewModel : ViewModelBase
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfPassword, ErrorMessage = ValidationMessages.ErrorMessageForDisplayNameProperty)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ValidationMessages.ErrorMessageForPasswordProperty)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel : ViewModelBase
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
