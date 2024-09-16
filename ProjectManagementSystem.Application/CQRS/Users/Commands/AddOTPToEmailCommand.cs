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


    public record AddOTPToEmailCommand(string Email) : IRequest<ResultDTO<OTPAddedDTO>>;

    public class OTPAddedDTO
    {
       public string Email { get; set; }
       public string OTP { get; set; }
    }
       

    public class AddOTPToEmailCommandHandler : IRequestHandler<AddOTPToEmailCommand, ResultDTO<OTPAddedDTO>>
    {
        IRepository<User> _repository;
        IMediator _mediator;

        public AddOTPToEmailCommandHandler(IRepository<User> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<OTPAddedDTO>> Handle(AddOTPToEmailCommand request, CancellationToken cancellationToken)
        {

            var user = await _repository.GetAllAsync()
                               .Where(u => u.Email == request.Email)
                               .FirstOrDefaultAsync();

            if (user is null)
            {
                return ResultDTO<OTPAddedDTO>.Faliure("Email is incorrect!");
            }


            user.OTP = OTPGenerator.CreateOTP();

            user = await _repository.UpdateAsync(user);

            await _repository.SaveChangesAsync();

            var OTPAdded = user.MapOne<OTPAddedDTO>();

            return ResultDTO<OTPAddedDTO>.Sucess(OTPAdded, "Add OTP to this Email successfully!");
        }






    }
}
