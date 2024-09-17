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

    public record UpdateProjectCommand(ProjectUpdateDTO projectDTO) : IRequest<ResultDTO<ProjectUpdateDTO>>;

    public class ProjectUpdateDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultDTO<ProjectUpdateDTO>>
    {
        IRepository<Project> _repository;
        IMediator _mediator;

        public UpdateProjectCommandHandler(IRepository<Project> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<ProjectUpdateDTO>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync()
                                        .Where(p => p.ID == request.projectDTO.ID)
                                        .FirstOrDefaultAsync();

            if (result is null)
            {
                return ResultDTO<ProjectUpdateDTO>.Faliure("Project ID Not Found!");
            }

            var project = request.projectDTO.MapOne<Project>();

            project.CreatedDate = result.CreatedDate;

            await _repository.UpdateAsync(project);

            await _repository.SaveChangesAsync();

            var projectDTO = project.MapOne<ProjectUpdateDTO>();

            return ResultDTO<ProjectUpdateDTO>.Sucess(projectDTO, "Project Updated successfully!");
        }
    }
}
