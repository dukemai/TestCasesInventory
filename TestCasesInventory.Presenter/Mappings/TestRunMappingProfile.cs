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
    public class TestRunMappingProfile : Profile
    {

        ITestCasesInTestRunRepository testCasesInTestRunRepository;
        UserManager<ApplicationUser> UserManager;
        TeamRepository teamRepository;

        public TestRunMappingProfile(string profileName) : base(profileName)
        {
            teamRepository = new TeamRepository();
            testCasesInTestRunRepository = new TestCasesInTestRunRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TestRunDataModel, TestRunViewModel>()
                 .ForMember(dest => dest.TestCasesNumber, opt => opt.MapFrom(src => testCasesInTestRunRepository.TotalTestCasesInTestRun(src.ID)))
                 .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName))
                 .ForMember(dest => dest.CreateDisplayOnly, opt =>
                  {
                      opt.MapFrom(src => string.IsNullOrEmpty(src.CreateDisplayOnly) ? teamRepository.FindUserByEmail(src.Created).DisplayName : src.CreateDisplayOnly);
                  })
                 .ForMember(dest => dest.LastModifiedDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.LastModifiedDisplayOnly) ? teamRepository.FindUserByEmail(src.LastModifiedDisplayOnly).DisplayName : src.LastModifiedDisplayOnly);
                 })
                 .ForMember(dest => dest.TeamNameDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.TeamNameDisplayOnly) ? teamRepository.GetTeamByID(src.TeamID).Name : src.TeamNameDisplayOnly);
                 });
            this.CreateMap<IPagedList<TestRunDataModel>, IPagedList<TestRunViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestRunDataModel, TestRunViewModel>>();

            this.CreateMap<CreateTestRunViewModel, TestRunDataModel>();
            this.CreateMap<TestRunDataModel, CreateTestRunViewModel>();
                

            this.CreateMap<EditTestRunViewModel, TestRunDataModel>();
            this.CreateMap<TestRunDataModel, EditTestRunViewModel>();
        }

    }
}
