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
 
    public record DeleteTaskCommand(TaskDeleteDTO taskDTO) : IRequest<ResultDTO<bool>>;

    public class DeleteTaskCommandHandler : BaseRequestHandler<AppTask, DeleteTaskCommand, ResultDTO<bool>>
    {

        public DeleteTaskCommandHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetAllAsync()
                                        .Where(
                                                p => p.ID == request.taskDTO.ID
                                                && p.UserCreateID == request.taskDTO.UserCreateID
                                        )
                                        .FirstOrDefaultAsync();

            if (task is null)
            {
                return ResultDTO<bool>.Faliure("Task ID Not Found!");
            }

            _repository.DeleteAsync(task);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Delete Task successfully!");

        }
    }
}
