using AutoMapper;
using PagedList;

namespace TestCasesInventory.Presenter.Common
{
    public static class MapperExtensions
    {
        public static Destination MapTo<Source,Destination>(this Source source)
        {
            return Mapper.Map<Destination>(source);
        }
    }
}
