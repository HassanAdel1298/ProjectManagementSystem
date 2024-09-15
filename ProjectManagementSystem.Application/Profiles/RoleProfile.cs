using AutoMapper;
using ProjectManagementSystem.Application.CQRS.Roles.Commands;
using ProjectManagementSystem.Application.ViewModel.Roles;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<RoleViewModel, RoleDTO>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
        }
    }
}
