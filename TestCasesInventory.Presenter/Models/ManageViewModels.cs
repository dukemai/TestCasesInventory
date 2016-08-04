using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;

namespace TestCasesInventory.Presenter.Models
{
    public class IndexViewModel : ViewModelBase
    {

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Name")]
        public string DisplayName { get; set; }
        public bool HasPassword { get; set; }
        [Display(Name = "Role")]
        public string UserRoles { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }
        public DateTime LastModifiedDate { get; set; }

}

    public class ManageLoginsViewModel : ViewModelBase
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel : ViewModelBase
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel : ViewModelBase
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be a string with a maximum length of {1}")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel : ViewModelBase
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be a string with a maximum length of {1}")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel : ViewModelBase
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class UpdateDisplayNameViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "Name")]
        public string DisplayName { get; set; }

    }

    public class UpdateRolesViewModel : ViewModelBase
    {
        [Required]
        [Display(Name = "User Roles")]
        public string UserRoles { get; set; }
        public List<System.Web.Mvc.SelectListItem> RoleList { get; set; }
        public UpdateRolesViewModel()
        {
            RoleList = new List<System.Web.Mvc.SelectListItem>();
        }
    }

    public class VerifyPhoneNumberViewModel : ViewModelBase
    {
        
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel : ViewModelBase
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}
