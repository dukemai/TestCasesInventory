using System;
using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using TestCasesInventory.Data;
using System.Linq;
using System.Collections;

namespace TestCasesInventory.Presenter.Business
{


    public class RolePresenter : PresenterBase, IRolePresenter
    {

        #region Properties
        protected HttpContextBase HttpContext;
        protected RoleManager<IdentityRole> RoleManager;
        protected ApplicationUserManager UserManager;
        #endregion

        #region Methods



        public RolePresenter(HttpContextBase context)
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        }



        public List<IdentityRole> ListRole()
        {
            return RoleManager.Roles.ToList();
        }




        public IdentityResult CreateRole(string role)
        {
            return RoleManager.Create(new IdentityRole { Name = role });
        }

        public IdentityResult DeleteRole(string id)
        {
            var choosenRole = RoleManager.FindById(id);

            if (choosenRole == null)
            {
                throw new RoleNotFoundException();
            }

            return RoleManager.Delete(choosenRole);
        }

        public bool IsRoleExist(string role)
        {
            return RoleManager.RoleExists(role);
        }

        public RoleViewModel GetRoleById(string id)
        {
            var choosenRole = new RoleViewModel();
            var currentRole = RoleManager.FindById(id);
            if (currentRole == null)
            {
                throw new RoleNotFoundException();
            }

            choosenRole.Name = currentRole.Name;


            return choosenRole;
        }

        public IdentityResult UpdateRole(string id, string newRole)
        {
            var choosenRole = RoleManager.FindById(id);

            if (choosenRole == null)
            {
                throw new RoleNotFoundException();
            }

            choosenRole.Name = newRole;

            return RoleManager.Update(choosenRole);
        }

        public List<UsersNotBelongRoleViewModel> ListUsersNotBelongRole(string roleID)
        {
            var role = RoleManager.FindById(roleID);
            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            var ListUserNotInRole = UserManager.Users.Where(user => user.Roles.All(r => r.RoleId != roleID)).ToList();
            var Result = new List<UsersNotBelongRoleViewModel>();
            foreach (var user in ListUserNotInRole)
            {
                Result.Add(new UsersNotBelongRoleViewModel
                {
                    ID = user.Id,
                    Email = user.Email,
                    DisplayName = user.DisplayName
                });
            }
            return Result;
        }

        public List<UsersBelongRoleViewModel> ListUsersBelongRole(string roleID)
        {
            var role = RoleManager.FindById(roleID);
            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            var ListUserInRole = UserManager.Users.Where(user => user.Roles.Any(r => r.RoleId == roleID)).ToList();
            var Result = new List<UsersBelongRoleViewModel>();
            foreach (var user in ListUserInRole)
            {
                Result.Add(new UsersBelongRoleViewModel
                {
                    ID = user.Id,
                    Email = user.Email,
                    DisplayName = user.DisplayName
                });
            }
            return Result;

        }

        public void AddUsersToRole(string RoleID, string[] usersToAddRole)
        {
            var role = RoleManager.FindById(RoleID);
            if (role == null)
            {
                throw new RoleNotFoundException();
            }
            if (usersToAddRole != null)
            {
                foreach (var userID in usersToAddRole)
                {
                    var user = UserManager.FindById(userID);
                    if (user == null)
                    {
                        throw new UserNotFoundException();
                    }
                    else
                    {
                        UserManager.AddToRole(userID, role.Name);
                    }
                }
            }
        }

        public void RemoveUsersFromRole(string RoleID, string[] usersToRemoveRole)
        {
            var role = RoleManager.FindById(RoleID);
            if (role == null)
            {
                throw new RoleNotFoundException();
            }
            if (usersToRemoveRole != null)
            {
                foreach (var userID in usersToRemoveRole)
                {
                    var user = UserManager.FindById(userID);
                    if (user == null)
                    {
                        throw new UserNotFoundException();
                    }
                    else
                    {
                        UserManager.RemoveFromRole(userID, role.Name);
                    }
                }

            }
        }
    }

    #endregion
}