using AutoMapper;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCaseMappingProfile : Profile
    {
        public TestCaseMappingProfile(string profileName) : base(profileName)
        {

            this.CreateMap<TestCaseDataModel, TestCaseViewModel>();
            this.CreateMap<IPagedList<TestCaseDataModel>, IPagedList<TestCaseViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestCaseDataModel, TestCaseViewModel>>();
        }
    }
}
