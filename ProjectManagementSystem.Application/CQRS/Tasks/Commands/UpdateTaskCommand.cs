using MediatR;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Commands
{
 
    public record UpdateTaskCommand(TaskUpdateDTO taskDTO) : IRequest<ResultDTO<TaskUpdateDTO>>;



    public class UpdateTaskCommandHandler : BaseRequestHandler<AppTask, UpdateTaskCommand, ResultDTO<TaskUpdateDTO>>
    {

        public UpdateTaskCommandHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<TaskUpdateDTO>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var resultTask = await _repository.GetAllAsync()
                                        .Where(t => t.ID == request.taskDTO.ID
                                                && t.UserCreateID == request.taskDTO.UserCreateID
                                        )
                                        .FirstOrDefaultAsync();

            if (resultTask is null)
            {
                return ResultDTO<TaskUpdateDTO>.Faliure("Task ID Not Found or isn't managed this Task!");
            }

            var task = request.taskDTO.MapOne<AppTask>();

            task.CreatedDate = resultTask.CreatedDate;
            task.UserCreateID = resultTask.UserCreateID;
            task.ProjectID = resultTask.ProjectID;
            task.Status = resultTask.Status;

            await _repository.UpdateAsync(task);

            await _repository.SaveChangesAsync();

            var taskDTO = task.MapOne<TaskUpdateDTO>();

            return ResultDTO<TaskUpdateDTO>.Sucess(taskDTO, "Task Updated successfully!");
        }
    }
}
