using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        public DbSet<TestCasesInTestRunDataModel> TestCasesInTestRuns { get; set; }
        public DbSet<TestCaseResultDataModel> TestCaseResults { get; set; }
        public DbSet<TestRunResultDataModel> TestRunResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestRunResultDataModel>()
                .HasRequired(t => t.TestRun)
                .WithMany(t => t.TestRunResults)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestCaseResultDataModel>()
                .HasRequired(t => t.TestRunResult)
                .WithMany(t => t.TestCaseResults)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestCaseResultDataModel>()
                .HasRequired(t => t.User)
                .WithMany(t => t.TestCaseResults)
                .HasForeignKey(t => t.RunBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestCaseResultDataModel>()
               .HasRequired(t => t.TestCase)
               .WithMany(t => t.TestCaseResults)
               .WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestCasesInTestRunDataModel>()
                .HasRequired(c => c.TestRun)
                .WithMany(t => t.TestCasesInTestRuns)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestCasesInTestRunDataModel>()
                .HasRequired(s => s.TestCase)
                .WithMany(t => t.TestCasesInTestRuns)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestCasesInTestRunDataModel>()
                .HasRequired(u => u.ApplicationUser)
                .WithMany(u => u.TestCasesInTestRuns)
                .HasForeignKey(t => t.AssignedTo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TestRunDataModel>()
               .HasRequired(t => t.Team)
               .WithMany(t => t.TestRuns)
               .WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
