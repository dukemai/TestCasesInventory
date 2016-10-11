using System;
using System.Web.Mvc;
using TestCasesInventory.Data.Common;
using TestCasesInventory.Presenter.Business;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Presenter.Validations;
using TestCasesInventory.Bindings;
using TestCasesInventory.Common;
using TestCasesInventory.Web.Common.Utils;

namespace TestCasesInventory.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = PrivilegedUsersConfig.AdminRole)]
    public class TeamController : Controller
    {
        #region Properties

        private ITeamPresenter teamPresenterObject;
        protected ITeamPresenter TeamPresenterObject
        {
            get
            {
                if (teamPresenterObject == null)
                {
                    teamPresenterObject = new TeamPresenter(HttpContext);
                }
                return teamPresenterObject;
            }
        }

        #endregion

        // GET: Admin/Team
        public ActionResult Index([ModelBinder(typeof(FilterOptionsBinding))] FilterOptions filterOptions)
        {
            var Teams = TeamPresenterObject.GetTeams(filterOptions);
            return View("Index", Teams);
        }

        // GET: Admin/Team/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var team = TeamPresenterObject.GetTeamById(id.Value);
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
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var updatedTeam = TeamPresenterObject.GetTeamById(id.Value);
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
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var deletedTeam = TeamPresenterObject.GetTeamById(id.Value);
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

        public ActionResult AssignUsersToTeam(int? id, TabOptions tabOptions)
        {
            if (!id.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var teamName = TeamPresenterObject.GetTeamById(id.Value).Name;
            var model = new AssignUsersToTeamViewModel { TeamName = teamName };
            model.Tabs.Add(new TabViewModel
            {
                ID = "UsersBelongingToTheTeam",
                Name = "List Users in Team",
                Action = "RemoveUsersFromTeam",
                Controller = "Team",
                TabIndex = 0,
                IsActive = tabOptions.ActiveTab == 0
            });
            model.Tabs.Add(new TabViewModel
            {
                ID = "UsersNotBelongingToTheTeam",
                Name = "List Users NOT belonging to the Team",
                Action = "AddUsersToTeam",
                Controller = "Team",
                TabIndex = 1,
                IsActive = tabOptions.ActiveTab == 1
            });
           
            return View(model);
        }

        // GET: Admin/Team/AddUsersToTeam/5
        public ActionResult AddUsersToTeam(int? id, FilterOptions options, TabOptions tabOptions)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var team = TeamPresenterObject.GetTeamById(id.Value);
                //default tab is 1 --> if tab is not active then we use default filter
                options = tabOptions.ActiveTab != tabOptions.CurrentTabIndex ? PagingHelper.DefaultFilterOptions : options;
                var listUsersNotBelongTeam = TeamPresenterObject.ListUsersNotBelongTeam(id.Value, options);
                ViewBag.TeamID = id;
                ViewBag.CurrentTabIndex = tabOptions.CurrentTabIndex;

                return View("Team/AddUsersToTeam", listUsersNotBelongTeam);
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
        public ActionResult RemoveUsersFromTeam(int? id, FilterOptions options, TabOptions tabOptions)
        {
            try
            {
                if (!id.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var team = TeamPresenterObject.GetTeamById(id.Value);
                //default tab is 0 --> if tab is not active then we use default filter
                options = tabOptions.ActiveTab != tabOptions.CurrentTabIndex ? PagingHelper.DefaultFilterOptions : options;
                var listUsersBelongTeam = TeamPresenterObject.ListUsersBelongTeam(id.Value, options);

                ViewBag.TeamID = id;
                ViewBag.CurrentTabIndex = tabOptions.CurrentTabIndex;

                return View("Team/RemoveUsersFromTeam", listUsersBelongTeam);
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
        public ActionResult RemoveUsersFromTeam(int id, string[] usersToRemove)
        {
            try
            {
                TeamPresenterObject.RemoveUsersFromTeam(id, usersToRemove);
                return RedirectToAction("AssignUsersToTeam", new { id = id });
            }
            catch (UserNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }


        [HttpGet]
        public ActionResult ListMembersInTeam(int? teamID, FilterOptions options)
        {
            try
            {
                if (!teamID.HasValue)
                {
                    throw new Exception("Id was not valid.");
                }
                var team = TeamPresenterObject.GetTeamById(teamID.Value);
                var listMembersInTeam = TeamPresenterObject.ListUsersBelongTeam(teamID.Value, options);
                return PartialView("Team/ListMembersInTeamPartial", listMembersInTeam);
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

        public ActionResult RemoveMembersFromTeam(int teamID, string[] memberBeRemoved)
        {
            try
            {
                TeamPresenterObject.RemoveUsersFromTeam(teamID, memberBeRemoved);
                return RedirectToAction("Details", new { id = teamID });
            }
            catch (UserNotFoundException e)
            {
                return View("ResultNotFoundError");
            }
        }

    }
}
