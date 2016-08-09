using System.Collections.Generic;

namespace TestCasesInventory.Presenter.Models
{
    public class FilterViewModel : ViewModelBase
    {
        public string FilterKeyword { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Area { get; set; }

        public List<KeyValuePair<string, string>> FilterFields { get; set; }
    }
}
