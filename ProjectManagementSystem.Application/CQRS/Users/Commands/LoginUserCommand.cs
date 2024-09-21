using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public record LoginUserCommand(LoginUserDTO loginUserDTO) : IRequest<ResultDTO<string>>;

    public class LoginUserCommandHandler : BaseRequestHandler<User,LoginUserCommand, ResultDTO<string>>
    {

        public LoginUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllAsync()
                                    .Where(u => u.Email == request.loginUserDTO.Email
                                            && u.IsEmailVerified)
                                    .FirstOrDefaultAsync();

            if (user is null 
                || !BCrypt.Net.BCrypt.Verify(request.loginUserDTO.Password, user.PasswordHash))
            {
                return ResultDTO<string>.Faliure("Email or Password is incorrect");
            }

            var token = TokenGenerator.GenerateToken(user.ID.ToString(),user.Email,user.FullName);

            return ResultDTO<string>.Sucess(token, "User Login Successfully!");
        }

    }
}
