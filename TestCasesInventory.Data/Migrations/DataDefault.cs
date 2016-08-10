using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestCasesInventory.Data.DataModels;


namespace TestCasesInventory.Data.Migrations
{
    public class DataDefault 
    {

        public static void DataSetup()
        {
            var dataContext = new ApplicationDbContext();
             
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dataContext));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dataContext));

            var roleAdmin = RoleManager.FindByName("Admin");
            if(roleAdmin == null)
            {
                roleAdmin = new IdentityRole() { Name = "Admin" };
                RoleManager.Create(roleAdmin);
            }

            var roleTester = RoleManager.FindByName("Tester");
            if (roleTester == null)
            {
                roleTester = new IdentityRole() { Name = "Tester" };
                RoleManager.Create(roleTester);
            }

            var userAdmin = UserManager.FindByName("admin@gmail.com");
            if(userAdmin == null)
            {
                userAdmin = new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    DisplayName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    LastModifiedDate = DateTime.Now
                };

                UserManager.Create(userAdmin, "12345678");
                //UserManager.AddToRole(userAdmin.Id, roleAdmin.Name);
            }
            UserManager.AddToRole(userAdmin.Id, roleAdmin.Name);
        }

    }
}
