using System.Linq;
using TestCasesInventory.Data.DataModels;

namespace TestCasesInventory.Data.Repositories
{
    public class RepositoryBase
    {
        protected ApplicationDbContext dataContext;

        public RepositoryBase()
        {
            dataContext = new ApplicationDbContext();
        }

        public virtual IQueryable<T> OrderByID<T>(IQueryable<T> query) where T : DataModelBase
        {
            query = (query as IOrderedQueryable<T>).ThenByDescending(t => t.ID);
            return query;
        }

        public virtual IQueryable<ApplicationUser> OrderByID(IQueryable<ApplicationUser> query)
        {
            query = (query as IOrderedQueryable<ApplicationUser>).ThenByDescending(t => t.Id);
            return query;
        }
    }
}
