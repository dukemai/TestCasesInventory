﻿using System;
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
using TestCasesInventory.Presenter.Common;

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
            var teamViewModel = team.MapTo<TeamDataModel, TeamViewModel>();
            return teamViewModel;
        }

        public void InsertTeam(CreateTeamViewModel team)
        {
            var teamDataModel = team.MapTo<CreateTeamViewModel, TeamDataModel>();
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
                teamDataModel = team.MapTo<EditTeamViewModel, TeamDataModel>(teamDataModel);
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
                var usersNotBelongTeamView = user.MapTo<ApplicationUser, UsersNotBelongTeamViewModel>();
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
                var usersBelongTeamView = user.MapTo<ApplicationUser, UsersBelongTeamViewModel>();
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
            var mappedList = list.MapTo<IPagedList<TeamDataModel>, IPagedList<TeamViewModel>>();
            return mappedList;
        }
    }
}
