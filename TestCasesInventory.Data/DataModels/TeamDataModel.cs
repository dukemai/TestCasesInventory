using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class TeamDataModel : DataModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
