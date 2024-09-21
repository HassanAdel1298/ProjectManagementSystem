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

namespace ProjectManagementSystem.Application.CQRS.UserProjects.Commands
{

    public record ManageProjectToOwnerCommand(ProjectCreateDTO projectDTO) : IRequest<ResultDTO<bool>>;



    public class ManageProjectToOwnerCommandHandler : BaseRequestHandler<UserProject, ManageProjectToOwnerCommand, ResultDTO<bool>>
    {

        public ManageProjectToOwnerCommandHandler(RequestParameters<UserProject> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(ManageProjectToOwnerCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync()
                               .Where(up => up.ProjectID == request.projectDTO.ID
                                        && up.UserID == request.projectDTO.UserCreateID
                                        && up.Role == UserRole.Owner)
                               .FirstOrDefaultAsync();

            if (result is not null)
            {
                return ResultDTO<bool>.Faliure("User ID is already managed this project!");
            }

            var userProject = new UserProject()
            {
                UserID = request.projectDTO.UserCreateID,
                ProjectID = request.projectDTO.ID,
                AssignedDate = null,
                Role = UserRole.Owner
            };

            await _repository.AddAsync(userProject);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Manage Project to User successfully!");
        }
    }
}
