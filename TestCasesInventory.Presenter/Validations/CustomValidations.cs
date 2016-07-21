using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repository;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Validations
{
    public class Unique : ValidationAttribute
    {
        ITeamRepository teamRepository = new TeamRepository();
        static string GetErrorMessage()
        {
            return "Team already exist!";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TeamViewModel team = (TeamViewModel)validationContext.ObjectInstance;
            var existedTeam = teamRepository.ListAll().Where(t => t.Name == team.Name);
            if (existedTeam.ToArray().Length > 0)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

    }
}
