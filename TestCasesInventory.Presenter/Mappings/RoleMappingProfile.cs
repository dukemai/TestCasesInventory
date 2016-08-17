using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCasesInventory.Data;
using TestCasesInventory.Data.DataModels;
using TestCasesInventory.Presenter.Models;

namespace TestCasesInventory.Presenter.Mappings
{
    class RoleMappingProfile : Profile
    {
        protected RoleManager<IdentityRole> RoleManager;
        protected UserManager<ApplicationUser> UserManager;

        public RoleMappingProfile(string profileName) : base(profileName)
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.CreateMap<IdentityRole, RoleViewModel>()
                 .ForMember(dest => dest.numberOfAccount, opt => opt.MapFrom(src => RoleManager.FindById(src.Id).Users.ToList().Count));
            this.CreateMap<IPagedList<IdentityRole>, IPagedList<RoleViewModel>>()
                .ConvertUsing<Mappings.PagedListConverter<IdentityRole, RoleViewModel>>();
            this.CreateMap<ApplicationUser, UsersNotBelongRoleViewModel>();
            this.CreateMap<ApplicationUser, UsersBelongRoleViewModel>();
        }
    }
}
