using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface IUserRoleRepository
    {
        IPagedList<ApplicationUser> ListUsersNotBelongRole(string RoleID, FilterOptions options);

        IPagedList<ApplicationUser> ListUsersBelongRole(string RoleID, FilterOptions options);
    }
}
