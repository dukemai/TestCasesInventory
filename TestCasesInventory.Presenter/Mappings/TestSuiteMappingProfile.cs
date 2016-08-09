using AutoMapper;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestSuiteMappingProfile : Profile
    {
        ITestCaseRepository testCaseRepository;

        public TestSuiteMappingProfile(string profileName) : base(profileName)
        {
            testCaseRepository = new TestCaseRepository();

            this.CreateMap<TestSuiteDataModel, TestSuiteViewModel>()
                 .ForMember(dest => dest.TestCasesNumber, opt => opt.MapFrom(src => testCaseRepository.TotalTestCasesForTestSuite(src.ID)));
            this.CreateMap<IPagedList<TestSuiteDataModel>, IPagedList<TestSuiteViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestSuiteDataModel, TestSuiteViewModel>>();
        }
    }
}
