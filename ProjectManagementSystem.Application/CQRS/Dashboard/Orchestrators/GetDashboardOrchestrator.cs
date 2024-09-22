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
using ProjectManagementSystem.Application.DTO.Dashboard;
using ProjectManagementSystem.Application.CQRS.Dashboard.Queries;

namespace ProjectManagementSystem.Application.CQRS.Dashboard.Orchestrators
{
 
    public record GetDashboardOrchestrator() : IRequest<ResultDTO<DashboardDTO>>;


    public class GetDashboardOrchestratorHandler : BaseRequestHandler<User, GetDashboardOrchestrator, ResultDTO<DashboardDTO>>
    {

        public GetDashboardOrchestratorHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<DashboardDTO>> Handle(GetDashboardOrchestrator request, CancellationToken cancellationToken)
        {

            var resultTotalUsers = await _mediator.Send(new TotalUsersQuery());

            var resultTotalProjects = await _mediator.Send(new TotalProjectsQuery());

            var resultTotalTasks = await _mediator.Send(new TotalTasksQuery());

            DashboardDTO dashboardDTO = new DashboardDTO()
            {
                TotalUsersActive = resultTotalUsers.Data.Active,
                TotalUsersInactive = resultTotalUsers.Data.Inactive,
                TotalProjects = resultTotalProjects.Data,
                TotalTasks = resultTotalTasks.Data.Total,
                TotalTasksInProgress = resultTotalTasks.Data.InProgress
            };


            return ResultDTO<DashboardDTO>.Sucess(dashboardDTO, "Get Dashboard successfully!");
        }


    }
}
