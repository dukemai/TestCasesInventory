using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Validations;

namespace TestCasesInventory.Presenter.Models
{
    public class RoleViewModel : ViewModelBase
    {
        public string Id { get; set; }
        [Required]
        //[StringLength(ValidationMagicNumbers.MaximumLengthOfTeamName, ErrorMessage = ValidationMessages.ErrorMessageForTeamNameProperty)]
        //[Display(Name = "Role Name:")]
        //[TeamUniqueValidation]
        
        public string Name { get; set; }
    }

    public class CreateRoleViewModel: ViewModelBase
    {
        [Required]
        [Display(Name = "Role Name:")]
        public string Name { get; set; }
    }

    public class EditRoleViewModel : ViewModelBase
    {
        [Display(Name = "Role Name:")]
        public string Name { get; set; }
    }

}
