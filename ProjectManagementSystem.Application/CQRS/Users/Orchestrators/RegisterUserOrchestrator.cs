using MediatR;
using ProjectManagementSystem.Application.CQRS.UserRoles.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Users.Orchestrators
{
    public record RegisterUserOrchestrator(RegisterUserDTO registerUserDTO) : IRequest<ResultDTO<RegisterUserDTO>>;

    public class RegisterUserOrchestratorHandler : IRequestHandler<RegisterUserOrchestrator, ResultDTO<RegisterUserDTO>>
    {
        private readonly IMediator _mediator;
        public RegisterUserOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultDTO<RegisterUserDTO>> Handle(RegisterUserOrchestrator request, CancellationToken cancellationToken)
        {
            var resultDTO = await _mediator.Send(new RegisterUserCommand(request.registerUserDTO));

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



            //UserRoleDTO userRoleDTO = new UserRoleDTO(resultDTO.Data.ID, 1);

            //await _mediator.Send(new AssignUserToRoleCommand(userRoleDTO));

            return resultDTO;
        }
    }
}
