using AutoMapper;
using ProjectManagementSystem.Application.CQRS.UserRoles.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.ViewModel.Users;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterViewModel, RegisterUserDTO>().ReverseMap();
            CreateMap<RegisterUserDTO, User>().ReverseMap();


            CreateMap<LoginViewModel, LoginUserDTO>().ReverseMap();
            CreateMap<LoginUserDTO, User>().ReverseMap();


            CreateMap<VerifyAccountViewModel, VerifyUserDTO>().ReverseMap();


            CreateMap<OTPAddedDTO, User>().ReverseMap();

            
            CreateMap<ResetPasswordViewModel, ResetPasswordDto>().ReverseMap();


            CreateMap<UserRoleDTO, UserRole>().ReverseMap();
        }
    }
}
