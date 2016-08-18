using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Common;
using TestCasesInventory.Data.Config;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class UserRoleRepository : RepositoryBase, IUserRoleRepository
    {
        public UserRoleRepository() : base()
        {

        }

        public IPagedList<ApplicationUser> ListUsersNotBelongRole(string RoleID, FilterOptions options)
        {
            IQueryable<ApplicationUser> query = dataContext.Users.Where(user => user.Roles.All(r => r.RoleId != RoleID));

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

        //public int NumberMemberInRole(int RoleID)
        //{
        //    return dataContext.Users.Where(user => user.RoleID == RoleID).Count();
        //}

        public IPagedList<ApplicationUser> ListUsersBelongRole(string RoleID, FilterOptions options)
        {
            IQueryable<ApplicationUser> query = dataContext.Users.Where(user => user.Roles.Any(r => r.RoleId == RoleID));

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

    }
}
