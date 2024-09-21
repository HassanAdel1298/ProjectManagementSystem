using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Users;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Users.Commands
{

    public record ResetPasswordCommand(ResetPasswordDto resetPasswordDto) : IRequest<ResultDTO<bool>>;
    
    

    public class ResetPasswordCommandHandler : BaseRequestHandler<User, ResetPasswordCommand, ResultDTO<bool>>
    {

        public ResetPasswordCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.resetPasswordDto.NewPassword != request.resetPasswordDto.ConfirmNewPassword)
            {
                return ResultDTO<bool>.Faliure("Confirm New Password does not match the New Password.");
            }

            var user = await _repository.GetAllAsync()
                               .Where(u => u.Email == request.resetPasswordDto.Email
                                        && u.IsEmailVerified )
                               .FirstOrDefaultAsync();

            if (user is null)
            {
                return ResultDTO<bool>.Faliure("Email is incorrect!");
            }

            user.PasswordHash = PasswordHashGenerator.CreatePasswordHash(request.resetPasswordDto.NewPassword);

            await _repository.UpdateAsync(user);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Reset New Password successfully!");
        }
    }
}
