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

namespace TestCasesInventory.Presenter.Business
{
   

    public class RolePresenter : PresenterBase, IRolePresenter
    {

        #region Properties
        protected HttpContextBase HttpContext;
        protected RoleManager<IdentityRole> RoleManager;


        #endregion

        #region Methods



        public RolePresenter(HttpContextBase context)
        {
            HttpContext = context;

            RoleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        }

      

        public List<IdentityRole> ListRole()
        {            
            return RoleManager.Roles.ToList();
        }




        public IdentityResult CreateRole(string role)
        {
            if (IsRoleExist(role))
            {
                throw new DuplicateNameException("Role Name is exist.");
            }
            return RoleManager.Create(new IdentityRole { Name = role });
        }

        public IdentityResult DeleteRole(string role)
        {
            var choosenRole = RoleManager.FindByName(role);
            
            if(choosenRole == null)
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
    }

    #endregion
}