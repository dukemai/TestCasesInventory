using System;
using System.ComponentModel.DataAnnotations;

namespace TestCasesInventory.Data.DataModels
{
    public class DataModelBase
    {
        [Key]
        public int ID { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
