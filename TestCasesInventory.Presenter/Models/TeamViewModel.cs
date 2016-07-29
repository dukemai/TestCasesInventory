﻿using System;
using System.ComponentModel.DataAnnotations;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Config;
using TestCasesInventory.Presenter.Validations;

namespace TestCasesInventory.Presenter.Models
{
    public class TeamViewModel : ViewModelBase
    {
        public int ID { get; set; }
        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfTeamName, ErrorMessage = ValidationMessages.ErrorMessageForTeamNameProperty)]
        [Display(Name = "Team Name:")]
        [TeamUniqueValidation]
        public string Name { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class TeamDetailsViewModel : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class CreateTeamViewModel : ViewModelBase
    {
        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfTeamName, ErrorMessage = ValidationMessages.ErrorMessageForTeamNameProperty)]
        [Display(Name = "Team Name:")]
       
        public string Name { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class EditTeamViewModel : ViewModelBase
    {
        [Required]
        [StringLength(ValidationMagicNumbers.MaximumLengthOfTeamName, ErrorMessage = ValidationMessages.ErrorMessageForTeamNameProperty)]
        [Display(Name = "Team Name:")]
        public string Name { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class UsersBelongTeamViewModel : ViewModelBase
    {
        public string Email { get; set; }
    }

    public class UsersNotBelongTeamViewModel : ViewModelBase
    {
        public string Email { get; set; }
    }

}
