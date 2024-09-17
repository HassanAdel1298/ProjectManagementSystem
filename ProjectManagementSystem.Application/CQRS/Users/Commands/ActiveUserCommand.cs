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

namespace ProjectManagementSystem.Application.CQRS.Users.Commands
{

    public record ActiveUserCommand(int userID) : IRequest<ResultDTO<bool>>;


    public class ActiveUserCommandHandler : IRequestHandler<ActiveUserCommand, ResultDTO<bool>>
    {
        IRepository<User> _repository;
        IMediator _mediator;

        public ActiveUserCommandHandler(IRepository<User> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(ActiveUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _repository.GetAllAsync()
                               .Where(u => u.ID == request.userID)
                               .FirstOrDefaultAsync();

            if (user is null)
            {
                return ResultDTO<bool>.Faliure("User ID is incorrect!");
            }


            user.IsActive = true;

            await _repository.UpdateAsync(user);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Active Account Successfully!");
        }






    }
}
