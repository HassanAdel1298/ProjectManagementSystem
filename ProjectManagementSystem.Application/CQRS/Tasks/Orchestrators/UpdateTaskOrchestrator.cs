using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Tasks.Commands;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.CQRS.Tasks.Queries;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Orchestrators
{

    public record UpdateTaskOrchestrator(TaskUpdateDTO taskDTO) : IRequest<ResultDTO<TaskUpdateDTO>>;


    public class UpdateTaskOrchestratorHandler : BaseRequestHandler<AppTask, UpdateTaskOrchestrator, ResultDTO<TaskUpdateDTO>>
    {

        public UpdateTaskOrchestratorHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskUpdateDTO>> Handle(UpdateTaskOrchestrator request, CancellationToken cancellationToken)
        {

            var resultIsVerifiedAssignUser = await _mediator.Send(new IsVerifiedUserCommand(request.taskDTO.UserAssignID));

            if (!resultIsVerifiedAssignUser.IsSuccess)
            {
                return ResultDTO<TaskUpdateDTO>.Faliure(resultIsVerifiedAssignUser.Message);
            }

            var resultGetProjectIDByTask = await _mediator.Send(new GetProjectIDByTaskQuery(request.taskDTO.ID));

            if (!resultGetProjectIDByTask.IsSuccess)
            {
                return ResultDTO<TaskUpdateDTO>.Faliure(resultGetProjectIDByTask.Message);
            }

            var resultIsUsedProject = await _mediator.Send(new IsUsedProjectToUserCommand(
                                            resultGetProjectIDByTask.Data, request.taskDTO.UserAssignID));

            if (!resultIsUsedProject.IsSuccess)
            {
                return ResultDTO<TaskUpdateDTO>.Faliure(resultIsUsedProject.Message);
            }

            var resultUpdateTaskDTO = await _mediator.Send(new UpdateTaskCommand(request.taskDTO));

            return resultUpdateTaskDTO;
        }


    }
}
