using System.Collections.Generic;

namespace TestCasesInventory.Presenter.Models
{
    public class AssignUsersToTeamViewModel
    {
        public AssignUsersToTeamViewModel()
        {
            Tabs = new List<TabViewModel>();
        }
        public List<TabViewModel> Tabs { get; set; }
        public string TeamName { get; set; }
    }
}
