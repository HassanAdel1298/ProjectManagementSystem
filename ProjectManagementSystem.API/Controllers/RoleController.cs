using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.CQRS.Roles.Commands;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.ViewModel;
using ProjectManagementSystem.Application.ViewModel.Roles;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ResultViewModel<bool>> CreateRole(RoleViewModel roleViewModel)
        {
            var roleDTO = roleViewModel.MapOne<RoleDTO>();

            var resultDTO = await _mediator.Send(new CreateRoleCommand(roleDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }
    }
}
