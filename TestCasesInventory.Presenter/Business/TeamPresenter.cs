using System;
using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using TestCasesInventory.Common;
using PagedList;
using AutoMapper;
using TestCasesInventory.Presenter.Common;
using System.Web.Mvc;

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

        public TeamViewModel GetTeamById(int teamID)
        {
            var team = teamRepository.GetTeamByID(teamID);
            if (team == null)
            {
                logger.Error("Team was not found.");
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
                logger.Debug("Team was not found.");
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
                logger.Error("Team was not found.");
                throw new TeamNotFoundException("Team was not found.");
            }
            else
            {
                teamRepository.DeleteTeam(teamID);
                teamRepository.Save();
            }
        }


        public IPagedList<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int teamID, FilterOptions options)
        {
            var list = teamRepository.ListUsersNotBelongTeam(teamID, options);
            var mappedList = Mapper.Map<IPagedList<UsersNotBelongTeamViewModel>>(list);
            return mappedList;

        }

        public IPagedList<UsersBelongTeamViewModel> ListUsersBelongTeam(int teamID, FilterOptions options)
        {
            var list = teamRepository.ListUsersBelongTeam(teamID, options);
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
                        logger.Error("User was not found.");
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
                        logger.Error("User was not found.");
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

        public List<TeamViewModel> ListAllTeam()
        {
            var listTeamDataModel = teamRepository.ListAll();
            var listTeamViewModel = new List<TeamViewModel>();

            foreach (var item in listTeamDataModel)
            {
                listTeamViewModel.Add(item.MapTo<TeamDataModel, TeamViewModel>());
            }    
            return listTeamViewModel;
        }
    }
}
