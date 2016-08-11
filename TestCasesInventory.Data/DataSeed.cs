using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Common;

namespace TestCasesInventory.Data
{
    public class DataSeed : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleAdmin = RoleManager.FindByName(PrivilegedUsersConfig.AdminRole);
            if (roleAdmin == null)
            {
                roleAdmin = new IdentityRole() { Name = PrivilegedUsersConfig.AdminRole };
                RoleManager.Create(roleAdmin);
            }

            var roleTester = RoleManager.FindByName(PrivilegedUsersConfig.TesterRole);
            if (roleTester == null)
            {
                roleTester = new IdentityRole() { Name = PrivilegedUsersConfig.TesterRole };
                RoleManager.Create(roleTester);
            }

            var userAdmin = UserManager.FindByName(FirstAdminUserDefault.UserName);
            if (userAdmin == null)
            {
                userAdmin = new ApplicationUser()
                {
                    UserName = FirstAdminUserDefault.UserName,
                    DisplayName = FirstAdminUserDefault.DisplayName,
                    Email = FirstAdminUserDefault.Email,
                    EmailConfirmed = FirstAdminUserDefault.EmailConfirmed,
                    PhoneNumberConfirmed = FirstAdminUserDefault.PhoneNumberConfirmed,
                    LockoutEnabled = FirstAdminUserDefault.LockoutEnabled,
                    AccessFailedCount = FirstAdminUserDefault.AccessFailedCount,
                    TwoFactorEnabled = FirstAdminUserDefault.TwoFactorEnabled,
                    LastModifiedDate = DateTime.Now
                };

                UserManager.Create(userAdmin, FirstAdminUserDefault.Password);
            }
            UserManager.AddToRole(userAdmin.Id, roleAdmin.Name);
        }
    }
}
