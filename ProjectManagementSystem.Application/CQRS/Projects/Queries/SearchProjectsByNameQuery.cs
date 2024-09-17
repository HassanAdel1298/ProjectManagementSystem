using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Projects.Queries
{

    public record SearchProjectsByNameQuery(ProjectSearchDTO projectSearchDTO) : IRequest<ResultDTO<IEnumerable<ProjectViewDTO>>>;

    public class ProjectSearchDTO
    {
        public int userID { get; set; }
        public string Name { get; set; }

    }

    public class SearchProjectsByNameQueryHandler : IRequestHandler<SearchProjectsByNameQuery, ResultDTO<IEnumerable<ProjectViewDTO>>>
    {
        IRepository<Project> _repository;
        IMediator _mediator;

        public SearchProjectsByNameQueryHandler(IRepository<Project> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<IEnumerable<ProjectViewDTO>>> Handle(SearchProjectsByNameQuery request, CancellationToken cancellationToken)
        {
            var projectsDTO = await _repository.GetAllAsync()
                                        .Where(p => p.Title == request.projectSearchDTO.Name && 
                                        p.UserProjects.Contains
                                            (p.UserProjects.Where
                                                (up => up.UserID == request.projectSearchDTO.userID).FirstOrDefault()
                                            )
                                        )
                                        .Select(p => new ProjectViewDTO()
                                        {
                                            Title = p.Title,
                                            Description = p.Description,
                                            IsPublic = p.IsPublic,
                                            CreatedDate = p.CreatedDate,
                                            NumUsers = p.UserProjects.Count(),
                                            NumTasks = p.Tasks.Count(),
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<ProjectViewDTO>>.Sucess(projectsDTO, "View Projects successfully!");
        }
    }
}
