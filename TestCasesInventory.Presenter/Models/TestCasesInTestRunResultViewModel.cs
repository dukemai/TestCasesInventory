namespace TestCasesInventory.Presenter.Models
{
    public class TestCasesInTestRunResultViewModel : TestCasesInTestRunViewModel
    {
        public string Status { get; set; }
        public string StatusStyleClass
        {
            get
            {
                return string.IsNullOrEmpty(Status) ? "default" : Status.ToLowerInvariant();
            }
        }
        public string Comment { get; set; }
        public string RunBy { get; set; }
    }
}
