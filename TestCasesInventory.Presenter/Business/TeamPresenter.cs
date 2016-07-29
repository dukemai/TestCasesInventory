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

    }
}
