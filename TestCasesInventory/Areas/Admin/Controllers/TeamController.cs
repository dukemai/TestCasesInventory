using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    public class TeamController : Controller
    {
        #region Properties

        protected ITeamPresenter TeamPresenterObject;

        #endregion

        #region Constructors

        public TeamController()
        {
            TeamPresenterObject = new TeamPresenter();
        }

        #endregion

        // GET: Admin/Team
        public ActionResult Index()
        {
            var data = TeamPresenterObject.ListAll();
            return View("Index", data);
        }

        // GET: Admin/Team/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: Admin/Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Team/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Team/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Team/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Team/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
