using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public record LoginUserCommand(LoginUserDTO loginUserDTO) : IRequest<ResultDTO<string>>;

    public class LoginUserDTO 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    } 

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDTO<string>>
    {
        IRepository<User> _repository;
        IMediator _mediator;

        public LoginUserCommandHandler(IRepository<User> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllAsync()
                                    .Where(u => u.Email == request.loginUserDTO.Email)
                                    .FirstOrDefaultAsync();

            if (user is null 
                || !BCrypt.Net.BCrypt.Verify(request.loginUserDTO.Password, user.PasswordHash) 
                || !user.IsEmailVerified)
            {
                return ResultDTO<string>.Faliure("Email or Password is incorrect");
            }

            var token = TokenGenerator.GenerateToken(user.ID.ToString()
                                , user.FullName
                                , user.UserRoles.Where(r => r.UserID == user.ID).ToString());

            return ResultDTO<string>.Sucess(token, "User Login Successfully!");
        }

    }
}
