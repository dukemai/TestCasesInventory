using System;
using System.Collections.Generic;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;
using TestCasesInventory.Data.Common;

namespace TestCasesInventory.Presenter.Business
{
    public class TeamPresenter : PresenterBase, ITeamPresenter
    {
        protected ITeamRepository teamRepository;

        public TeamPresenter()
        {
            teamRepository = new TeamRepository();
        }

        public TeamViewModel GetTeamById(int? id)
        {
            if (id.HasValue)
            {
                var team = teamRepository.GetTeamByID(id.Value);
                if (team == null)
                {
                    throw new TeamNotFoundException("Team was not found.");
                }
                return new TeamViewModel
                {
                    ID = team.ID,
                    Name = team.Name,
                    Created = team.Created,
                    CreatedDate = team.CreatedDate,
                    LastModified = team.LastModified,
                    LastModifiedDate = team.LastModifiedDate
                };
            }
            else
            {
                throw new Exception("Id was not valid.");
            }
        }

        public List<TeamViewModel> ListAll()
        {
            var listTeam = teamRepository.ListAll();
            List<TeamViewModel> listTeamView = new List<TeamViewModel>();
            foreach (var item in listTeam)
            {
                var teamView = new TeamViewModel
                {
                    ID = item.ID,
                    Name = item.Name,
                    Created = item.Created,
                    CreatedDate = item.CreatedDate,
                    LastModified = item.LastModified,
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

        public void UpdateTeam(int id, EditTeamViewModel team)
        {
            var teamDataModel = teamRepository.GetTeamByID(id);
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

        public void DeleteTeam(int id)
        {
            var teamDataModel = teamRepository.GetTeamByID(id);
            if (teamDataModel == null)
            {
                throw new TeamNotFoundException("Team was not found.");
            }
            else
            {
                teamRepository.DeleteTeam(id);
                teamRepository.Save();
            }
        }

        public List<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int? teamID)
        {
            if (teamID.HasValue)
            {
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
            else
            {
                throw new Exception("Id was not valid.");
            }
        }

        public List<UsersBelongTeamViewModel> ListUsersBelongTeam(int? teamID)
        {
            if (teamID.HasValue)
            {
                var usersBelongTeam = teamRepository.ListUsersBelongTeam(teamID.Value);
                List<UsersBelongTeamViewModel> listUsersBelongTeamView = new List<UsersBelongTeamViewModel>();
                foreach (var user in usersBelongTeam)
                {
                    var usersBelongTeamView = new UsersBelongTeamViewModel
                    {
                        ID = user.Id,
                        Email = user.Email,
                        DisplayName = user.DisplayName
                    };
                    listUsersBelongTeamView.Add(usersBelongTeamView);
                }
                return listUsersBelongTeamView;
            }
            else
            {
                throw new Exception("Id was not valid.");
            }
        }

        public void AddUsersToTeam(int teamID, string[] usersNotBelongTeam)
        {
            if (usersNotBelongTeam.Length > 0)
            {
                List<ApplicationUser> listUsersBeAddedToTeam = new List<ApplicationUser>();
                foreach (var userID in usersNotBelongTeam)
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

        public void RemoveUsersFromTeam(int teamID, string[] usersBelongTeam)
        {
            if (usersBelongTeam.Length > 0)
            {
                List<ApplicationUser> listUsersBeRemovedFromTeam = new List<ApplicationUser>();
                foreach (var userID in usersBelongTeam)
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
