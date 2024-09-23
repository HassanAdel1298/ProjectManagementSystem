



using AutoMapper;
using ProjectManagementSystem.Application.DTO.Projects;
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
            CreateMap<ViewProjectViewModel, ProjectViewDTO>().ReverseMap();

            CreateMap<ProjectUpdateViewModel, ProjectUpdateDTO>().ReverseMap();
            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();

            CreateMap<ProjectUpdateVisibilityViewModel, ProjectUpdateVisibilityDTO>().ReverseMap();
            CreateMap<ProjectUpdateVisibilityDTO, Project>().ReverseMap();

        }
    }
}
