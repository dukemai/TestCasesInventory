using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class TeamDataModel : DataModelBase
    {
        [Required]
        public string Name { get; set; }

        //add appication user navigation property
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
