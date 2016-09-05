using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesInventory.Presenter.Models
{
    public class AssignUsersToRoleViewModel
    {
        public List<TabViewModel> Tabs { get; set; }
        public string RoleName { get; set; }

        public AssignUsersToRoleViewModel()
        {
            Tabs = new List<TabViewModel>();
        }

    }
}
