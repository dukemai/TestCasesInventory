using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IRolePresenter
    {


        List<IdentityRole> ListRole();

        RoleViewModel GetRoleById(string id);

        bool IsRoleExist(string role);

        IdentityResult CreateRole(string role);

        IdentityResult UpdateRole(string id, string newRole);

        IdentityResult DeleteRole(string role);


    }
}

