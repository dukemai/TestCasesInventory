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
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new TestSuiteMappingProfile("TestSuiteMapping"));
                cfg.AddProfile(new TestCaseMappingProfile("TestCaseMapping"));
                cfg.AddProfile(new TeamMappingProfile("TeamMapping"));
                cfg.AddProfile(new RoleMappingProfile("RoleMapping"));
                cfg.AddProfile(new UserMappingProfile("UserMapping"));
                cfg.AddProfile(new TestRunMappingProfile("TestRunMapping"));

            });
        }
    }
}
