using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
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
        IEnumerable<TeamDataModel> GetTeamsBeSearchedByName(string teamName);

        IEnumerable<ApplicationUser> ListUsersNotBelongTeam(int teamID);
        IEnumerable<ApplicationUser> ListUsersBelongTeam(int teamID);


        void AssignUsersToTeam(IList<ApplicationUser> users, int teamID);
        void RemoveUsersFromTeam(IList<ApplicationUser> users);

        ApplicationUser FindUserByID(string userID);

        IPagedList<TeamDataModel> GetTeams(FilterOptions options);

    }
}
