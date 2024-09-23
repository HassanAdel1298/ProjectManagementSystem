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

namespace ProjectManagementSystem.Application.CQRS.Projects.Commands
{

    public record UpdateProjectVisibilityCommand(ProjectUpdateVisibilityDTO projectDTO) : IRequest<ResultDTO<ProjectUpdateVisibilityDTO>>;



    public class UpdateProjectVisibilityCommandHandler : BaseRequestHandler<Project, UpdateProjectVisibilityCommand, ResultDTO<ProjectUpdateVisibilityDTO>>
    {

        public UpdateProjectVisibilityCommandHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<ProjectUpdateVisibilityDTO>> Handle(UpdateProjectVisibilityCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetAllAsync()
                                        .Where(p => p.ID == request.projectDTO.ID &&
                                        p.UserProjects.Contains
                                            (p.UserProjects.Where
                                                (
                                                up => up.UserID == request.projectDTO.UserCreateID
                                                && up.Role == UserRole.Owner
                                                && up.IsDeleted != true
                                                ).FirstOrDefault()
                                            )
                                        )
                                        .FirstOrDefaultAsync();

            if (project is null)
            {
                return ResultDTO<ProjectUpdateVisibilityDTO>.Faliure("Project ID Not Found or isn't managed this Project!");
            }

            
            project.IsPublic = request.projectDTO.IsPublic;

            await _repository.UpdateAsync(project);

            await _repository.SaveChangesAsync();

            var projectDTO = project.MapOne<ProjectUpdateVisibilityDTO>();

            return ResultDTO<ProjectUpdateVisibilityDTO>.Sucess(projectDTO, "Project Updated Visibility successfully!");
        }
    }
}
