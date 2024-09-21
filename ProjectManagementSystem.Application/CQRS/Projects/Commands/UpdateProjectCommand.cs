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

    public record UpdateProjectCommand(ProjectUpdateDTO projectDTO) : IRequest<ResultDTO<ProjectUpdateDTO>>;

    

    public class UpdateProjectCommandHandler : BaseRequestHandler<Project ,UpdateProjectCommand, ResultDTO<ProjectUpdateDTO>>
    {

        public UpdateProjectCommandHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<ProjectUpdateDTO>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync()
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
