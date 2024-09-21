using MediatR;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.Helpers;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Commands
{

    public record CreateTaskCommand(TaskCreateDTO taskDTO) : IRequest<ResultDTO<TaskCreateDTO>>;



    public class CreateTaskCommandHandler : BaseRequestHandler<AppTask ,CreateTaskCommand, ResultDTO<TaskCreateDTO>>
    {

        public CreateTaskCommandHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskCreateDTO>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {


            var task = request.taskDTO.MapOne<AppTask>();

            await _repository.AddAsync(task);

            await _repository.SaveChangesAsync();

            var taskDTO = task.MapOne<TaskCreateDTO>();

            return ResultDTO<TaskCreateDTO>.Sucess(taskDTO, "Task created successfully!");
        }
    }
}
