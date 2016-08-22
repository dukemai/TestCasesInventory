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
        TeamRepository teamRepository;

        public TestSuiteMappingProfile(string profileName)
            : base(profileName)
        {
            testCaseRepository = new TestCaseRepository();
            teamRepository = new TeamRepository();

            this.CreateMap<TestSuiteDataModel, TestSuiteViewModel>()
                 .ForMember(dest => dest.TestCasesNumber, opt => opt.MapFrom(src => testCaseRepository.TotalTestCasesForTestSuite(src.ID)))
                 .ForMember(dest => dest.CreateDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.CreateDisplayOnly) ? teamRepository.FindUserByEmail(src.Created).DisplayName: src.CreateDisplayOnly);
                 })
                 .ForMember(dest => dest.LastModifiedDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.LastModifiedDisplayOnly) ? teamRepository.FindUserByEmail(src.LastModifiedDisplayOnly).DisplayName : src.LastModifiedDisplayOnly);
                 })
                 .ForMember(dest => dest.TeamNameDisplayOnly, opt =>
                 {
                     opt.MapFrom(src => string.IsNullOrEmpty(src.TeamNameDisplayOnly) ? teamRepository.GetTeamByID(src.TeamID).Name : src.TeamNameDisplayOnly);
                 });
            this.CreateMap<IPagedList<TestSuiteDataModel>, IPagedList<TestSuiteViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TestSuiteDataModel, TestSuiteViewModel>>();

            this.CreateMap<CreateTestSuiteViewModel, TestSuiteDataModel>()
                .ForMember(dest => dest.TeamID, opt => opt.MapFrom(src => teamRepository.FindUserByEmail(src.Created).TeamID));

            this.CreateMap<EditTestSuiteViewModel, TestSuiteDataModel>();

        }
    }
}
