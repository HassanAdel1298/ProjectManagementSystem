using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Commands
{
 

    public record UpdateStatusTaskCommand(TaskUpdateStatusDTO taskDTO) : IRequest<ResultDTO<TaskUpdateStatusDTO>>;



    public class UpdateStatusTaskCommandHandler : BaseRequestHandler<AppTask, UpdateStatusTaskCommand, ResultDTO<TaskUpdateStatusDTO>>
    {

        public UpdateStatusTaskCommandHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<TaskUpdateStatusDTO>> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetAllAsync()
                                        .Where(t => t.ID == request.taskDTO.ID
                                                && t.UserCreateID == request.taskDTO.UserCreateID
                                        )
                                        .FirstOrDefaultAsync();

            if (task is null)
            {
                return ResultDTO<TaskUpdateStatusDTO>.Faliure("Task ID Not Found or isn't managed this Task!");
            }


            task.Status = request.taskDTO.Status;

            await _repository.UpdateAsync(task);

            await _repository.SaveChangesAsync();

            var taskDTO = task.MapOne<TaskUpdateStatusDTO>();

            return ResultDTO<TaskUpdateStatusDTO>.Sucess(taskDTO, "Task Updated successfully!");
        }
    }
}
