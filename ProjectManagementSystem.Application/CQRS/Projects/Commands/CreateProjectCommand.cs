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

namespace ProjectManagementSystem.Application.CQRS.Projects.Commands
{

    public record CreateProjectCommand(ProjectCreateDTO projectDTO) : IRequest<ResultDTO<ProjectCreateDTO>>;

    public class ProjectCreateDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int UserCreateID { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ResultDTO<ProjectCreateDTO>>
    {
        IRepository<Project> _repository;
        IMediator _mediator;

        public CreateProjectCommandHandler(IRepository<Project> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<ProjectCreateDTO>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            

            var project = request.projectDTO.MapOne<Project>();

            await _repository.AddAsync(project);

            await _repository.SaveChangesAsync();

            var projectDTO = project.MapOne<ProjectCreateDTO>();

            return ResultDTO<ProjectCreateDTO>.Sucess(projectDTO, "Project created successfully!");
        }
    }
}
