using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.UserProjects;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.UserProjects.Orchestrators
{
    
    public record UnassignProjectToUserOrchestrator(UnassignProjectDTO unassignProjectDTO) : IRequest<ResultDTO<bool>>;


    public class UnassignProjectToUserOrchestratorHandler : BaseRequestHandler<UserProject, UnassignProjectToUserOrchestrator, ResultDTO<bool>>
    {

        public UnassignProjectToUserOrchestratorHandler(RequestParameters<UserProject> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(UnassignProjectToUserOrchestrator request, CancellationToken cancellationToken)
        {

            var resultIsVerifiedUnassignUser = await _mediator.Send(new IsVerifiedUserCommand(request.unassignProjectDTO.UserUnassignID));

            if (!resultIsVerifiedUnassignUser.IsSuccess)
            {
                return resultIsVerifiedUnassignUser;
            }

            var resultIsDeletedProject = await _mediator.Send(new IsDeletedProjectCommand(request.unassignProjectDTO.ProjectID));

            if (!resultIsDeletedProject.IsSuccess)
            {
                return resultIsDeletedProject;
            }

            var resultUnassignProjectDTO = await _mediator.Send(new UnassignProjectToUserCommand(request.unassignProjectDTO));

            return resultUnassignProjectDTO;
        }


    }
}
