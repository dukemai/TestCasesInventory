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
    public class RoleViewModel : IdentityRole
    {
        [Required]        
        [RoleValidation]      
        public int numberOfAccount { get; set; }

    }

    public class CreateRoleViewModel: ViewModelBase
    {
        [Display(Name = "Role Name:")]
        [RoleValidation]
        public string Name { get; set; }
    }

    public class EditRoleViewModel : ViewModelBase
    {
        [Display(Name = "Role Name:")]
        public string Name { get; set; }
    }

    public class UsersBelongRoleViewModel : ViewModelBase
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }

    public class UsersNotBelongRoleViewModel : ViewModelBase
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }

}
