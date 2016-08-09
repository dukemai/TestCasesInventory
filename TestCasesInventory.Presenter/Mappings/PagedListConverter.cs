using AutoMapper;
using PagedList;
using TestCasesInventory.Common;

namespace TestCasesInventory.Presenter.Mappings
{
    public class PagedListConverter<Source, Destination> : ITypeConverter<IPagedList<Source>, IPagedList<Destination>>
    {
        public IPagedList<Destination> Convert(IPagedList<Source> source, IPagedList<Destination> destination, ResolutionContext context)
        {
            var tempDestination = new CustomPagedList<Destination>();
            tempDestination.CopyFrom<Source>(source as CustomPagedList<Source>);
            return tempDestination;
        }
    }
}
