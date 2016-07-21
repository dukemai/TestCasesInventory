using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repository;
using TestCasesInventory.Presenter.Models;

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
            int id_Valid;
            if (id.HasValue)
            {
                id_Valid = (int)id;
                var team = teamRepository.GetTeamByID(id_Valid);
                return new TeamViewModel
                {
                    ID = team.ID,
                    Name = team.Name
                };
            }
            else
            {
                throw new Exception();
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
                    Name = item.Name
                };
                listTeamView.Add(teamView);
            }
            return listTeamView;
        }
        public void InsertTeam(CreateTeamViewModel team)
        {
            var teamDataModel = new TeamDataModel
            {
                Name = team.Name
            };
            teamRepository.InsertTeam(teamDataModel);
            teamRepository.Save();
        }

        public void UpdateTeam(int id, EditTeamViewModel team)
        {
            var teamDataModel = new TeamDataModel
            {
                ID = id,
                Name = team.Name
            };
            teamRepository.UpdateTeam(teamDataModel);
            teamRepository.Save();
        }

        public void DeleteTeam(int id)
        {
            teamRepository.DeleteTeam(id);
            teamRepository.Save();
        }

        public void Save()
        {
            teamRepository.Save();
        }

    }
}
