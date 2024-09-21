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

    public record SearchProjectsByNameQuery(ProjectSearchDTO projectSearchDTO) : IRequest<ResultDTO<IEnumerable<ProjectReturnViewDTO>>>;

    

    public class SearchProjectsByNameQueryHandler : BaseRequestHandler<Project , SearchProjectsByNameQuery, ResultDTO<IEnumerable<ProjectReturnViewDTO>>>
    {

        public SearchProjectsByNameQueryHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<ProjectReturnViewDTO>>> Handle(SearchProjectsByNameQuery request, CancellationToken cancellationToken)
        {
            var projectsDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.projectSearchDTO.pageNumber,
                                            request.projectSearchDTO.pageSize
                                        )
                                        .Where(p => p.Title == request.projectSearchDTO.Name && 
                                        p.UserProjects.Contains
                                            (p.UserProjects.Where
                                                (
                                                up => up.UserID == request.projectSearchDTO.userID
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


            return ResultDTO<IEnumerable<ProjectReturnViewDTO>>.Sucess(projectsDTO, "View Projects by Name successfully!");
        }
    }
}
