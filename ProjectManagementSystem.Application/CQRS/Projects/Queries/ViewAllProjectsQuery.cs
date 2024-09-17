using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
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

    public record ViewAllProjectsQuery(int userID) : IRequest<ResultDTO<IEnumerable<ProjectViewDTO>>>;

    public class ProjectViewDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int NumUsers { get; set; }
        public int NumTasks { get; set; }
        public DateTime CreatedDate { get; set; }

    }

    public class ViewAllProjectsQueryHandler : IRequestHandler<ViewAllProjectsQuery, ResultDTO<IEnumerable<ProjectViewDTO>>>
    {
        IRepository<Project> _repository;
        IMediator _mediator;

        public ViewAllProjectsQueryHandler(IRepository<Project> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<IEnumerable<ProjectViewDTO>>> Handle(ViewAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projectsDTO = await _repository.GetAllAsync()
                                        .Where(p => p.UserProjects.Contains
                                            (p.UserProjects.Where
                                                (up => up.UserID == request.userID).FirstOrDefault()
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
