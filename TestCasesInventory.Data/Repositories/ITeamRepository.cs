using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public interface ITeamRepository
    {
        IEnumerable<TeamDataModel> ListAll();
        TeamDataModel GetTeamByID(int teamID);
        void InsertTeam(TeamDataModel team);
        void DeleteTeam(int teamID);
        void UpdateTeam(TeamDataModel team);
        void Save();
        IEnumerable<TeamDataModel> GetExistedTeamByName(string teamName);
        IEnumerable<TeamDataModel> GetExistedTeamByID(int id);

    }
}
