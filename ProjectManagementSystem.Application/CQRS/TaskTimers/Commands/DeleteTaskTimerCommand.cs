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

    public record DeleteTaskTimerCommand(TaskTimerDeleteDTO taskTimerDTO) : IRequest<ResultDTO<bool>>;

    public class DeleteTaskTimerCommandHandler : BaseRequestHandler<TaskTimer, DeleteTaskTimerCommand, ResultDTO<bool>>
    {

        public DeleteTaskTimerCommandHandler(RequestParameters<TaskTimer> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(DeleteTaskTimerCommand request, CancellationToken cancellationToken)
        {
            var taskTimer = await _repository.GetAllAsync()
                                        .Where(
                                                tt => tt.ID == request.taskTimerDTO.ID
                                                && (
                                                tt.Task.UserCreateID == request.taskTimerDTO.UserCreateID
                                                || tt.Task.UserAssignID == request.taskTimerDTO.UserCreateID
                                                )
                                        )
                                        .FirstOrDefaultAsync();

            if (taskTimer is null)
            {
                return ResultDTO<bool>.Faliure("Task Timer ID Not Found or isn't managed this Task Timer!");
            }

            _repository.DeleteAsync(taskTimer);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Delete Task Timer successfully!");

        }
    }
}
