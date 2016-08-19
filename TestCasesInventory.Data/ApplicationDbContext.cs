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
        public DbSet<TestRunDataModel> TestRuns { get; set; }
        public DbSet<TestCaseInTestRunDataModel> TestCaseInTestRuns { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         //   base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestCaseInTestRunDataModel>()
                .HasRequired(c => c.TestRun)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestCaseInTestRunDataModel>()
                .HasRequired(s => s.TestCase)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
