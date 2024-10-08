﻿using MediatR;
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
    public record RegisterUserCommand(RegisterUserDTO registerUserDTO) : IRequest<ResultDTO<RegisterUserDTO>>;   


    public class RegisterUserCommandHandler : BaseRequestHandler<User,RegisterUserCommand, ResultDTO<RegisterUserDTO>>
    {

        public RegisterUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<RegisterUserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (request.registerUserDTO.Password != request.registerUserDTO.ConfirmPassword)
            {
                return ResultDTO<RegisterUserDTO>.Faliure("Confirm Password does not match the Password.");
            }

            var result = await _repository.GetAllAsync()
                               .Where(u => u.Email == request.registerUserDTO.Email)
                               .FirstOrDefaultAsync();

            if (result is not null)
            {
                return ResultDTO<RegisterUserDTO>.Faliure("Email is already registered!");
            }

            
           

            var user = request.registerUserDTO.MapOne<User>();

            user.PasswordHash = PasswordHashGenerator.CreatePasswordHash(request.registerUserDTO.Password);

            user.OTP = OTPGenerator.CreateOTP();

            user = await _repository.AddAsync(user);

            await _repository.SaveChangesAsync();

            var userDTO = user.MapOne<RegisterUserDTO>();

            return ResultDTO<RegisterUserDTO>.Sucess(userDTO, "User registred successfully!");
        }


        

        

    }
}
