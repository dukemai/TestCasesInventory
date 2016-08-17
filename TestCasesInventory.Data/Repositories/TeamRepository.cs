using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class TeamRepository : RepositoryBase, ITeamRepository, IDisposable
    {
        public TeamRepository()
            : base()
        {
        }

        public IEnumerable<TeamDataModel> ListAll()
        {
            return dataContext.Teams.ToList();
        }

        public TeamDataModel GetTeamByID(int teamID)
        {
            return dataContext.Teams.Find(teamID);
        }

        public void InsertTeam(TeamDataModel team)
        {
            dataContext.Teams.Add(team);
        }

        public void DeleteTeam(int teamID)
        {
            TeamDataModel teamDataModel = dataContext.Teams.Find(teamID);
            dataContext.Teams.Remove(teamDataModel);
        }

        public void UpdateTeam(TeamDataModel team)
        {
            dataContext.Entry(team).State = EntityState.Modified;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public IEnumerable<TeamDataModel> GetExistedTeamByName(string teamName)
        {
            return dataContext.Teams.Where(t => t.Name == teamName).ToList();
        }

        public IEnumerable<TeamDataModel> GetTeamsBeSearchedByName(string teamName)
        {
            return dataContext.Teams.Where(t => t.Name.StartsWith(teamName)).ToList();
        }

        public IEnumerable<ApplicationUser> ListUsersNotBelongTeam(int teamID)
        {
            return dataContext.Users.Where(u => u.TeamID != teamID).ToList();
        }

        public IEnumerable<ApplicationUser> ListUsersBelongTeam(int teamID)
        {
            return dataContext.Users.Where(u => u.TeamID == teamID).ToList();
        }

        public void AssignUsersToTeam(IList<ApplicationUser> users, int teamID)
        {
            users.ToList().ForEach(u => u.TeamID = teamID);
        }

        public void RemoveUsersFromTeam(IList<ApplicationUser> users)
        {
            users.ToList().ForEach(u => u.TeamID = null);
        }

        public ApplicationUser FindUserByID(string userID)
        {
            return dataContext.Users.Find(userID);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {


            if (!this.disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IPagedList<TeamDataModel> GetTeams(FilterOptions options)
        {
            IQueryable<TeamDataModel> query = dataContext.Teams.Select(t => t);

            if (options == null)
            {
                return query.ToCustomPagedList<TeamDataModel>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }
            if (!string.IsNullOrEmpty(options.Keyword))
            {
                foreach (var field in options.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "name":
                            query = query.Where(t => t.Name.Contains(options.Keyword));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (options.SortOptions != null)
            {
                var sortOptions = options.SortOptions;
                switch (sortOptions.Field.ToLowerInvariant())
                {
                    case "name":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                        break;
                    default:
                        query = query.OrderBy(t => t.ID);
                        break;
                }
            }

            if (options.PagingOptions != null)
            {
                var pagingOption = options.PagingOptions;
                return query.ToCustomPagedList(pagingOption.CurrentPage, pagingOption.PageSize);
            }
            return query.ToCustomPagedList(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
        }
    }
}
