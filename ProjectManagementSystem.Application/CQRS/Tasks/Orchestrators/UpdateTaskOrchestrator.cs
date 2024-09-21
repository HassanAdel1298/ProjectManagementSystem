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



            var resultUpdateTaskDTO = await _mediator.Send(new UpdateTaskCommand(request.taskDTO));

            return resultUpdateTaskDTO;
        }


    }
}
