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

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ResultViewModel<ProjectCreateDTO>> CreateProject(ProjectCreateViewModel projectViewModel)
        {
            var projectDTO = projectViewModel.MapOne<ProjectCreateDTO>();

            var resultDTO = await _mediator.Send(new CreateProjectOrchestrator(projectDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<ProjectCreateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<ProjectCreateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<ProjectViewDTO>>> GetAllProjectsByUserCreate(int userID)
        {

            var resultDTO = await _mediator.Send(new ViewAllProjectsQuery(userID));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<ProjectViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<ProjectViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel<IEnumerable<ProjectViewDTO>>> SearchProjectsByName(ProjectSearchViewModel projectSearchViewModel)
        {

            var projectSearchDTO = projectSearchViewModel.MapOne<ProjectSearchDTO>();

            var resultDTO = await _mediator.Send(new SearchProjectsByNameQuery(projectSearchDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<ProjectViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<ProjectViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpDelete]
        public async Task<ResultViewModel<bool>> DeleteProject(int projectID)
        {

            var resultDTO = await _mediator.Send(new DeleteProjectCommand(projectID));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPut]
        public async Task<ResultViewModel<ProjectUpdateDTO>> UpdateProject(ProjectUpdateViewModel projectUpdateViewModel)
        {
            var projectUpdateDTO = projectUpdateViewModel.MapOne<ProjectUpdateDTO>();

            var resultDTO = await _mediator.Send(new UpdateProjectCommand(projectUpdateDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<ProjectUpdateDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<ProjectUpdateDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel<bool>> AssignProjectToUser(AssignProjectViewModel assignProjectViewModel)
        {
            var assignProjectDTO = assignProjectViewModel.MapOne<AssignProjectDTO>();

            var resultDTO = await _mediator.Send(new AssignProjectToUserCommand(assignProjectDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel<bool>> UnassignProjectToUser(UnassignProjectViewModel unassignProjectViewModel)
        {
            var unassignProjectDTO = unassignProjectViewModel.MapOne<UnassignProjectDTO>();

            var resultDTO = await _mediator.Send(new UnassignProjectToUserCommand(unassignProjectDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }

    }
}
