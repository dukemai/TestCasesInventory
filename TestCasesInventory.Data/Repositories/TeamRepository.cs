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

        public IEnumerable<ApplicationUser> GetUsersByTeamID(int teamID)
        {
            return dataContext.Users.Where(t => t.TeamID == teamID).ToList();
        }


        public IPagedList<ApplicationUser> ListUsersNotBelongTeam(int teamID, FilterOptions options)
        {
            IQueryable<ApplicationUser> query = dataContext.Users.Where(t => t.TeamID != teamID);

            if (options == null)
            {
                return query.ToCustomPagedList<ApplicationUser>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }
            if (!string.IsNullOrEmpty(options.Keyword))
            {
                foreach (var field in options.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "name":
                            query = query.Where(t => t.DisplayName.Contains(options.Keyword));
                            break;
                        case "mail":
                            query = query.Where(t => t.Email.Contains(options.Keyword));
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
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.DisplayName) : query.OrderByDescending(t => t.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(t => t.Id);
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

        public int NumberMemberInTeam(int teamID)
        {
            return dataContext.Users.Where(user => user.TeamID == teamID).Count();
        }

        public IPagedList<ApplicationUser> ListUsersBelongTeam(int teamID, FilterOptions options)
        {
            IQueryable<ApplicationUser> query = dataContext.Users.Where(t => t.TeamID == teamID);

            if (options == null)
            {
                return query.ToCustomPagedList<ApplicationUser>(DefaultPagingConfig.DefaultPageNumber, DefaultPagingConfig.DefaultPageSize);
            }
            if (!string.IsNullOrEmpty(options.Keyword))
            {
                foreach (var field in options.FilterFields)
                {
                    switch (field.ToLowerInvariant())
                    {
                        case "name":
                            query = query.Where(t => t.DisplayName.Contains(options.Keyword));
                            break;
                        case "mail":
                            query = query.Where(t => t.Email.Contains(options.Keyword));
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
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.DisplayName) : query.OrderByDescending(t => t.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(t => t.Id);
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

        public ApplicationUser FindUserByEmail(string Email)
        {
            return dataContext.Users.Where(u => u.Email == Email).FirstOrDefault();
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
                    case "numbermember":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.ApplicationUsers.Count) : query.OrderByDescending(t => t.ApplicationUsers.Count);
                        break;
                    case "createdby":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.Created) : query.OrderByDescending(t => t.Created);
                        break;
                    case "lastmodifiedby":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.LastModified) : query.OrderByDescending(t => t.LastModified);
                        break;
                    case "created":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
                        break;
                    case "lastmodified":
                        query = sortOptions.Direction == SortDirections.Asc ? query.OrderBy(t => t.LastModifiedDate) : query.OrderByDescending(t => t.LastModifiedDate);
                        break;
                    default:
                        query = query.OrderByDescending(d => d.CreatedDate);
                        break;
                }
                query = OrderByID(query);
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
