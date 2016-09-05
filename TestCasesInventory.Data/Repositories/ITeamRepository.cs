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
        IEnumerable<ApplicationUser> GetUsersByTeamID(int teamID);

        IPagedList<ApplicationUser> ListUsersNotBelongTeam(int teamID, FilterOptions options);
        int NumberMemberInTeam(int teamID);
        IPagedList<ApplicationUser> ListUsersBelongTeam(int teamID, FilterOptions options);


        void AssignUsersToTeam(IList<ApplicationUser> users, int teamID);
        void RemoveUsersFromTeam(IList<ApplicationUser> users);

        ApplicationUser FindUserByID(string userID);
        ApplicationUser FindUserByEmail(string Email);

        IPagedList<TeamDataModel> GetTeams(FilterOptions options);

    }
}
