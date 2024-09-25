using AutoMapper;
using ProjectManagementSystem.Application.DTO.TaskTimers;
using ProjectManagementSystem.Application.ViewModel.TaskTimers;
using ProjectManagementSystem.Entity.Entities;


namespace ProjectManagementSystem.Application.Profiles
{
    public class TaskTimerProfile : Profile
    {
        public TaskTimerProfile() 
        {
            CreateMap<TaskTimerCreateViewModel, TaskTimerCreateDTO>().ReverseMap();
            CreateMap<TaskTimerCreateDTO, TaskTimer>().ReverseMap();

            CreateMap<ViewTaskTimerViewModel, TaskTimerViewDTO>().ReverseMap();

            CreateMap<TaskTimerUpdateViewModel, TaskTimerUpdateDTO>().ReverseMap();
            CreateMap<TaskTimerUpdateDTO, TaskTimer>().ReverseMap();
        }
    }
}
