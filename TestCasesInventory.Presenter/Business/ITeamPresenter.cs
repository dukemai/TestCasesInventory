using PagedList;
using System.Collections.Generic;
using TestCasesInventory.Common;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Business
{
    public interface ITeamPresenter : IPresenter<TeamViewModel>
    {
        TeamViewModel GetTeamById(int? teamID);
        void InsertTeam(CreateTeamViewModel team);
        void UpdateTeam(int teamID, EditTeamViewModel team);
        void DeleteTeam(int teamID);

        IPagedList<UsersNotBelongTeamViewModel> ListUsersNotBelongTeam(int? teamID, FilterOptions options);
        IPagedList<UsersBelongTeamViewModel> ListUsersBelongTeam(int? teamID, FilterOptions options);
        void AddUsersToTeam(int teamID, string[] usersNotBelongTeam);
        void RemoveUsersFromTeam(int teamID, string[] usersBelongTeam);
        IPagedList<TeamViewModel> GetTeams(FilterOptions options);
        List<TeamViewModel> ListAllTeam();
    }
}
