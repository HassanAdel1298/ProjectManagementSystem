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

    public record ChangePasswordOrchestrator(string Email) : IRequest<ResultDTO<OTPAddedDTO>>;

    public class ChangePasswordOrchestratorHandler : BaseRequestHandler<User,ChangePasswordOrchestrator
                                                            , ResultDTO<OTPAddedDTO>>
    {
        public ChangePasswordOrchestratorHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<OTPAddedDTO>> Handle(ChangePasswordOrchestrator request, CancellationToken cancellationToken)
        {
            var resultAddOTPDTO = await _mediator.Send(new AddOTPToEmailCommand(request.Email));

            if (!resultAddOTPDTO.IsSuccess)
            {
                return resultAddOTPDTO;
            }

            SendEmailDTO sendEmailDTO = new SendEmailDTO()
            {
                ToEmail = resultAddOTPDTO.Data.Email,
                Subject = "Verify your email",
                Body = $"Please verify your email address by OTP : {resultAddOTPDTO.Data.OTP}"
            };



            await _mediator.Send(new SendVerificationEmailCommand(sendEmailDTO));



            return resultAddOTPDTO;
        }
    }
}
