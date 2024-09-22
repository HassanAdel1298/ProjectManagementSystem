using MediatR;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.Tasks.Queries
{

    public record GetProjectIDByTaskQuery(int taskID) : IRequest<ResultDTO<int>>;



    public class GetProjectIDByTaskQueryHandler : BaseRequestHandler<AppTask, GetProjectIDByTaskQuery, ResultDTO<int>>
    {

        public GetProjectIDByTaskQueryHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<int>> Handle(GetProjectIDByTaskQuery request, CancellationToken cancellationToken)
        {
            var ProjectID = await _repository.GetAllAsync()
                                        .Where(t => t.ID == request.taskID)
                                        .Select(t => t.ProjectID)
                                        .FirstOrDefaultAsync();

            if (ProjectID == 0)
            {
                return ResultDTO<int>.Faliure("Task ID Not Found!");
            }

            

            return ResultDTO<int>.Sucess(ProjectID, "Get Project ID by Task ID successfully!");
        }
    }
}
