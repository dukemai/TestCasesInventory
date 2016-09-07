using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.Linq;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Common;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    class TestCaseResultMappingProfile : Profile
    {
        TestCaseResultRepository TestCaseResultRepository;
        UserManager<ApplicationUser> UserManager;
        TestRunRepository TestRunRepository;

        public TestCaseResultMappingProfile(string profileName) : base(profileName)
        {
            TestRunRepository = new TestRunRepository();
            TestCaseResultRepository = new TestCaseResultRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TestCaseResultDataModel, TestCaseResultViewModel>()
                 .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName))
                 .ForMember(dest => dest.TestRunTitle, opt => opt.MapFrom(src => TestRunRepository.GetTestRunByID(src.TestRunResultID).Title));
            this.CreateMap<IPagedList<TestCaseResultDataModel>, IPagedList<TestCaseResultViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestCaseResultDataModel, TestCaseResultViewModel>>();
            this.CreateMap<EditTestCaseResultViewModel, TestCaseResultDataModel>()
                .Ignore(m => m.Created)
                .Ignore(m => m.CreatedDate);
            this.CreateMap<CreateTestCaseResultViewModel, TestCaseResultDataModel>();
        }
    }
}