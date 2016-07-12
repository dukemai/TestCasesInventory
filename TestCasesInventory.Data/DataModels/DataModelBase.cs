using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class DataModelBase
    {
        [Key]
        public int ID { get; set; }
    }
}
