using MediatR;
using ProjectManagementSystem.Application.DTO.UserProjects;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;

namespace ProjectManagementSystem.Application.CQRS.UserProjects.Orchestrators
{

    public record AssignProjectToUserOrchestrator(AssignProjectDTO assignProjectDTO) : IRequest<ResultDTO<bool>>;


    public class AssignProjectToUserOrchestratorHandler : BaseRequestHandler<UserProject, AssignProjectToUserOrchestrator, ResultDTO<bool>>
    {

        public AssignProjectToUserOrchestratorHandler(RequestParameters<UserProject> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(AssignProjectToUserOrchestrator request, CancellationToken cancellationToken)
        {

            var resultIsVerifiedAssignUser = await _mediator.Send(new IsVerifiedUserCommand(request.assignProjectDTO.UserAssignID));

            if (!resultIsVerifiedAssignUser.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(resultIsVerifiedAssignUser.Message);
            }
            var resultIsDeletedProject = await _mediator.Send(new IsDeletedProjectCommand(request.assignProjectDTO.ProjectID));

            if (!resultIsDeletedProject.IsSuccess)
            {
                return resultIsDeletedProject;
            }
            

            var resultAssignProjectDTO = await _mediator.Send(new AssignProjectToUserCommand(request.assignProjectDTO));

            return resultAssignProjectDTO;
        }


    }
}
