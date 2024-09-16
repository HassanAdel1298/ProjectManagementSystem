using MediatR;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Users.Orchestrators
{

    public record ChangePasswordOrchestrator(string Email) : IRequest<ResultDTO<OTPAddedDTO>>;

    public class ChangePasswordOrchestratorHandler : IRequestHandler<ChangePasswordOrchestrator
                                                            , ResultDTO<OTPAddedDTO>>
    {
        private readonly IMediator _mediator;
        public ChangePasswordOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultDTO<OTPAddedDTO>> Handle(ChangePasswordOrchestrator request, CancellationToken cancellationToken)
        {
            var resultDTO = await _mediator.Send(new AddOTPToEmailCommand(request.Email));

            if (!resultDTO.IsSuccess)
            {
                return resultDTO;
            }

            SendEmailDTO sendEmailDTO = new SendEmailDTO()
            {
                ToEmail = resultDTO.Data.Email,
                Subject = "Verify your email",
                Body = $"Please verify your email address by OTP : {resultDTO.Data.OTP}"
            };

   

            await _mediator.Send(new SendVerificationEmailCommand(sendEmailDTO));



            return resultDTO;
        }
    }
}
