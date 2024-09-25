using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.CQRS.Projects.Orchestrators
{

    public record CreateProjectOrchestrator(ProjectCreateDTO projectDTO) : IRequest<ResultDTO<ProjectCreateDTO>>;

    public class CreateProjectOrchestratorHandler : BaseRequestHandler<Project , CreateProjectOrchestrator, ResultDTO<ProjectCreateDTO>>
    {
        public CreateProjectOrchestratorHandler(RequestParameters<Project> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<ProjectCreateDTO>> Handle(CreateProjectOrchestrator request, CancellationToken cancellationToken)
        {
            var resultCreateProjectDTO = await _mediator.Send(new CreateProjectCommand(request.projectDTO));

            if (!resultCreateProjectDTO.IsSuccess)
            {
                return resultCreateProjectDTO;
            }

            resultCreateProjectDTO.Data.UserCreateID = request.projectDTO.UserCreateID;

            await _mediator.Send(new ManageProjectToOwnerCommand(resultCreateProjectDTO.Data));


            var resultActiveUserDTO = await _mediator.Send(new ActiveUserCommand(resultCreateProjectDTO.Data.UserCreateID));

            if (!resultActiveUserDTO.IsSuccess)
            {
                return ResultDTO<ProjectCreateDTO>.Faliure(resultActiveUserDTO.Message);
            }

            SendEmailMessage baseMessage = new SendEmailMessage
            {
                Sender = "ProjectManagementSystem",
                Action = "SendEmail",
                SentDate = DateTime.Now,
                ToEmail = _userState.Email,
                Subject = "Create New Project",
                Body = $"Create your Project : {request.projectDTO.Title}"
            };

            baseMessage.Type = baseMessage.GetType().Name;

            string msg = Newtonsoft.Json.JsonConvert.SerializeObject(baseMessage);


            _rabbitMQService.PublishMessage(msg);

            return resultCreateProjectDTO;
        }
    }
}
