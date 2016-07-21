using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITeamPresenter : IPresenter<TeamViewModel>
    {
        List<TeamViewModel> ListAll();

        TeamViewModel GetTeamById(int id);

        void InsertTeam(CreateTeamViewModel team);

        void UpdateTeam(int id, EditTeamViewModel team);

        void DeleteTeam(int id);

        void Save();
        
    }
}
