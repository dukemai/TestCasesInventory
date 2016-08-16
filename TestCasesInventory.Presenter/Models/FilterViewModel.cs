using System.Collections.Generic;
using TestCasesInventory.Common;

namespace TestCasesInventory.Presenter.Models
{
    public class FilterViewModel : ViewModelBase
    {
        public FilterOptions FilterOptions { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Area { get; set; }

        public List<FilterOptionViewModel> FilterFields { get; set; }
    }

    public class FilterOptionViewModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public bool IsChecked { get; set; }
    }
}
