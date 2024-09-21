using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.ViewModel;
using ProjectManagementSystem.Application.ViewModel.Projects;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.CQRS.Projects.Commands;
using ProjectManagementSystem.Application.CQRS.Projects.Queries;
using ProjectManagementSystem.Application.CQRS.Projects.Orchestrators;
using ProjectManagementSystem.Application.CQRS.UserProjects.Commands;
using ProjectManagementSystem.Application.ViewModel.UserProject;
using ProjectManagementSystem.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.DTO.UserProjects;
using ProjectManagementSystem.Application.CQRS.UserProjects.Orchestrators;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : BaseController
    {


        public ProjectController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }


        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<ProjectCreateDTO>> CreateProject(ProjectCreateViewModel projectViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<ProjectCreateDTO>.Faliure("User isn't Login");
            }

            var projectDTO = projectViewModel.MapOne<ProjectCreateDTO>();

            projectDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new CreateProjectOrchestrator(projectDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<ProjectCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<ProjectCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpGet]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<ProjectReturnViewDTO>>> GetAllProjects(ViewProjectViewModel viewProjectViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID,out userID);

            if (!isUserID) 
            {
                return ResultViewModel<IEnumerable<ProjectReturnViewDTO>>.Faliure("User isn't Login");
            }

            var projectViewDTO = viewProjectViewModel.MapOne<ProjectViewDTO>();

            projectViewDTO.userID = userID;

            var resultDTO = await _mediator.Send(new ViewAllProjectsQuery(projectViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<ProjectReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<ProjectReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<ProjectReturnViewDTO>>> SearchProjectsByName(ProjectSearchViewModel projectSearchViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<ProjectReturnViewDTO>>.Faliure("User isn't Login");
            }

            var projectSearchDTO = projectSearchViewModel.MapOne<ProjectSearchDTO>();

            projectSearchDTO.userID = userID;

            var resultDTO = await _mediator.Send(new SearchProjectsByNameQuery(projectSearchDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<ProjectReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<ProjectReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpDelete]
        [Authorize]
        public async Task<ResultViewModel<bool>> DeleteProject(int projectID)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            ProjectDeleteDTO projectDeleteDTO = new ProjectDeleteDTO()
            {
                ID = projectID,
                UserCreateID = userID
            };

            var resultDTO = await _mediator.Send(new DeleteProjectCommand(projectDeleteDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPut]
        [Authorize]
        public async Task<ResultViewModel<ProjectUpdateDTO>> UpdateProject(ProjectUpdateViewModel projectUpdateViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<ProjectUpdateDTO>.Faliure("User isn't Login");
            }

            var projectUpdateDTO = projectUpdateViewModel.MapOne<ProjectUpdateDTO>();

            projectUpdateDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new UpdateProjectCommand(projectUpdateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<ProjectUpdateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<ProjectUpdateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<bool>> AssignProjectToUser(AssignProjectViewModel assignProjectViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            var assignProjectDTO = assignProjectViewModel.MapOne<AssignProjectDTO>();

            assignProjectDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new AssignProjectToUserOrchestrator(assignProjectDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<bool>> UnassignProjectToUser(UnassignProjectViewModel unassignProjectViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }

            var unassignProjectDTO = unassignProjectViewModel.MapOne<UnassignProjectDTO>();

            unassignProjectDTO.UserCreateID = userID;

            var resultDTO = await _mediator.Send(new UnassignProjectToUserOrchestrator(unassignProjectDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }

    }
}
