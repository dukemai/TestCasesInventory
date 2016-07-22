using System.ComponentModel.DataAnnotations;
using TestCasesInventory.Presenter.Validations;

namespace TestCasesInventory.Presenter.Models
{
    public class TeamViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be a string with a maximum length of {1}")]
        [Display(Name = "Team Name:")]
        [TeamUniqueValidation]
        public string Name { get; set; }
    }

    public class TeamDetailsViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class CreateTeamViewModel : ViewModelBase
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be a string with a maximum length of {1}")]
        [Display(Name = "Team Name:")]
       
        public string Name { get; set; }
    }

    public class EditTeamViewModel : ViewModelBase
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be a string with a maximum length of {1}")]
        [Display(Name = "Team Name:")]
        public string Name { get; set; }
    }
}
