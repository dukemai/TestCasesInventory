using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    public class TestCasesInTestRunMappingProfile : Profile
    {
        private UserManager<ApplicationUser> UserManager;
        private ITestSuiteRepository testSuiteRepository;
        public TestCasesInTestRunMappingProfile(string profileName) : base(profileName)
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            testSuiteRepository = new TestSuiteRepository();

            this.CreateMap<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => UserManager.FindById(src.AssignedTo).DisplayName))
                .ForMember(dest => dest.AssignedBy, opt => opt.MapFrom(src => UserManager.FindById(src.AssignedBy).DisplayName))
                .ForMember(dest => dest.TestSuiteTitle, opt => opt.MapFrom(src => testSuiteRepository.GetTestSuiteByID(src.TestSuiteID).Title));


            this.CreateMap<IPagedList<TestCasesInTestRunDataModel>, IPagedList<TestCasesInTestRunViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestCasesInTestRunDataModel, TestCasesInTestRunViewModel>>();

            this.CreateMap<CreateTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>();
            this.CreateMap<TestCasesInTestRunDataModel, CreateTestCasesInTestRunViewModel>();

            this.CreateMap<EditTestCasesInTestRunViewModel, TestCasesInTestRunDataModel>()
                .Ignore(m => m.CreatedDate)
                .Ignore(m => m.Created)
                .Ignore(m => m.TestCaseID)
                .Ignore(m => m.TestRunID)
                .Ignore(m => m.TestSuiteID)
                .Ignore(m => m.LastRunDate);

            this.CreateMap<TestCasesInTestRunDataModel, EditTestCasesInTestRunViewModel>();
        }

    }
}
