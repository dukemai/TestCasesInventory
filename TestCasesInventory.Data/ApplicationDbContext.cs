using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
