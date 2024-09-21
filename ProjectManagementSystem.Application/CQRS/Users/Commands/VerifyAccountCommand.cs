using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Users;
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
    

    public record VerifyAccountCommand(VerifyUserDTO verifyUserDTO) : IRequest<ResultDTO<bool>>;

    public class VerifyAccountCommandHandler : BaseRequestHandler<User, VerifyAccountCommand, ResultDTO<bool>>
    {

        public VerifyAccountCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllAsync()
                                .Where(u => u.Email == request.verifyUserDTO.Email &&
                                            u.OTP == request.verifyUserDTO.OTP &&
                                            !u.IsEmailVerified)
                                .FirstOrDefaultAsync();
       

            if (user is null)
            {
                return ResultDTO<bool>.Faliure("Email or OTP is incorrect");
            }

            user.IsEmailVerified = true;

            await _repository.UpdateAsync(user);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Verify Account Successfully!");
        }

    }
}
