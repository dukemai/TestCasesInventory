using TestCasesInventory.Common;

namespace TestCasesInventory.Web.Common.Utils
{
    public class PagingHelper
    {
        public static PagingOptions DefaultPagingOptions
        {
            get
            {
                return new PagingOptions
                {
                    CurrentPage = PagingConfig.PageNumber,
                    PageSize = PagingConfig.PageSize
                };
            }
        }

        public static FilterOptions DefaultFilterOptions
        {
            get
            {
                return new FilterOptions
                {
                    FilterFields = new string[0],
                    PagingOptions = DefaultPagingOptions,
                    Keyword = string.Empty,
                    SortOptions = DefaultSortOptions
                };
            }
        }

        public static SortOptions DefaultSortOptions
        {
            get
            {
                return new SortOptions
                {
                    Field = "CreatedDate",
                    Direction = SortDirections.Desc
                };
            }
        }
    }
}
