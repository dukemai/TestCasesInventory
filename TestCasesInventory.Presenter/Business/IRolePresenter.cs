﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface IRolePresenter
    {


        List<RoleViewModel> ListRole();

        RoleViewModel GetRoleById(string id);

        bool IsRoleExist(string role);

        IdentityResult CreateRole(string role);

        IdentityResult UpdateRole(string id, string newRole);

        IdentityResult DeleteRole(string id);

        IPagedList<UsersNotBelongRoleViewModel> ListUsersNotBelongRole(string roleId, FilterOptions options);

        IPagedList<UsersBelongRoleViewModel> ListUsersBelongRole(string roleId, FilterOptions options);

        void AddUsersToRole(string RoleID, string[] usersToAddRole);

        void RemoveUsersFromRole(string RoleID, string[] usersToRemoveRole);
    }
}

