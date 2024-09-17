using MediatR;
using Microsoft.EntityFrameworkCore;
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

    public record UnassignProjectToUserCommand(UnassignProjectDTO unassignProjectDTO) : IRequest<ResultDTO<bool>>;

    public class UnassignProjectDTO
    {
        public int UserCreateID { get; set; }
        public int UserUnassignID { get; set; }
        public int ProjectID { get; set; }
    }

    public class UnassignProjectToUserCommandHandler : IRequestHandler<UnassignProjectToUserCommand, ResultDTO<bool>>
    {
        IRepository<UserProject> _repository;
        IMediator _mediator;

        public UnassignProjectToUserCommandHandler(IRepository<UserProject> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(UnassignProjectToUserCommand request, CancellationToken cancellationToken)
        {
            var manageProject = await _repository.GetAllAsync()
                               .Where(up => up.ProjectID == request.unassignProjectDTO.ProjectID
                                        && up.UserID == request.unassignProjectDTO.UserCreateID
                                        && up.Role == UserRole.Owner)
                               .FirstOrDefaultAsync();

            if (manageProject is null)
            {
                return ResultDTO<bool>.Faliure("User Create ID isn't managed this project!");
            }

            var useProject = await _repository.GetAllAsync()
                               .Where(up => up.ProjectID == request.unassignProjectDTO.ProjectID
                                        && up.UserID == request.unassignProjectDTO.UserUnassignID
                                        && up.Role == UserRole.User)
                               .FirstOrDefaultAsync();

            if (useProject is null)
            {
                return ResultDTO<bool>.Faliure("User Unassign ID isn't used this project or User Unassign ID is Owner!");
            }

            await _repository.DeleteAsync(useProject);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Unassign Project to User successfully!");
        }
    }
}
