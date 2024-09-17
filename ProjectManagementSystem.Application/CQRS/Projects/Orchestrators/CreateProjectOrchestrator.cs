using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Projects.Orchestrators
{

    public record CreateProjectOrchestrator(ProjectCreateDTO projectDTO) : IRequest<ResultDTO<ProjectCreateDTO>>;

    public class CreateProjectOrchestratorHandler : IRequestHandler<CreateProjectOrchestrator, ResultDTO<ProjectCreateDTO>>
    {
        private readonly IMediator _mediator;
        public CreateProjectOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResultDTO<ProjectCreateDTO>> Handle(CreateProjectOrchestrator request, CancellationToken cancellationToken)
        {
            var resultDTO = await _mediator.Send(new CreateProjectCommand(request.projectDTO));

            if (!resultDTO.IsSuccess)
            {
                return resultDTO;
            }

            resultDTO.Data.UserCreateID = request.projectDTO.UserCreateID;

            await _mediator.Send(new ManageProjectToUserCommand(resultDTO.Data));
            
            
            await _mediator.Send(new ActiveUserCommand(resultDTO.Data.UserCreateID));


            return resultDTO;
        }
    }
}
