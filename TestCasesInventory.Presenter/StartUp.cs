using AutoMapper;
using PagedList;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Mappings;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter
{
    public static class StartUp
    {
        public static void Start()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new TestSuiteMappingProfile("TestSuiteMapping")));
            //Mapper.Initialize(cfg => cfg.CreateMap<IPagedList<TestSuiteDataModel>, IPagedList<TestSuiteViewModel>>()
            //                        .ConvertUsing<Mappings.PagedListConverter<TestSuiteDataModel, TestSuiteViewModel>>());
            //Mapper.Initialize(cfg => cfg.CreateMap<IPagedList<TestSuiteViewModel>, IPagedList<TestSuiteDataModel>>());
        }
    }
}
