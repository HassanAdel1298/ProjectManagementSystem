using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO.Dashboard;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Dashboard.Queries
{

    public record TotalTasksQuery() : IRequest<ResultDTO<TotalTasksDTO>>;


    public class TotalTasksQueryHandler : BaseRequestHandler<AppTask, TotalTasksQuery, ResultDTO<TotalTasksDTO>>
    {

        public TotalTasksQueryHandler(RequestParameters<AppTask> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TotalTasksDTO>> Handle(TotalTasksQuery request, CancellationToken cancellationToken)
        {
            var total = await _repository.GetAllAsync().CountAsync();

            var totalInProgress = await _repository.GetAllAsync()
                                                .CountAsync(t => t.Status == Status.InProgress);

            TotalTasksDTO totalTasksDTO = new TotalTasksDTO()
            {
                Total = total,
                InProgress = totalInProgress
            };

            return ResultDTO<TotalTasksDTO>.Sucess(totalTasksDTO, "Total Tasks successfully!");
        }
    }
}
