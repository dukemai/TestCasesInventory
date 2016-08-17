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
using TestCasesInventory.Common;
using PagedList;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Data;

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
            var teamViewModel = Mapper.Map<TeamViewModel>(team);
            return teamViewModel;
        }

        public void InsertTeam(CreateTeamViewModel team)
        {
            var teamDataModel = new TeamDataModel();
            teamDataModel = Mapper.Map(team, teamDataModel);
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
                teamDataModel = Mapper.Map(team, teamDataModel);
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


        public IPagedList<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int? teamID, FilterOptions options)
        {
            if (!teamID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }

            var list = teamRepository.ListUsersNotBelongTeam(teamID.Value, options);
            var mappedList = Mapper.Map<IPagedList<UsersNotBelongTeamViewModel>>(list);
            return mappedList;

        }

        public IPagedList<UsersBelongTeamViewModel> ListUsersBelongTeam(int? teamID, FilterOptions options)
        {
            if (!teamID.HasValue)
            {
                throw new Exception("Id was not valid.");
            }
            var list = teamRepository.ListUsersBelongTeam(teamID.Value, options);
            var mappedList = Mapper.Map<IPagedList<UsersBelongTeamViewModel>>(list);
            return mappedList;
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
                        UserManager.AddToRoles(user.Id, PrivilegedUsersConfig.TesterRole);
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
                        UserManager.RemoveFromRole(user.Id, PrivilegedUsersConfig.TesterRole);
                        listUsersBeRemovedFromTeam.Add(user);
                    }
                }
                teamRepository.RemoveUsersFromTeam(listUsersBeRemovedFromTeam);
                teamRepository.Save();
            }
        }

        public IPagedList<TeamViewModel> GetTeams(FilterOptions options)
        {
            var list = teamRepository.GetTeams(options);
            var mappedList = Mapper.Map<IPagedList<TeamViewModel>>(list);
            return mappedList;
        }
    }
}
