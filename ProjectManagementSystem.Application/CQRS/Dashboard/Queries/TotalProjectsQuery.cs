using MediatR;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Application.CQRS.Dashboard.Queries
{

    public record TotalProjectsQuery() : IRequest<ResultDTO<int>>;


    public class TotalProjectsQueryHandler : BaseRequestHandler<Project, TotalProjectsQuery, ResultDTO<int>>
    {

        public TotalProjectsQueryHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<int>> Handle(TotalProjectsQuery request, CancellationToken cancellationToken)
        {
            var TotalprojectsDTO = await _repository.GetAllAsync().CountAsync();


            return ResultDTO<int>.Sucess(TotalprojectsDTO, "Total Projects successfully!");
        }
    }
}
