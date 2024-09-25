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
using ProjectManagementSystem.Entity.Migrations;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.TaskTimers.Commands
{

    public record UpdateTaskTimerCommand(TaskTimerUpdateDTO taskTimerDTO) : IRequest<ResultDTO<TaskTimerUpdateDTO>>;



    public class UpdateTaskTimerCommandHandler : BaseRequestHandler<TaskTimer, UpdateTaskTimerCommand, ResultDTO<TaskTimerUpdateDTO>>
    {

        public UpdateTaskTimerCommandHandler(RequestParameters<TaskTimer> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<TaskTimerUpdateDTO>> Handle(UpdateTaskTimerCommand request, CancellationToken cancellationToken)
        {
            var resultTaskTimer = await _repository.GetAllAsync()
                                        .Where(tt => tt.ID == request.taskTimerDTO.ID
                                                && (
                                                tt.Task.UserCreateID == request.taskTimerDTO.UserCreateID
                                                || tt.Task.UserAssignID == request.taskTimerDTO.UserCreateID
                                                )
                                        )
                                        .FirstOrDefaultAsync();

            if (resultTaskTimer is null)
            {
                return ResultDTO<TaskTimerUpdateDTO>.Faliure("Task Timer ID Not Found or isn't managed this Task Timer!");
            }

            var taskTimer = request.taskTimerDTO.MapOne<TaskTimer>();

            taskTimer.TaskID = resultTaskTimer.TaskID;

            await _repository.UpdateAsync(taskTimer);

            await _repository.SaveChangesAsync();

            var taskTimerDTO = taskTimer.MapOne<TaskTimerUpdateDTO>();

            return ResultDTO<TaskTimerUpdateDTO>.Sucess(taskTimerDTO, "Task Timer Updated successfully!");
        }
    }
}
