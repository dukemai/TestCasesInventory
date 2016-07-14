namespace TestCasesInventory.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using TestCasesInventory.Data.Repository;

    internal sealed class Configuration : DbMigrationsConfiguration<TestCasesInventory.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TestCasesInventory.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Teams.Add(new DataModels.TeamDataModel { Name = "Team1" });
            context.Teams.Add(new DataModels.TeamDataModel { Name = "Team2" });
            context.Teams.Add(new DataModels.TeamDataModel { Name = "Team3" });
            context.Users.Add(new DataModels.ApplicationUser
            {
                Email = "user1@email.com",
                UserName = "user1@email.com",
                PasswordHash = "25d55ad283aa400af464c76d713c07ad",//12345678
            });
            context.Users.Add(new DataModels.ApplicationUser
            {
                Email = "user2@email.com",
                UserName = "user2@email.com",
                PasswordHash = "25d55ad283aa400af464c76d713c07ad",//12345678
            });
        }
    }
}
