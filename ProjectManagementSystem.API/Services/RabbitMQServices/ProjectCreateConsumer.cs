using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Orchestrators;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;

namespace ProjectManagementSystem.API.Services.RabbitMQServices
{
    public class ProjectCreateConsumer : IBaseConsumer<ProjectCreateMessage>
    {
        IMediator _mediator;
        public ProjectCreateConsumer(IMediator mediator) 
        { 
            _mediator = mediator;
        }

        public async Task<Task> Consume(ProjectCreateMessage message)
        {
            ProjectCreateDTO projectCreateDTO = new ProjectCreateDTO()
            {
                Title = message.Title,
                Description = message.Description,
                IsPublic = message.IsPublic,
                UserCreateID = message.UserCreateID
            };
            var createProjectOrchestrator = await _mediator.Send(new CreateProjectOrchestrator(projectCreateDTO));
            
            return Task.CompletedTask;
        }
    }
}
