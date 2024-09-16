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

namespace ProjectManagementSystem.Application.CQRS.UserRoles.Commands
{
    public record AssignUserToRoleCommand(UserRoleDTO userRoleDTO) : IRequest<ResultDTO<UserRoleDTO>>;

    public class UserRoleDTO 
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    } 

    public class AssignUserToRoleCommandHandler : IRequestHandler<AssignUserToRoleCommand, ResultDTO<UserRoleDTO>>
    {
        IRepository<UserRole> _repository;
        IMediator _mediator;

        public AssignUserToRoleCommandHandler(IRepository<UserRole> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<UserRoleDTO>> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _repository.GetAllAsync().Where
                                (ur => (ur.RoleID == request.userRoleDTO.RoleID) 
                                    && (ur.UserID == request.userRoleDTO.UserID)
                                ).FirstOrDefaultAsync();

            if (userRole is not null)
            {
                return ResultDTO<UserRoleDTO>.Faliure("User is already assigned to this role!");
            }

            userRole = request.userRoleDTO.MapOne<UserRole>();

            userRole = await _repository.AddAsync(userRole);
            await _repository.SaveChangesAsync();

            var userRoleDTO = userRole.MapOne<UserRoleDTO>();

            return ResultDTO<UserRoleDTO>.Sucess(userRoleDTO, "User assigned to role successfully!");
        }
    }
}
