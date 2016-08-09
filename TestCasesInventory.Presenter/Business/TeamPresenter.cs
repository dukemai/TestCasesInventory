using System;
using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;


namespace TestCasesInventory.Presenter.Business
{
    public class TeamPresenter : PresenterBase, ITeamPresenter
    {
        protected HttpContextBase HttpContext;
        protected ITeamRepository teamRepository;
        protected ApplicationUserManager UserManager;


        public TeamPresenter(HttpContextBase context)
        {
            HttpContext = context;
            teamRepository = new TeamRepository();
            UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public TeamViewModel GetTeamById(int? teamID)
        {
            if (!teamID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var team = teamRepository.GetTeamByID(teamID.Value);
            if (team == null)
            {
                throw new TeamNotFoundException("Team was not found.");
            }
            var createdBy = UserManager.FindByEmail(team.Created);
            return new TeamViewModel
            {
                ID = team.ID,
                Name = team.Name,
                Created = createdBy != null ? createdBy.DisplayName : string.Empty,
                CreatedDate = team.CreatedDate,
                LastModified = createdBy != null ? createdBy.DisplayName : string.Empty,
                LastModifiedDate = team.LastModifiedDate
            };
        }

        public List<TeamViewModel> ListAll()
        {
            var listTeam = teamRepository.ListAll();
            List<TeamViewModel> listTeamView = new List<TeamViewModel>();
            foreach (var item in listTeam)
            {
                var membersNumber = teamRepository.ListUsersBelongTeam(item.ID).Count();
                var createdBy = UserManager.FindByEmail(item.Created);
                var teamView = new TeamViewModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Created = createdBy != null ? createdBy.DisplayName : string.Empty,
                    MembersNumber = membersNumber,
                    CreatedDate = item.CreatedDate,
                    LastModified = createdBy != null ? createdBy.DisplayName : string.Empty,
                    LastModifiedDate = item.LastModifiedDate
                };
                listTeamView.Add(teamView);
            }
            return listTeamView;
        }
        public void InsertTeam(CreateTeamViewModel team)
        {
            var teamDataModel = new TeamDataModel
            {
                Name = team.Name,
                Created = team.Created,
                CreatedDate = team.CreatedDate,
                LastModified = team.LastModified,
                LastModifiedDate = team.LastModifiedDate
            };
            teamRepository.InsertTeam(teamDataModel);
            teamRepository.Save();
        }

        public void UpdateTeam(int teamID, EditTeamViewModel team)
        {
            var teamDataModel = teamRepository.GetTeamByID(teamID);
            if (teamDataModel == null)
            {
                throw new TeamNotFoundException("Team was not found.");
            }
            else
            {
                teamDataModel.Name = team.Name;
                teamDataModel.LastModified = team.LastModified;
                teamDataModel.LastModifiedDate = team.LastModifiedDate;
                teamRepository.UpdateTeam(teamDataModel);
                teamRepository.Save();
            }
        }

        public void DeleteTeam(int teamID)
        {
            var teamDataModel = teamRepository.GetTeamByID(teamID);
            if (teamDataModel == null)
            {
                throw new TeamNotFoundException("Team was not found.");
            }
            else
            {
                teamRepository.DeleteTeam(teamID);
                teamRepository.Save();
            }
        }

        public List<TeamViewModel> GetTeamsBeSearchedByName(string searchByName)
        {
            var teamsDataModelBeSearched = teamRepository.GetTeamsBeSearchedByName(searchByName);
            List<TeamViewModel> teamsViewBeSearched = new List<TeamViewModel>();
            foreach (var item in teamsDataModelBeSearched)
            {
                var membersNumber = teamRepository.ListUsersBelongTeam(item.ID).Count();
                var teamView = new TeamViewModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Created = item.Created,
                    MembersNumber = membersNumber,
                    CreatedDate = item.CreatedDate,
                    LastModified = item.LastModified,
                    LastModifiedDate = item.LastModifiedDate
                };
                teamsViewBeSearched.Add(teamView);
            }
            return teamsViewBeSearched;
        }

        public List<TeamViewModel> GetTeamsBeSorted(List<TeamViewModel> teams, string sortBy)
        {
            List<TeamViewModel> teamsBeSorted = new List<TeamViewModel>();
            switch (sortBy)
            {
                case "Name desc":
                    teamsBeSorted = teams.OrderByDescending(t => t.Name).ToList();
                    break;
                case "MembersNumber desc":
                    teamsBeSorted = teams.OrderByDescending(t => t.MembersNumber).ToList();
                    break;
                case "MembersNumber asc":
                    teamsBeSorted = teams.OrderBy(t => t.MembersNumber).ToList();
                    break;
                default:
                    teamsBeSorted = teams.OrderBy(t => t.Name).ToList();
                    break;
            }
            return teamsBeSorted;
        }


        public List<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int? teamID)
        {
            if (!teamID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var usersNotBelongTeam = teamRepository.ListUsersNotBelongTeam(teamID.Value);
            List<UsersNotBelongTeamViewModel> listUsersNotBelongTeamView = new List<UsersNotBelongTeamViewModel>();
            foreach (var user in usersNotBelongTeam)
            {
                string nameOfTeamManageUser = null;
                if (user.TeamID.HasValue)
                {
                    nameOfTeamManageUser = teamRepository.GetTeamByID(user.TeamID.Value).Name;
                }
                var usersNotBelongTeamView = new UsersNotBelongTeamViewModel
                {
                    ID = user.Id,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    TeamName = nameOfTeamManageUser
                };
                listUsersNotBelongTeamView.Add(usersNotBelongTeamView);
            }
            return listUsersNotBelongTeamView;
        }

        public List<UsersBelongTeamViewModel> ListUsersBelongTeam(int? teamID)
        {
            if (!teamID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var usersBelongTeam = teamRepository.ListUsersBelongTeam(teamID.Value);
            List<UsersBelongTeamViewModel> listUsersBelongTeamView = new List<UsersBelongTeamViewModel>();
            foreach (var user in usersBelongTeam)
            {
                var usersBelongTeamView = new UsersBelongTeamViewModel
                {
                    ID = user.Id,
                    TeamID = user.TeamID.Value,
                    Email = user.Email,
                    DisplayName = user.DisplayName
                };
                listUsersBelongTeamView.Add(usersBelongTeamView);
            }
            return listUsersBelongTeamView;
        }

        public void AddUsersToTeam(int teamID, string[] usersToAdd)
        {
            if (usersToAdd != null)
            {
                List<ApplicationUser> listUsersBeAddedToTeam = new List<ApplicationUser>();
                foreach (var userID in usersToAdd)
                {
                    var user = teamRepository.FindUserByID(userID);
                    if (user == null)
                    {
                        throw new UserNotFoundException("User was not found.");
                    }
                    else
                    {
                        listUsersBeAddedToTeam.Add(user);
                    }
                }
                teamRepository.AssignUsersToTeam(listUsersBeAddedToTeam, teamID);
                teamRepository.Save();
            }
        }

        public void RemoveUsersFromTeam(int teamID, string[] usersToRemove)
        {
            if (usersToRemove != null)
            {
                List<ApplicationUser> listUsersBeRemovedFromTeam = new List<ApplicationUser>();
                foreach (var userID in usersToRemove)
                {
                    var user = teamRepository.FindUserByID(userID);
                    if (user == null)
                    {
                        throw new UserNotFoundException("User was not found.");
                    }
                    else
                    {
                        listUsersBeRemovedFromTeam.Add(user);
                    }
                }
                teamRepository.RemoveUsersFromTeam(listUsersBeRemovedFromTeam);
                teamRepository.Save();
            }
        }
    }
}
