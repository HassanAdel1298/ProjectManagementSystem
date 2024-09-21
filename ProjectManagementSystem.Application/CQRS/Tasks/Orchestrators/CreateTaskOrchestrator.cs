using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO.UserProjects;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.CQRS.Tasks.Commands;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Orchestrators
{


    public record CreateTaskOrchestrator(TaskCreateDTO taskDTO) : IRequest<ResultDTO<TaskCreateDTO>>;


    public class CreateTaskOrchestratorHandler : BaseRequestHandler<AppTask, CreateTaskOrchestrator, ResultDTO<TaskCreateDTO>>
    {

        public CreateTaskOrchestratorHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskCreateDTO>> Handle(CreateTaskOrchestrator request, CancellationToken cancellationToken)
        {

            var resultIsVerifiedAssignUser = await _mediator.Send(new IsVerifiedUserCommand(request.taskDTO.UserAssignID));

            if (!resultIsVerifiedAssignUser.IsSuccess)
            {
                return ResultDTO<TaskCreateDTO>.Faliure(resultIsVerifiedAssignUser.Message);
            }

            var resultIsDeletedProject = await _mediator.Send(new IsDeletedProjectCommand(request.taskDTO.ProjectID));

            if (!resultIsDeletedProject.IsSuccess)
            {
                return ResultDTO<TaskCreateDTO>.Faliure(resultIsDeletedProject.Message);
            }

            var resultIsManagedProject = await _mediator.Send(new IsManagedProjectToOwnerCommand(
                                            request.taskDTO.ProjectID,request.taskDTO.UserCreateID));

            if (!resultIsManagedProject.IsSuccess)
            {
                return ResultDTO<TaskCreateDTO>.Faliure(resultIsManagedProject.Message);
            }
            


            var resultCreateTaskDTO = await _mediator.Send(new CreateTaskCommand(request.taskDTO));

            return resultCreateTaskDTO;
        }


    }
}
