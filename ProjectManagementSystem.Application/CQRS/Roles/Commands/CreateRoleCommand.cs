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

namespace ProjectManagementSystem.Application.CQRS.Roles.Commands
{
    public record CreateRoleCommand(RoleDTO roleDTO) : IRequest<ResultDTO<bool>>;

    public class RoleDTO 
    {
       public string Name { get; set; }
    } 

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ResultDTO<bool>>
    {
        IRepository<Role> _repository;
        IMediator _mediator;

        public CreateRoleCommandHandler(IRepository<Role> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync()
                            .Where(r => r.Name == request.roleDTO.Name)
                            .FirstOrDefaultAsync();

            if (result is not null)
            {
                return ResultDTO<bool>.Faliure("Role is already exists!");
            }

            var role = request.roleDTO.MapOne<Role>();

            await _repository.AddAsync(role);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Role created successfully!");
        }
    }
}
