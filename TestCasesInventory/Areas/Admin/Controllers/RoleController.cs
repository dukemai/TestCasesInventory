using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Common;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = PrivilegedUsersConfig.AdminRole)]
    public class RoleController : Web.Common.Base.ControllerBase
    {
        private IRolePresenter rolePresenter;

        protected IRolePresenter RolePresenter
        {
            get
            {
                if (rolePresenter == null)
                {
                    rolePresenter = new RolePresenter(HttpContext);
                }
                return rolePresenter;
            }
        }

        // GET: Admin/Role
        public ActionResult Index()
        {
            var model = RolePresenter.ListRole();
            return View(model);
        }

        //GET 
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                string role = model.Name.Trim();
                RolePresenter.CreateRole(model.Name);
                return RedirectToAction("Index");
            }
            return View();
        }


        //GET
        public ActionResult Edit(string id)
        {
            try
            {
                var model = RolePresenter.GetRoleById(id);
                return View(model);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }

        }



        //POST
        [HttpPost]
        public ActionResult Edit(string id, EditRoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string role = model.Name.Trim();

                    RolePresenter.UpdateRole(id, model.Name);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }


        }


        //GET
        [HttpGet]
        public ActionResult Delete(string id, RoleViewModel model)
        {
            try
            {
                model = RolePresenter.GetRoleById(id);

                return View(model);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }

        }

        //POST
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                RolePresenter.DeleteRole(id);
                return RedirectToAction("Index");
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }

        }


        //GET
        [HttpGet]
        public ActionResult Details(string id)
        {
            try
            {
                var Role = RolePresenter.GetRoleById(id);
                return View(Role);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }




        public ActionResult AssignUsersToRole(string id)
        {
            try
            {
                var role = RolePresenter.GetRoleById(id);
                return View(role);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }

        }

        // GET: Admin/Role/AddUsersToRole/5
        public ActionResult AddUsersToRole(string id, FilterOptions options)
        {
            try
            {
                ViewBag.RoleId = id;
                var Role = RolePresenter.GetRoleById(id);
                var listUsersNotBelongRole = RolePresenter.ListUsersNotBelongRole(id, options);
                return View("_AddUsersToRolePartialView", listUsersNotBelongRole);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/Role/AddUsersToRole/5
        [HttpPost]
        public ActionResult AddUsersToRole(string id, string[] usersNotBelongRole)
        {
            try
            {
                RolePresenter.AddUsersToRole(id, usersNotBelongRole);
                return RedirectToAction("AssignUsersToRole", new { id = id });
            }
            catch (UserNotFoundException e)
            {
                return View("UserNotFoundError");
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return RedirectToAction("AssignUsersToRole", new { id = id });
            }
        }


        // GET: Admin/Role/RemoveUsersFromRole/5
        public ActionResult RemoveUsersFromRole(string id, FilterOptions options)
        {
            try
            {
                ViewBag.RoleId = id;
                var Role = RolePresenter.GetRoleById(id);
                var listUsersBelongRole = RolePresenter.ListUsersBelongRole(id, options);
                return View("_RemoveUsersFromRolePartialView", listUsersBelongRole);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/Role/RemoveUsersFromRole/5
        [HttpPost]
        public ActionResult RemoveUsersFromRole(string id, string[] usersBelongRole)
        {
            try
            {
                RolePresenter.RemoveUsersFromRole(id, usersBelongRole);
                return RedirectToAction("AssignUsersToRole", new { id = id });
            }
            catch (UserNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return RedirectToAction("AssignUsersToRole", new { id = id });
            }
        }

        [HttpGet]
        public ActionResult ListUsersInRole(string RoleID, FilterOptions options)
        {
            try
            {
                var Role = RolePresenter.GetRoleById(RoleID);
                var listUsersInRole = RolePresenter.ListUsersBelongRole(RoleID, options);
                return PartialView("_ListUsersInRolePartial", listUsersInRole);
            }
            catch (RoleNotFoundException e)
            {
                return View("RoleNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        public ActionResult RemoveAccountsFromRole(string RoleID, string[] userBeRemoved)
        {
            try
            {
                RolePresenter.RemoveUsersFromRole(RoleID, userBeRemoved);
                return RedirectToAction("Details", new { id = RoleID });
            }
            catch (UserNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

    }
}