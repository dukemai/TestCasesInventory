using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var teams = TeamPresenterObject.ListAll();
            return View("Index", teams);
        }

        // GET: Admin/Team/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var team = TeamPresenterObject.GetById(id);
                return View("Details", team);
            }
            catch(Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Admin/Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string teamName = Request.Form["teamName"];
                var team = new CreateTeamViewModel { Name = teamName};
                TeamPresenterObject.InsertTeam(team);
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
            var model = TeamPresenterObject.GetById(id);
            return View("Edit", model);
        }

        // POST: Admin/Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string teamName = Request.Form["teamName"];
                var updatedTeam = new TeamDetailsViewModel { Name = teamName };
                TeamPresenterObject.UpdateTeam(updatedTeam);
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
            var team = TeamPresenterObject.GetById(id);
            return View("Delete", team);
        }

        // POST: Admin/Team/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                TeamPresenterObject.DeleteTeam(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
