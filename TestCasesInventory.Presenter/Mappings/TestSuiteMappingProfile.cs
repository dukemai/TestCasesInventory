using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestSuiteMappingProfile : Profile
    {
        ITestCaseRepository testCaseRepository;
        UserManager<ApplicationUser> UserManager;
        TeamRepository teamRepository;

        public TestSuiteMappingProfile(string profileName)
            : base(profileName)
        {
            testCaseRepository = new TestCaseRepository();

            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TestSuiteDataModel, TestSuiteViewModel>()
                 .ForMember(dest => dest.TestCasesNumber, opt => opt.MapFrom(src => testCaseRepository.TotalTestCasesForTestSuite(src.ID)))
                 .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName));
            this.CreateMap<IPagedList<TestSuiteDataModel>, IPagedList<TestSuiteViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestSuiteDataModel, TestSuiteViewModel>>();

            this.CreateMap<CreateTestSuiteViewModel, TestSuiteDataModel>()
                .ForMember(dest => dest.TeamID, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).TeamID));

            this.CreateMap<EditTestSuiteViewModel, TestSuiteDataModel>();
        }
    }
}
