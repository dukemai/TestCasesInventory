using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Validations
{
    public class TeamUniqueValidationAttribute : ValidationAttribute
    {
        ITeamRepository teamRepository = new TeamRepository();
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TeamViewModel team = validationContext.ObjectInstance as TeamViewModel;
            var existedTeam = teamRepository.ListAll().Where(t => t.Name == team.Name);
            if (existedTeam.ToArray().Length > 0)
            {
                return new ValidationResult("Team already exist!");
            }
            return ValidationResult.Success;
        }

    }
}
