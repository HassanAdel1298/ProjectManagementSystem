using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.UserProjects;
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

    public record AssignProjectToUserCommand(AssignProjectDTO assignProjectDTO) : IRequest<ResultDTO<bool>>;


    public class AssignProjectToUserCommandHandler : BaseRequestHandler<UserProject , AssignProjectToUserCommand, ResultDTO<bool>>
    {

        public AssignProjectToUserCommandHandler(RequestParameters<UserProject> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(AssignProjectToUserCommand request, CancellationToken cancellationToken)
        {
            var manageProject = await _repository.GetAllAsync()
                               .Where(up => up.ProjectID == request.assignProjectDTO.ProjectID
                                        && up.UserID == request.assignProjectDTO.UserCreateID
                                        && up.Role == UserRole.Owner)
                               .FirstOrDefaultAsync();

            if (manageProject is null)
            {
                return ResultDTO<bool>.Faliure("User Create ID isn't managed this project!");
            }

            var useProject = await _repository.GetAllAsync()
                               .Where(up => up.ProjectID == request.assignProjectDTO.ProjectID
                                        && up.UserID == request.assignProjectDTO.UserAssignID)
                               .FirstOrDefaultAsync();

            if (useProject is not null)
            {
                return ResultDTO<bool>.Faliure("User Assign ID is already used this project!");
            }

            var newUserProject = new UserProject()
            {
                UserID = request.assignProjectDTO.UserAssignID,
                ProjectID = request.assignProjectDTO.ProjectID,
                AssignedDate = DateTime.Now,
                Role = UserRole.User
            };

            await _repository.AddAsync(newUserProject);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Assign Project to User successfully!");
        }
    }
}
