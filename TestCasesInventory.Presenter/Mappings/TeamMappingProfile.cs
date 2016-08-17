using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System.Linq;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    class TeamMappingProfile : Profile
    {
        TeamRepository teamRepository;
        UserManager<ApplicationUser> UserManager;

        public TeamMappingProfile(string profileName) : base(profileName)
        {
            teamRepository = new TeamRepository();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<TeamDataModel, TeamViewModel>()
                 .ForMember(dest => dest.MembersNumber, opt => opt.MapFrom(src => teamRepository.ListUsersBelongTeam(src.ID).Count()))
                 .ForMember(dest => dest.Created, opt => opt.MapFrom(src => UserManager.FindByEmail(src.Created).DisplayName))
                 .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => UserManager.FindByEmail(src.LastModified).DisplayName));
            this.CreateMap<IPagedList<TeamDataModel>, IPagedList<TeamViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TeamDataModel, TeamViewModel>>();
            this.CreateMap<EditTeamViewModel, TeamDataModel>();
            this.CreateMap<CreateTeamViewModel, TeamDataModel>();
            this.CreateMap<ApplicationUser, UsersBelongTeamViewModel>();
            this.CreateMap<ApplicationUser, UsersNotBelongTeamViewModel>();
        }

    }
}
