using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Projects.Orchestrators;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.ViewModel.Projects;
using ProjectManagementSystem.Application.ViewModel;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.ViewModel.Tasks;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.CQRS.Tasks.Commands;
using ProjectManagementSystem.Application.CQRS.Tasks.Orchestrators;
using ProjectManagementSystem.Application.CQRS.Projects.Queries;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.CQRS.Tasks.Queries;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : BaseController
    {

        public TaskController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }


        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<TaskCreateDTO>> CreateTask(TaskCreateViewModel taskViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<TaskCreateDTO>.Faliure("User isn't Login");
            }

            var taskDTO = taskViewModel.MapOne<TaskCreateDTO>();

            taskDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new CreateTaskOrchestrator(taskDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<TaskCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<TaskCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<TaskReturnViewDTO>>> GetAllTasks(ViewTaskViewModel viewTaskViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<TaskReturnViewDTO>>.Faliure("User isn't Login");
            }

            var taskViewDTO = viewTaskViewModel.MapOne<TaskViewDTO>();

            taskViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllTasksQuery(taskViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<TaskReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<TaskReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<TaskReturnViewDTO>>> SearchTasksByName(TaskSearchViewModel taskSearchViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<TaskReturnViewDTO>>.Faliure("User isn't Login");
            }

            var taskSearchDTO = taskSearchViewModel.MapOne<TaskSearchDTO>();

            taskSearchDTO.userID = userID;

            var resultDTO = await _mediator.Send(new SearchTasksByNameQuery(taskSearchDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<TaskReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<TaskReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }



        [HttpDelete]
        [Authorize]
        public async Task<ResultViewModel<bool>> DeleteTask(int taskID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            TaskDeleteDTO taskDeleteDTO = new TaskDeleteDTO()
            {
                ID = taskID,
                UserCreateID = userID
            };

            var resultDTO = await _mediator.Send(new DeleteTaskCommand(taskDeleteDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPut]
        [Authorize]
        public async Task<ResultViewModel<TaskUpdateDTO>> UpdateTask(TaskUpdateViewModel taskUpdateViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<TaskUpdateDTO>.Faliure("User isn't Login");
            }

            var taskUpdateDTO = taskUpdateViewModel.MapOne<TaskUpdateDTO>();

            taskUpdateDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new UpdateTaskOrchestrator(taskUpdateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<TaskUpdateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<TaskUpdateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



        [HttpPut]
        [Authorize]
        public async Task<ResultViewModel<TaskUpdateStatusDTO>> UpdateTaskStatus(TaskUpdateStatusViewModel taskUpdateStatusViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<TaskUpdateStatusDTO>.Faliure("User isn't Login");
            }

            var taskUpdateStatusDTO = taskUpdateStatusViewModel.MapOne<TaskUpdateStatusDTO>();

            taskUpdateStatusDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new UpdateStatusTaskCommand(taskUpdateStatusDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<TaskUpdateStatusDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<TaskUpdateStatusDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpGet]
        [Authorize]
        public async Task<ResultViewModel<TaskReturnViewGroupByStatusDTO>> GetAllTasksByProjectGroupByStatus(int projectID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<TaskReturnViewGroupByStatusDTO>.Faliure("User isn't Login");
            }

            var resultDTO = await _mediator.Send(new GetAllTasksGroupByStatusOrchestrator(projectID));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<TaskReturnViewGroupByStatusDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<TaskReturnViewGroupByStatusDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }



    }
}
