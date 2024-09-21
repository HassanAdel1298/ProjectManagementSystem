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


    public class ActiveUserCommandHandler : BaseRequestHandler<User , ActiveUserCommand, ResultDTO<bool>>
    {

        public ActiveUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(ActiveUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _repository.GetAllAsync()
                               .Where(u => u.ID == request.userID
                                      && u.IsEmailVerified)
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
