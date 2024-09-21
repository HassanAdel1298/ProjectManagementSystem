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

namespace ProjectManagementSystem.Application.CQRS.Projects.Commands
{

    public record CreateProjectCommand(ProjectCreateDTO projectDTO) : IRequest<ResultDTO<ProjectCreateDTO>>;

    

    public class CreateProjectCommandHandler : BaseRequestHandler<Project , CreateProjectCommand, ResultDTO<ProjectCreateDTO>>
    {

        public CreateProjectCommandHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<ProjectCreateDTO>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            

            var project = request.projectDTO.MapOne<Project>();

            await _repository.AddAsync(project);

            await _repository.SaveChangesAsync();

            var projectDTO = project.MapOne<ProjectCreateDTO>();

            return ResultDTO<ProjectCreateDTO>.Sucess(projectDTO, "Project created successfully!");
        }
    }
}
