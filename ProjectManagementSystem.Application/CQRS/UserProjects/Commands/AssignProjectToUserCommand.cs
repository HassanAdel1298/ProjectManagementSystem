using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.DTO;
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

    public class AssignProjectDTO
    {
        public int UserCreateID { get; set; }
        public int UserAssignID { get; set; }
        public int ProjectID { get; set; }
    }

    public class AssignProjectToUserCommandHandler : IRequestHandler<AssignProjectToUserCommand, ResultDTO<bool>>
    {
        IRepository<UserProject> _repository;
        IMediator _mediator;

        public AssignProjectToUserCommandHandler(IRepository<UserProject> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(AssignProjectToUserCommand request, CancellationToken cancellationToken)
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
