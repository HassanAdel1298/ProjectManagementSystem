using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Users.Commands
{
   
    public record IsVerifiedUserCommand(int UserID) : IRequest<ResultDTO<bool>>;

    public class IsVerifiedUserCommandHandler : BaseRequestHandler<User, IsVerifiedUserCommand, ResultDTO<bool>>
    {

        public IsVerifiedUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(IsVerifiedUserCommand request, CancellationToken cancellationToken)
        {
            var isVerifiedUser = await _repository.GetAllAsync()
                                .AnyAsync(u => u.ID == request.UserID 
                                        && u.IsEmailVerified);


            if (!isVerifiedUser)
            {
                return ResultDTO<bool>.Faliure("User ID isn't Verified");
            }

            return ResultDTO<bool>.Sucess(true, "User ID is Verified");
        }

    }
}
