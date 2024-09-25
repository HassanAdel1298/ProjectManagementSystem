using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.TaskTimers;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Commands
{

    public record IsDeletedTaskCommand(TaskTimerCreateDTO taskTimerDTO) : IRequest<ResultDTO<bool>>;

    public class IsDeletedTaskCommandHandler : BaseRequestHandler<AppTask, IsDeletedTaskCommand, ResultDTO<bool>>
    {

        public IsDeletedTaskCommandHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(IsDeletedTaskCommand request, CancellationToken cancellationToken)
        {
            var isVerifiedTask = await _repository.GetAllAsync()
                                .AnyAsync(
                                        t => t.ID == request.taskTimerDTO.TaskID
                                        && (t.UserCreateID == request.taskTimerDTO.UserCreateID
                                        || t.UserAssignID == request.taskTimerDTO.UserCreateID)
                                        );


            if (!isVerifiedTask)
            {
                return ResultDTO<bool>.Faliure("Task ID Not Found or isn't managed this Task");
            }

            return ResultDTO<bool>.Sucess(true, "Project ID Found");
        }

    }
}
