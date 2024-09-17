﻿using MediatR;
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
    public record RegisterUserCommand(RegisterUserDTO registerUserDTO) : IRequest<ResultDTO<RegisterUserDTO>>;

    public class RegisterUserDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Country { get; set; }
        public string OTP { get; set; } 

    }
       

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO<RegisterUserDTO>>
    {
        IRepository<User> _repository;
        IMediator _mediator;

        public RegisterUserCommandHandler(IRepository<User> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<RegisterUserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
