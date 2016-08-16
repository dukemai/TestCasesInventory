using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITeamPresenter : IPresenter<TeamViewModel>
    {
        List<TeamViewModel> ListAll();
        TeamViewModel GetTeamById(int? teamID);
        void InsertTeam(CreateTeamViewModel team);
        void UpdateTeam(int teamID, EditTeamViewModel team);
        void DeleteTeam(int teamID);

        List<TeamViewModel> GetTeamsBeSorted(List<TeamViewModel> teams, string sortBy);
        List<TeamViewModel> GetTeamsBeSearchedByName(string searchByName);
        List<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int? teamID);
        List<UsersBelongTeamViewModel> ListUsersBelongTeam(int? teamID);
        void AddUsersToTeam(int teamID, string[] usersNotBelongTeam);
        void RemoveUsersFromTeam(int teamID, string[] usersBelongTeam);
        IPagedList<TeamViewModel> GetTeams(FilterOptions options);
    }
}
