using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public class TeamPresenter : PresenterBase, ITeamPresenter
    {
        public TeamPresenter()
            : base()
        {
        }

        public bool Delete(TeamViewModel obj)
        {
            throw new NotImplementedException();
        }

        public TeamViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TeamViewModel> ListAll()
        {
            return DataContext.Teams.Select(t => new TeamViewModel
            {
                ID = t.ID,
                Name = t.Name
            }).ToList();
        }

        public void Save(TeamViewModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
