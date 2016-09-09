using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCasesInTestRunResultMappingProfile : Profile
    {
        private UserManager<ApplicationUser> UserManager;
        private TestCaseRepository testCaseRepository;
        private ITestSuiteRepository testSuiteRepository;
        public TestCasesInTestRunResultMappingProfile(string profileName) : base(profileName)
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            testSuiteRepository = new TestSuiteRepository();
            testCaseRepository = new TestCaseRepository();

            this.CreateMap<TestCasesInTestRunDataModel, TestCasesInTestRunResultViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => UserManager.FindById(src.AssignedTo).DisplayName))
                .ForMember(dest => dest.AssignedBy, opt => opt.MapFrom(src => UserManager.FindById(src.AssignedBy).DisplayName))
                .ForMember(dest => dest.TestSuiteTitle, opt => opt.MapFrom(src => testSuiteRepository.GetTestSuiteByID(src.TestSuiteID).Title));
            this.CreateMap<TestCaseResultDataModel, TestCasesInTestRunResultViewModel>();

        }

    }
}
