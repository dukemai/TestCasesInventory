using AutoMapper;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestSuiteMappingProfile : Profile
    {
        public TestSuiteMappingProfile(string profileName) : base(profileName)
        {
            this.CreateMap<TestSuiteDataModel, TestSuiteViewModel>();
            this.CreateMap<IPagedList<TestSuiteDataModel>, IPagedList<TestSuiteViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestSuiteDataModel, TestSuiteViewModel>>();
        }
    }
}
