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

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
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
                try
                {
                    RolePresenter.CreateRole(model.Name);
                    return RedirectToAction("Index");
                }
                catch (DuplicateNameException e)
                {
                    ViewBag.Message = e.Message;
                }
                
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
                var role = RolePresenter.GetRoleById(id);
                RolePresenter.DeleteRole(role.Name);
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
        public ActionResult Details()
        {
            return View();
        }

    }
}