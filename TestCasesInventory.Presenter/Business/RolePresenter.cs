﻿using System.Collections.Generic;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System;
using PagedList;
using TestCasesInventory.Common;
using AutoMapper;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Common;

namespace TestCasesInventory.Presenter.Business
{


    public class RolePresenter : PresenterBase, IRolePresenter
    {

        #region Properties
        protected HttpContextBase HttpContext;
        protected RoleManager<IdentityRole> RoleManager;
        protected ApplicationUserManager UserManager;
        protected UserRoleRepository UserRepository;
        #endregion

        #region Methods



        public RolePresenter(HttpContextBase context)
        {
            HttpContext = context;
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            UserRepository = new UserRoleRepository();
        }



        public List<RoleViewModel> ListRole()
        {
            var listRoleViewModel = new List<RoleViewModel>();
            var roleManager = RoleManager.Roles.ToList();
            foreach (var item in roleManager)
            {
                var Role = item.MapTo<IdentityRole, RoleViewModel>();
                listRoleViewModel.Add(Role);
            }
            return listRoleViewModel;
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
                logger.Error("Role was not found");
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
            var currentRole = RoleManager.FindById(id);
            if (currentRole == null)
            {
                logger.Error("Role was not found");
                throw new RoleNotFoundException();
            }

            var choosenRole = currentRole.MapTo<IdentityRole, RoleViewModel>();
            return choosenRole;
        }

        public IdentityResult UpdateRole(string id, string newRole)
        {
            var choosenRole = RoleManager.FindById(id);

            if (choosenRole == null)
            {
                logger.Error("Role was not found");
                throw new RoleNotFoundException();
            }

            choosenRole.Name = newRole;

            return RoleManager.Update(choosenRole);
        }

        IPagedList<UsersNotBelongRoleViewModel> IRolePresenter.ListUsersNotBelongRole(string roleId, FilterOptions options)
        {
            var role = RoleManager.FindById(roleId);
            if (role == null)
            {
                logger.Error("Role was not found");
                throw new RoleNotFoundException();
            }

            //var ListUserNotInRole = UserManager.Users.Where(user => user.Roles.All(r => r.RoleId != roleId)).ToList();
            //var Result = new List<UsersNotBelongRoleViewModel>();
            //foreach (var user in ListUserNotInRole)
            //{
            //    Result.Add(user.MapTo<ApplicationUser, UsersNotBelongRoleViewModel>());
            //}

            var list = UserRepository.ListUsersNotBelongRole(roleId, options);
            var Result = list.MapTo<IPagedList<ApplicationUser>, IPagedList<UsersNotBelongRoleViewModel>>();
            return Result;
        }

        IPagedList<UsersBelongRoleViewModel> IRolePresenter.ListUsersBelongRole(string roleId, FilterOptions options)
        {
            var role = RoleManager.FindById(roleId);
            if (role == null)
            {
                logger.Error("Role was not found");
                throw new RoleNotFoundException();
            }

            //var ListUserInRole = UserManager.Users.Where(user => user.Roles.Any(r => r.RoleId == roleId)).ToList();
            var list = UserRepository.ListUsersBelongRole(roleId, options);
            var Result = list.MapTo<IPagedList<ApplicationUser>, IPagedList<UsersBelongRoleViewModel>>();
            //foreach (var user in ListUserInRole)
            //{
            //    Result.Add(user.MapTo<ApplicationUser, UsersBelongRoleViewModel>());
            //}
            return Result;

        }

        public void AddUsersToRole(string RoleID, string[] usersToAddRole)
        {
            var role = RoleManager.FindById(RoleID);
            if (role == null)
            {
                logger.Error("Role was not found");
                throw new RoleNotFoundException();
            }
            if (usersToAddRole != null)
            {
                foreach (var userID in usersToAddRole)
                {
                    var user = UserManager.FindById(userID);
                    if (user == null)
                    {
                        logger.Error("Use was not found");
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
                logger.Error("Role was not found");
                throw new RoleNotFoundException();
            }
            if (usersToRemoveRole != null)
            {
                foreach (var userID in usersToRemoveRole)
                {
                    var user = UserManager.FindById(userID);
                    if (user == null)
                    {
                        logger.Error("User was not found");
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