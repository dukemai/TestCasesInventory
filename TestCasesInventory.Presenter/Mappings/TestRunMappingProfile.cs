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
    public class TestRunMappingProfile: Profile
    {
        private ITestCasesInTestRunRepository testCasesInTestRunRepository;
        private UserManager<ApplicationUser> UserManager;

        public TestRunMappingProfile(string profileName) : base(profileName)
        {
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TestRunDataModel, TestRunViewModel>()
                 .ForMember(dest => dest.TestCasesNumber, opt => opt.MapFrom(src => testCasesInTestRunRepository.TotalTestCasesInTestRun(src.ID)))
                 .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName));
            this.CreateMap<IPagedList<TestRunDataModel>, IPagedList<TestRunViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestRunDataModel, TestRunViewModel>>();

            this.CreateMap<CreateTestRunViewModel, TestRunDataModel>()
                .ForMember(dest => dest.TeamID, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).TeamID));

            this.CreateMap<EditTestRunViewModel, TestRunDataModel>();
        }
    }
}
