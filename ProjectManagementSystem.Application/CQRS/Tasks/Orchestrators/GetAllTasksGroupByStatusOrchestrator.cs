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

    public record GetAllTasksGroupByStatusOrchestrator(int projectID) : IRequest<ResultDTO<TaskReturnViewGroupByStatusDTO>>;


    public class GetAllTasksGroupByStatusOrchestratorHandler : BaseRequestHandler<AppTask, GetAllTasksGroupByStatusOrchestrator, ResultDTO<TaskReturnViewGroupByStatusDTO>>
    {

        public GetAllTasksGroupByStatusOrchestratorHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskReturnViewGroupByStatusDTO>> Handle(GetAllTasksGroupByStatusOrchestrator request, CancellationToken cancellationToken)
        {

          

            var resultIsDeletedProject = await _mediator.Send(new IsDeletedProjectCommand(request.projectID));

            if (!resultIsDeletedProject.IsSuccess)
            {
                return ResultDTO<TaskReturnViewGroupByStatusDTO>.Faliure(resultIsDeletedProject.Message);
            }

            



            var resultGetAllTasksGroupByStatusDTO = await _mediator.Send(new GetAllTasksGroupByStatusQuery(request.projectID));

            return resultGetAllTasksGroupByStatusDTO;
        }


    }
}
