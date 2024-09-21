using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.CQRS.Projects.Queries;
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

    public record DeleteProjectCommand(ProjectDeleteDTO projectDTO) : IRequest<ResultDTO<bool>>;

    public class DeleteProjectCommandHandler : BaseRequestHandler<Project , DeleteProjectCommand, ResultDTO<bool>>
    {

        public DeleteProjectCommandHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
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
                return ResultDTO<bool>.Faliure("Project ID Not Found!");
            }

            _repository.DeleteAsync(project);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Delete Project successfully!");
        }
    }
}
