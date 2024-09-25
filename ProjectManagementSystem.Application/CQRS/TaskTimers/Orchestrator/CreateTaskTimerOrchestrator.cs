using MediatR;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Tasks.Commands;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;
using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO.TaskTimers;
using ProjectManagementSystem.Application.CQRS.TaskTimers.Commands;

namespace ProjectManagementSystem.Application.CQRS.TaskTimers.Orchestrator
{

    public record CreateTaskTimerOrchestrator(TaskTimerCreateDTO taskTimerDTO) : IRequest<ResultDTO<TaskTimerCreateDTO>>;


    public class CreateTaskTimerOrchestratorHandler : BaseRequestHandler<TaskTimer, CreateTaskTimerOrchestrator, ResultDTO<TaskTimerCreateDTO>>
    {

        public CreateTaskTimerOrchestratorHandler(RequestParameters<TaskTimer> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<TaskTimerCreateDTO>> Handle(CreateTaskTimerOrchestrator request, CancellationToken cancellationToken)
        {

            

            var resultIsDeletedTask = await _mediator.Send(new IsDeletedTaskCommand(request.taskTimerDTO));

            if (!resultIsDeletedTask.IsSuccess)
            {
                return ResultDTO<TaskTimerCreateDTO>.Faliure(resultIsDeletedTask.Message);
            }


            var resultCreateTaskTimerDTO = await _mediator.Send(new CreateTaskTimerCommand(request.taskTimerDTO));

            

            return resultCreateTaskTimerDTO;
        }


    }
}
