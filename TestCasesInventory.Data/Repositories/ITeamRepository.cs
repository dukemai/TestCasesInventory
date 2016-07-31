using System.Collections.Generic;
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
        IEnumerable<ApplicationUser> ListUsersNotBelongTeam();
        IEnumerable<ApplicationUser> ListUsersBelongTeam(int teamId);
        void AssignUsersToTeam(ApplicationUser user);
        ApplicationUser FindUserByID(string userID);
    }
}
