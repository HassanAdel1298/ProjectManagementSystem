using MediatR;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Users;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Users.Orchestrators
{
    public record RegisterUserOrchestrator(RegisterUserDTO registerUserDTO) : IRequest<ResultDTO<RegisterUserDTO>>;

    public class RegisterUserOrchestratorHandler : BaseRequestHandler<User ,RegisterUserOrchestrator, ResultDTO<RegisterUserDTO>>
    {
        public RegisterUserOrchestratorHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<RegisterUserDTO>> Handle(RegisterUserOrchestrator request, CancellationToken cancellationToken)
        {
            var resultRegisterUserDTO = await _mediator.Send(new RegisterUserCommand(request.registerUserDTO));

            if (!resultRegisterUserDTO.IsSuccess)
            {
                return resultRegisterUserDTO;
            }

            SendEmailDTO sendEmailDTO = new SendEmailDTO()
            {
                ToEmail = resultRegisterUserDTO.Data.Email,
                Subject = "Verify your email",
                Body = $"Please verify your email address by OTP : {resultRegisterUserDTO.Data.OTP}"
            };
    

            await _mediator.Send(new SendVerificationEmailCommand(sendEmailDTO));


            return resultRegisterUserDTO;
        }
    }
}
