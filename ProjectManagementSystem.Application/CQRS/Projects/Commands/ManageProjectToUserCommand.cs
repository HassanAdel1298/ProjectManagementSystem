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

    public record ManageProjectToUserCommand(ProjectCreateDTO projectDTO) : IRequest<ResultDTO<bool>>;

 

    public class ManageProjectToUserCommandHandler : IRequestHandler<ManageProjectToUserCommand, ResultDTO<bool>>
    {
        IRepository<UserProject> _repository;
        IMediator _mediator;

        public ManageProjectToUserCommandHandler(IRepository<UserProject> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(ManageProjectToUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync()
                               .Where(up => up.ProjectID == request.projectDTO.ID 
                                        && up.UserID == request.projectDTO.UserCreateID)
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
