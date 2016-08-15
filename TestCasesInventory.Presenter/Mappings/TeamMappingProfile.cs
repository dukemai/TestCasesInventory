using AutoMapper;
using PagedList;
using System.Linq;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Data.Repositories;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    class TeamMappingProfile : Profile
    {
        TeamRepository teamRepository;

        public TeamMappingProfile(string profileName) : base(profileName)
        {
            teamRepository = new TeamRepository();

            this.CreateMap<TeamDataModel, TeamViewModel>()
                 .ForMember(dest => dest.MembersNumber, opt => opt.MapFrom(src => teamRepository.ListUsersBelongTeam(src.ID).Count()));
            this.CreateMap<IPagedList<TeamDataModel>, IPagedList<TeamViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<TeamDataModel, TeamViewModel>>();
        }
    }
}
