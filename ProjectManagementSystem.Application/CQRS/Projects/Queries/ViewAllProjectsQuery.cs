using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Projects.Queries
{

    public record ViewAllProjectsQuery(ProjectViewDTO projectViewDTO) : IRequest<ResultDTO<IEnumerable<ProjectReturnViewDTO>>>;


    public class ViewAllProjectsQueryHandler : BaseRequestHandler<Project , ViewAllProjectsQuery, ResultDTO<IEnumerable<ProjectReturnViewDTO>>>
    {

        public ViewAllProjectsQueryHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<ProjectReturnViewDTO>>> Handle(ViewAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projectsDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.projectViewDTO.pageNumber,
                                            request.projectViewDTO.pageSize
                                        )
                                        .Where(p => p.UserProjects.Contains
                                            (p.UserProjects.Where
                                                (
                                                up => up.UserID == request.projectViewDTO.userID
                                                && up.IsDeleted != true
                                                ).FirstOrDefault()
                                            )
                                        )
                                        .Select(p => new ProjectReturnViewDTO()
                                        {
                                            Title = p.Title,
                                            Description = p.Description,
                                            IsPublic = p.IsPublic,
                                            CreatedDate = p.CreatedDate,
                                            NumUsers = p.UserProjects
                                                            .Count(up => up.IsDeleted != true),
                                            NumTasks = p.Tasks
                                                            .Count(t => t.IsDeleted != true)
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<ProjectReturnViewDTO>>.Sucess(projectsDTO, "View Projects successfully!");
        }
    }
}
