using AutoMapper;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCasesInTestRunMappingProfile : Profile
    {
        public TestCasesInTestRunMappingProfile(string profileName) : base(profileName)
        {
            this.CreateMap<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>();

            this.CreateMap<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>>();

            this.CreateMap<CreateTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>();

        }

    }
}
