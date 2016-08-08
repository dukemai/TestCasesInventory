using System;
using System.Web.Mvc;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Web.Common;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = PrivilegedUsersConfig.AdminRole)]
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
        public ActionResult Details(int? id)
        {
            try
            {
                var team = TeamPresenterObject.GetTeamById(id);
                return View("Details", team);
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
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
        public ActionResult Create([Bind(Include = "Name")] TeamViewModel team)
        {
            if (ModelState.IsValid)
            {
                string teamName = team.Name.Trim();
                var createdTeam = new CreateTeamViewModel
                {
                    Name = teamName,
                    Created = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    LastModified = User.Identity.Name,
                    LastModifiedDate = DateTime.Now
                };
                TeamPresenterObject.InsertTeam(createdTeam);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Team/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                var updatedTeam = TeamPresenterObject.GetTeamById(id);
                return View("Edit", updatedTeam);
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Name, ID")] TeamViewModel team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string teamName = team.Name;
                    var updatedTeam = new EditTeamViewModel
                    {
                        Name = teamName,
                        LastModified = User.Identity.Name,
                        LastModifiedDate = DateTime.Now
                    };
                    TeamPresenterObject.UpdateTeam(id, updatedTeam);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        // GET: Admin/Team/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var deletedTeam = TeamPresenterObject.GetTeamById(id);
                return View("Delete", deletedTeam);
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/Team/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                TeamPresenterObject.DeleteTeam(id);
                return RedirectToAction("Index");
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

        public ActionResult AssignUsersToTeam(int? id)
        {
            ViewBag.TeamName = TeamPresenterObject.GetTeamById(id).Name;
            return View();
        }

        // GET: Admin/Team/AddUsersToTeam/5
        public ActionResult AddUsersToTeam(int? id)
        {
            try
            {
                ViewBag.TeamID = id;
                var team = TeamPresenterObject.GetTeamById(id);
                var listUsersNotBelongTeam = TeamPresenterObject.ListUsersNotBelongTeam(id);
                return View("AddUsersToTeam", listUsersNotBelongTeam);
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/Team/AddUsersToTeam/5
        [HttpPost]
        public ActionResult AddUsersToTeam(int id, string[] usersToAdd)
        {
            try
            {
                TeamPresenterObject.AddUsersToTeam(id, usersToAdd);
                return RedirectToAction("AssignUsersToTeam", new { id = id });
            }
            catch (UserNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }


        // GET: Admin/Team/RemoveUsersFromTeam/5
        public ActionResult RemoveUsersFromTeam(int? id)
        {
            try
            {
                ViewBag.TeamID = id;
                var team = TeamPresenterObject.GetTeamById(id);
                var listUsersBelongTeam = TeamPresenterObject.ListUsersBelongTeam(id);
                return View("RemoveUsersFromTeam", listUsersBelongTeam);
            }
            catch (TeamNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            catch (Exception e)
            {
                return View("ResultNotFoundError");
            }
        }

        // POST: Admin/Team/RemoveUsersFromTeam/5
        [HttpPost]
        public ActionResult RemoveUsersFromTeam(int id, string[] usersToRemove)
        {
            try
            {
                TeamPresenterObject.RemoveUsersFromTeam(id, usersToRemove);
                return RedirectToAction("AssignUsersToTeam", new { id = id});
            }
            catch (UserNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
            
        }
    }
}
