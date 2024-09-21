using AutoMapper;
using ProjectManagementSystem.Application.CQRS.Projects.Queries;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.DTO.UserProjects;
using ProjectManagementSystem.Application.ViewModel.Projects;
using ProjectManagementSystem.Application.ViewModel.UserProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Profiles
{
    public class UserProjectProfile : Profile
    {
        public UserProjectProfile() 
        {
            CreateMap<AssignProjectViewModel, AssignProjectDTO>().ReverseMap();

            CreateMap<UnassignProjectViewModel, UnassignProjectDTO>().ReverseMap();
        }
    }
}
