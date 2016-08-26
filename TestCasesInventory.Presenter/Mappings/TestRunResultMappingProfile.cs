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

    public class TestRunResultMappingProfile : Profile
    {
        UserManager<ApplicationUser> UserManager;

        public TestRunResultMappingProfile(string profileName) : base(profileName)
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TestRunResultDataModel, TestRunResultViewModel>()
                 .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName))
                 .ForMember(dest => dest.CreateDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.CreateDisplayOnly) ? UserManager.FindByEmail(src.Created).DisplayName : src.CreateDisplayOnly);
                 })
                 .ForMember(dest => dest.LastModifiedDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.LastModifiedDisplayOnly) ? UserManager.FindByEmail(src.LastModified).DisplayName : src.LastModifiedDisplayOnly);
                 })
                 .ForMember(dest => dest.Status, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.Status) ? "" : src.Status);
                 });

            this.CreateMap<IPagedList<TestRunResultDataModel>, IPagedList<TestRunResultViewModel>>()
               .ConvertUsing<Mappings.PagedListConverter<TestRunResultDataModel, TestRunResultViewModel>>();

            this.CreateMap<CreateTestRunResultViewModel, TestRunResultDataModel>();
            this.CreateMap<TestRunResultDataModel, CreateTestRunResultViewModel>();

            this.CreateMap<EditTestRunResultViewModel, TestRunResultDataModel>();
            this.CreateMap<TestRunResultDataModel, EditTestRunResultViewModel>();

        }
    }
}
