using MediatR;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO.TaskTimers;

namespace ProjectManagementSystem.Application.CQRS.TaskTimers.Commands
{

    public record CreateTaskTimerCommand(TaskTimerCreateDTO taskTimerDTO) : IRequest<ResultDTO<TaskTimerCreateDTO>>;



    public class CreateTaskTimerCommandHandler : BaseRequestHandler<TaskTimer, CreateTaskTimerCommand, ResultDTO<TaskTimerCreateDTO>>
    {

        public CreateTaskTimerCommandHandler(RequestParameters<TaskTimer> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskTimerCreateDTO>> Handle(CreateTaskTimerCommand request, CancellationToken cancellationToken)
        {


            var taskTimer = request.taskTimerDTO.MapOne<TaskTimer>();

            await _repository.AddAsync(taskTimer);

            await _repository.SaveChangesAsync();

            var taskTimerDTO = taskTimer.MapOne<TaskTimerCreateDTO>();

            return ResultDTO<TaskTimerCreateDTO>.Sucess(taskTimerDTO, "Task Timer created successfully!");
        }
    }
}
