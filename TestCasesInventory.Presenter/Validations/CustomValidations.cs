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
            if (team.Name != null)
            {
                var livingTeam = teamRepository.GetExistedTeamByID(team.ID);
                var existedTeam = teamRepository.GetExistedTeamByName(team.Name.Trim());
                if (existedTeam.Any())
                {
                    if (!livingTeam.Any() || existedTeam.First().ID == livingTeam.First().ID)
                    {
                        if (team.ID != 0)
                            return ValidationResult.Success;
                    }
                    return new ValidationResult("Team already exist!");
                }
            }
            return ValidationResult.Success;
        }

    }
}
