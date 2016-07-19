using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Data.Repository
{
    public class RepositoryBase
    {
        protected ApplicationDbContext dataContext;

        public RepositoryBase()
        {
            dataContext = new ApplicationDbContext();
        }
    }
}
