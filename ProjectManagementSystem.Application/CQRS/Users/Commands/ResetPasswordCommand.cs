using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Application.DTO;
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
    

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

    }
        
    

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultDTO<bool>>
    {
        IRepository<User> _repository;
        IMediator _mediator;

        public ResetPasswordCommandHandler(IRepository<User> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.resetPasswordDto.NewPassword != request.resetPasswordDto.ConfirmNewPassword)
            {
                return ResultDTO<bool>.Faliure("Confirm New Password does not match the New Password.");
            }

            var user = await _repository.GetAllAsync()
                               .Where(u => u.Email == request.resetPasswordDto.Email)
                               .FirstOrDefaultAsync();

            if (user is null || !user.IsEmailVerified)
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
