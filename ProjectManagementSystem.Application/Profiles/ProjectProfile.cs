



using AutoMapper;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Projects.Queries;
using ProjectManagementSystem.Application.ViewModel.Projects;
using ProjectManagementSystem.Entity.Entities;

namespace ProjectManagementSystem.Application.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() 
        {
            CreateMap<ProjectCreateViewModel, ProjectCreateDTO>().ReverseMap();
            CreateMap<ProjectCreateDTO, Project>().ReverseMap();

            CreateMap<ProjectSearchViewModel, ProjectSearchDTO>().ReverseMap();

            CreateMap<ProjectUpdateViewModel, ProjectUpdateDTO>().ReverseMap();
            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();

        }
    }
}
