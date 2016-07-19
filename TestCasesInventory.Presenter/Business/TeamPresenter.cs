using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data;
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

        public bool Delete(TeamViewModel obj)
        {
            var teamObj = teamRepository.GetTeamByID(obj.ID);
            if (teamObj == null)
            {
                return false;
            }
            else
            {
                teamRepository.DeleteTeam(obj.ID);
                teamRepository.Save();
                return true;
            }
        }

        public TeamViewModel GetById(int id)
        {
            var team = teamRepository.GetTeamByID(id);
            if (team == null)
            {
                return null;
            }
            return new TeamViewModel
            {
                ID = team.ID,
                Name = team.Name
            };
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

        public void Save(TeamViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
