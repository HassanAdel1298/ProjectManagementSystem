using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.CQRS.Tasks.Orchestrators;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.ViewModel.Tasks;
using ProjectManagementSystem.Application.ViewModel;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.DTO.TaskTimers;
using ProjectManagementSystem.Application.ViewModel.TaskTimers;
using ProjectManagementSystem.Application.CQRS.TaskTimers.Orchestrator;
using ProjectManagementSystem.Application.CQRS.Tasks.Queries;
using ProjectManagementSystem.Application.CQRS.TaskTimers.Queries;
using ProjectManagementSystem.Application.CQRS.Tasks.Commands;
using ProjectManagementSystem.Application.CQRS.TaskTimers.Commands;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskTimerController : BaseController
    {
        public TaskTimerController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<TaskTimerCreateDTO>> CreateTaskTimer(TaskTimerCreateViewModel taskTimerViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<TaskTimerCreateDTO>.Faliure("User isn't Login");
            }

            var taskTimerDTO = taskTimerViewModel.MapOne<TaskTimerCreateDTO>();

            taskTimerDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new CreateTaskTimerOrchestrator(taskTimerDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<TaskTimerCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<TaskTimerCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<TaskTimerReturnViewDTO>>> GetAllTaskTimers(ViewTaskTimerViewModel viewTaskTimerViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<TaskTimerReturnViewDTO>>.Faliure("User isn't Login");
            }

            var taskTimerViewDTO = viewTaskTimerViewModel.MapOne<TaskTimerViewDTO>();

            taskTimerViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllTaskTimersQuery(taskTimerViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<TaskTimerReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<TaskTimerReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }



        [HttpDelete]
        [Authorize]
        public async Task<ResultViewModel<bool>> DeleteTaskTimer(int taskTimerID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            TaskTimerDeleteDTO taskTimerDeleteDTO = new TaskTimerDeleteDTO()
            {
                ID = taskTimerID,
                UserCreateID = userID
            };

            var resultDTO = await _mediator.Send(new DeleteTaskTimerCommand(taskTimerDeleteDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPut]
        [Authorize]
        public async Task<ResultViewModel<TaskTimerUpdateDTO>> UpdateTaskTimer(TaskTimerUpdateViewModel taskTimerUpdateViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<TaskTimerUpdateDTO>.Faliure("User isn't Login");
            }

            var taskTimerUpdateDTO = taskTimerUpdateViewModel.MapOne<TaskTimerUpdateDTO>();

            taskTimerUpdateDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new UpdateTaskTimerCommand(taskTimerUpdateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<TaskTimerUpdateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<TaskTimerUpdateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



    }
}
