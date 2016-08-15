using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data;

namespace TestCasesInventory.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        #region Constructors

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            if(!Database.Exists())
            Database.SetInitializer(new DataSeed());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #endregion

        #region Tables

        public DbSet<TeamDataModel> Teams { get; set; }
        public DbSet<TestCaseDataModel> TestCases { get; set; }
        public DbSet<TestSuiteDataModel> TestSuites { get; set; }

        #endregion
    }
}
