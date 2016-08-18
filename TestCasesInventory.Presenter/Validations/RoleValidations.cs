using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestCasesInventory.Data;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Validations
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        protected RoleManager<IdentityRole> RoleManager;
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(RoleValidationAttribute));


        public RoleValidationAttribute()
        {
            RoleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            CreateRoleViewModel role = validationContext.ObjectInstance as CreateRoleViewModel;

            if(role == null)
            {
                logger.Error("Expected object is CreateRoleViewModel while its type is " + validationContext.ObjectInstance.GetType());
                throw new System.Exception("Expected object is CreateRoleViewModel while its type is " + validationContext.ObjectInstance.GetType());
            }

            if (string.IsNullOrEmpty(role.Name) || string.IsNullOrWhiteSpace(role.Name))
            {
                return new ValidationResult("Role Name Is Required");
            }

            if(RoleManager.RoleExists(role.Name))
            {
                return new ValidationResult("Role already exist!");
            }
            return ValidationResult.Success;
        }
    }
     
    class RoleValidations
    {
    }
}
