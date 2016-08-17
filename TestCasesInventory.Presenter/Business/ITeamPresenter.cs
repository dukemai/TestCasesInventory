using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITeamPresenter : IPresenter<TeamViewModel>
    {
        TeamViewModel GetTeamById(int? teamID);
        void InsertTeam(CreateTeamViewModel team);
        void UpdateTeam(int teamID, EditTeamViewModel team);
        void DeleteTeam(int teamID);

        List<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int? teamID);
        List<UsersBelongTeamViewModel> ListUsersBelongTeam(int? teamID);
        void AddUsersToTeam(int teamID, string[] usersNotBelongTeam);
        void RemoveUsersFromTeam(int teamID, string[] usersBelongTeam);
        IPagedList<TeamViewModel> GetTeams(FilterOptions options);
    }
}
