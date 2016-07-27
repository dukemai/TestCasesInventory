using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Validations
{
    public class TeamUniqueValidationAttribute : ValidationAttribute
    {
        #region Fields

        private ITeamRepository teamRepository;

        #endregion

        public TeamUniqueValidationAttribute()
        {
            teamRepository = new TeamRepository();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TeamViewModel team = validationContext.ObjectInstance as TeamViewModel;
            if (team == null)
            {
                throw new System.Exception("Expected object is TeamViewmodel while its type is " + validationContext.ObjectInstance.GetType());
            }

            if (string.IsNullOrEmpty(team.Name) || string.IsNullOrWhiteSpace(team.Name))
            {
                return new ValidationResult("Team Name Is Required");
            }

            var existedTeamModel = teamRepository.GetExistedTeamByName(team.Name.Trim());
            var existedTeamModelById = teamRepository.GetExistedTeamByID(team.ID);

            if (!existedTeamModel.Any())
            {
                return ValidationResult.Success;
            }

            if (existedTeamModelById.Any() && existedTeamModelById.First().ID == existedTeamModel.First().ID)
            {
                return ValidationResult.Success;
            }
            if (!existedTeamModelById.Any())
            {
                if (existedTeamModel.Any() && team.ID == 0)
                {
                    return new ValidationResult("Team already exist!");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Team already exist!");
        }

    }
}
