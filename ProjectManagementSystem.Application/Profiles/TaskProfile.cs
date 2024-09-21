using AutoMapper;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.ViewModel.Projects;
using ProjectManagementSystem.Application.ViewModel.Tasks;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.Profiles
{
    public class TaskProfile : Profile
    {

        public TaskProfile() 
        {

            CreateMap<TaskCreateViewModel, TaskCreateDTO>().ReverseMap();
            CreateMap<TaskCreateDTO, AppTask>().ReverseMap();


            CreateMap<TaskSearchViewModel, TaskSearchDTO>().ReverseMap();
            CreateMap<ViewTaskViewModel, TaskViewDTO>().ReverseMap();

            CreateMap<TaskUpdateViewModel, TaskUpdateDTO>().ReverseMap();
            CreateMap<TaskUpdateDTO, AppTask>().ReverseMap();

            CreateMap<TaskUpdateStatusViewModel, TaskUpdateStatusDTO>().ReverseMap();
            CreateMap<TaskUpdateStatusDTO, AppTask>().ReverseMap();
        }

    }
}
